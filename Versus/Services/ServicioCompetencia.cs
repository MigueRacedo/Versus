using Versus.Interfaces;
using Versus.Models;
using Versus.ViewModels;

namespace Versus.Services;

public class ServicioCompetencia : IServicioCompetencia
{
    private readonly IRepositorioCompetencia _repositorio;

    public ServicioCompetencia(IRepositorioCompetencia repositorio)
    {
        _repositorio = repositorio;
    }

    public async Task<IEnumerable<CompetenciaViewModel>> ObtenerTodosAsync()
    {
        var competencias = await _repositorio.ObtenerTodosAsync();
        return competencias.Select(MapearAViewModel);
    }

    public async Task<CompetenciaViewModel?> ObtenerPorIdAsync(int id)
    {
        var competencia = await _repositorio.ObtenerPorIdAsync(id);
        return competencia == null ? null : MapearAViewModel(competencia);
    }

    public async Task CrearAsync(CompetenciaViewModel vm)
    {
        var competencia = new Competencia
        {
            Nombre = vm.Nombre,
            Fecha = vm.Fecha,
            Lugar = vm.Lugar,
            CategoriaId = vm.CategoriaId
        };
        await _repositorio.AgregarAsync(competencia);
    }

    public async Task ActualizarAsync(CompetenciaViewModel vm)
    {
        var competencia = await _repositorio.ObtenerPorIdAsync(vm.Id);
        if (competencia == null) return;
        competencia.Nombre = vm.Nombre;
        competencia.Fecha = vm.Fecha;
        competencia.Lugar = vm.Lugar;
        competencia.CategoriaId = vm.CategoriaId;
        await _repositorio.ActualizarAsync(competencia);
    }

    public async Task EliminarAsync(int id) => await _repositorio.EliminarAsync(id);

    private static CompetenciaViewModel MapearAViewModel(Competencia c) => new()
    {
        Id = c.Id,
        Nombre = c.Nombre,
        Fecha = c.Fecha,
        Lugar = c.Lugar,
        CategoriaId = c.CategoriaId,
        NombreCategoria = c.Categoria?.Nombre,
        TieneLlave = c.Llaves.Any(),
        LlaveId = c.Llaves.FirstOrDefault()?.Id ?? 0
    };
}
