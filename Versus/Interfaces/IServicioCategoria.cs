using Versus.ViewModels;

namespace Versus.Interfaces;

public interface IServicioCategoria
{
    Task<IEnumerable<CategoriaViewModel>> ObtenerTodosAsync();
    Task<CategoriaViewModel?> ObtenerPorIdAsync(int id);
    Task CrearAsync(CategoriaViewModel viewModel);
    Task ActualizarAsync(CategoriaViewModel viewModel);
    Task EliminarAsync(int id);
}
