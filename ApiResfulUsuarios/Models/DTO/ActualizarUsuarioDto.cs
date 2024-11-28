using System.ComponentModel.DataAnnotations;

namespace ApiResfulUsuarios.Models.DTO
{
    public class ActualizarUsuarioDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El email es obligatoria.")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
