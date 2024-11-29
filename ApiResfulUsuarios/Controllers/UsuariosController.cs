using ApiResfulUsuarios.Models.DTO;
using ApiResfulUsuarios.Models.Entidades;
using ApiResfulUsuarios.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ApiResfulUsuarios.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _usuariosRepository;

        public UsuariosController(IUsuarioRepository usuariosRepository)
        {
            _usuariosRepository = usuariosRepository;
        }

        [HttpGet]
        public IActionResult ObtenerUsuarios()
        {
            var usuarios = _usuariosRepository.ObtenerTodos().Select(u => new UsuarioDto
            {
                Id = u.Id,
                Nombre = u.Nombre,
                Email = u.Email,
            });

            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public IActionResult ObternerUsuarioPorId(int id)
        {
            var usuario = _usuariosRepository.ObtenerPorId(id);

            if (usuario == null)
                return NotFound();

            var usuarioDto = new UsuarioDto
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Email = usuario.Email
            };

            return Ok(usuarioDto);
        }

        [HttpPost]
        public IActionResult CrearUsuario([FromBody] CrearUsuarioDto crearUsuarioDto)
        {
            if (crearUsuarioDto == null)
                return BadRequest("El usuario es nulo.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_usuariosRepository.EmailExistente(crearUsuarioDto.Email))
                return Conflict("El email ya esta en uso.");

            var usuario = new Usuario
            {
                Nombre = crearUsuarioDto.Nombre,
                Email = crearUsuarioDto.Email,
                Password = crearUsuarioDto.Password //Se debe encriptar la contraseña
            };

            _usuariosRepository.Crear(usuario);

            var usuarioDto = new UsuarioDto
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Email = usuario.Email,
            };

            return CreatedAtAction(nameof(ObtenerUsuarios), new { Id = usuario.Id}, usuarioDto);
        }

        [HttpPut("{id}")]
        public IActionResult ActualizarUsuario(int id, [FromBody] ActualizarUsuarioDto actualizarUsuarioDto)
        {
            if (actualizarUsuarioDto == null)
                return BadRequest("El usuario es nulo.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuarioExistente = _usuariosRepository.ObtenerPorId(id);

            if (usuarioExistente == null)
                return NotFound("Usuario no encontrado");

            usuarioExistente.Nombre = actualizarUsuarioDto.Nombre;
            usuarioExistente.Email = actualizarUsuarioDto.Email;

            _usuariosRepository.Actualizar(usuarioExistente);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarUsuario(int id)
        {
            var usuario = _usuariosRepository.ObtenerPorId(id);

            if (usuario == null)
                return NotFound("No se encontro el usuario");

            _usuariosRepository.Eliminar(id);
            return NoContent();
        }
    }
}
