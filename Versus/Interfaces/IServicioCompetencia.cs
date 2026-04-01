using Versus.ViewModels;

namespace Versus.Interfaces;

public interface IServicioCompetencia
{
    Task<IEnumerable<CompetenciaViewModel>> ObtenerTodosAsync();
    Task<CompetenciaViewModel?> ObtenerPorIdAsync(int id);
    Task CrearAsync(CompetenciaViewModel viewModel);
    Task ActualizarAsync(CompetenciaViewModel viewModel);
    Task EliminarAsync(int id);
}
