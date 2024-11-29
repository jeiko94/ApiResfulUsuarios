using ApiResfulUsuarios.Models.Entidades;

namespace ApiResfulUsuarios.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly List<Usuario> _usuarios = new List<Usuario>();
        private int _nextId = 1;

        public IEnumerable<Usuario> ObtenerTodos()
        {
            return _usuarios;
        }
        public Usuario ObtenerPorId(int id)
        {
            return _usuarios.FirstOrDefault(u => u.Id == id);
        }
        public void Crear(Usuario usuario)
        {
            usuario.Id = _nextId++;
            _usuarios.Add(usuario);
        }
        public void Actualizar(Usuario usuario)
        {
            var index = _usuarios.FindIndex(u => u.Id == usuario.Id);

            if (index != -1)
            {
                _usuarios[index] = usuario;
            }
        }
        public void Eliminar(int id)
        {
            var usuario = ObtenerPorId(id);

            if (usuario != null)
            {
                _usuarios.Remove(usuario);
            }
        }
        public bool EmailExistente(string email)
        {
            return _usuarios.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }
    }
}
