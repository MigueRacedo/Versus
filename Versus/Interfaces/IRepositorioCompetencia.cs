using Versus.Models;

namespace Versus.Interfaces;

public interface IRepositorioCompetencia
{
    Task<IEnumerable<Competencia>> ObtenerTodosAsync();
    Task<Competencia?> ObtenerPorIdAsync(int id);
    Task AgregarAsync(Competencia competencia);
    Task ActualizarAsync(Competencia competencia);
    Task EliminarAsync(int id);
    Task<bool> ExisteAsync(int id);
}
