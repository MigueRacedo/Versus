using Versus.Interfaces;
using Versus.Models;
using Versus.ViewModels;

namespace Versus.Services;

public class ServicioCompetidor : IServicioCompetidor
{
    private readonly IRepositorioCompetidor _repositorio;

    public ServicioCompetidor(IRepositorioCompetidor repositorio)
    {
        _repositorio = repositorio;
    }

    public async Task<IEnumerable<CompetidorViewModel>> ObtenerTodosAsync()
    {
        var competidores = await _repositorio.ObtenerTodosAsync();
        return competidores.Select(MapearAViewModel);
    }

    public async Task<CompetidorViewModel?> ObtenerPorIdAsync(int id)
    {
        var competidor = await _repositorio.ObtenerPorIdAsync(id);
        return competidor == null ? null : MapearAViewModel(competidor);
    }

    public async Task CrearAsync(CompetidorViewModel vm)
    {
        var competidor = new Competidor
        {
            Nombre = vm.Nombre,
            Edad = vm.Edad,
            Peso = vm.Peso,
            CategoriaId = vm.CategoriaId
        };
        await _repositorio.AgregarAsync(competidor);
    }

    public async Task ActualizarAsync(CompetidorViewModel vm)
    {
        var competidor = await _repositorio.ObtenerPorIdAsync(vm.Id);
        if (competidor == null) return;
        competidor.Nombre = vm.Nombre;
        competidor.Edad = vm.Edad;
        competidor.Peso = vm.Peso;
        competidor.CategoriaId = vm.CategoriaId;
        await _repositorio.ActualizarAsync(competidor);
    }

    public async Task EliminarAsync(int id) => await _repositorio.EliminarAsync(id);

    public async Task AsignarCategoriaAsync(int competidorId, int? categoriaId)
    {
        var competidor = await _repositorio.ObtenerPorIdAsync(competidorId);
        if (competidor == null) return;
        competidor.CategoriaId = categoriaId;
        await _repositorio.ActualizarAsync(competidor);
    }

    public async Task<IEnumerable<CompetidorViewModel>> ObtenerPorCategoriaAsync(int categoriaId)
    {
        var competidores = await _repositorio.ObtenerPorCategoriaAsync(categoriaId);
        return competidores.Select(MapearAViewModel);
    }

    private static CompetidorViewModel MapearAViewModel(Competidor c) => new()
    {
        Id = c.Id,
        Nombre = c.Nombre,
        Edad = c.Edad,
        Peso = c.Peso,
        CategoriaId = c.CategoriaId,
        NombreCategoria = c.Categoria?.Nombre
    };
}
