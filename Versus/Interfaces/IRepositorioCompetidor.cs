using Versus.Models;

namespace Versus.Interfaces;

public interface IRepositorioCompetidor
{
    Task<IEnumerable<Competidor>> ObtenerTodosAsync();
    Task<Competidor?> ObtenerPorIdAsync(int id);
    Task<IEnumerable<Competidor>> ObtenerPorCategoriaAsync(int categoriaId);
    Task AgregarAsync(Competidor competidor);
    Task ActualizarAsync(Competidor competidor);
    Task EliminarAsync(int id);
    Task<bool> ExisteAsync(int id);
}
