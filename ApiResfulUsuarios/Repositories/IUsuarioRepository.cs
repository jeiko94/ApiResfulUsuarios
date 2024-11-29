using ApiResfulUsuarios.Models.Entidades;

namespace ApiResfulUsuarios.Repositories
{
    public interface IUsuarioRepository
    {
        IEnumerable<Usuario> ObtenerTodos();
        Usuario ObtenerPorId(int id);
        void Crear(Usuario usuario);
        void Actualizar(Usuario usuario);
        void Eliminar(int id);
        bool EmailExistente(string email);
    }
}
