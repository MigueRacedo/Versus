using Versus.Models;

namespace Versus.Interfaces;

public interface IRepositorioCategoria
{
    Task<IEnumerable<Categoria>> ObtenerTodosAsync();
    Task<Categoria?> ObtenerPorIdAsync(int id);
    Task AgregarAsync(Categoria categoria);
    Task ActualizarAsync(Categoria categoria);
    Task EliminarAsync(int id);
    Task<bool> ExisteAsync(int id);
}
