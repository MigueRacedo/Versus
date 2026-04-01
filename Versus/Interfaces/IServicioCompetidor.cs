using Versus.ViewModels;

namespace Versus.Interfaces;

public interface IServicioCompetidor
{
    Task<IEnumerable<CompetidorViewModel>> ObtenerTodosAsync();
    Task<CompetidorViewModel?> ObtenerPorIdAsync(int id);
    Task CrearAsync(CompetidorViewModel viewModel);
    Task ActualizarAsync(CompetidorViewModel viewModel);
    Task EliminarAsync(int id);
    Task AsignarCategoriaAsync(int competidorId, int? categoriaId);
    Task<IEnumerable<CompetidorViewModel>> ObtenerPorCategoriaAsync(int categoriaId);
}
