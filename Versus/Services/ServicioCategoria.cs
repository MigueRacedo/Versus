using Versus.Interfaces;
using Versus.Models;
using Versus.ViewModels;

namespace Versus.Services;

public class ServicioCategoria : IServicioCategoria
{
    private readonly IRepositorioCategoria _repositorio;

    public ServicioCategoria(IRepositorioCategoria repositorio)
    {
        _repositorio = repositorio;
    }

    public async Task<IEnumerable<CategoriaViewModel>> ObtenerTodosAsync()
    {
        var categorias = await _repositorio.ObtenerTodosAsync();
        return categorias.Select(MapearAViewModel);
    }

    public async Task<CategoriaViewModel?> ObtenerPorIdAsync(int id)
    {
        var categoria = await _repositorio.ObtenerPorIdAsync(id);
        return categoria == null ? null : MapearAViewModel(categoria);
    }

    public async Task CrearAsync(CategoriaViewModel vm)
    {
        var categoria = new Categoria
        {
            Nombre = vm.Nombre,
            Descripcion = vm.Descripcion,
            PesoMinimo = vm.PesoMinimo,
            PesoMaximo = vm.PesoMaximo,
            EdadMinima = vm.EdadMinima,
            EdadMaxima = vm.EdadMaxima
        };
        await _repositorio.AgregarAsync(categoria);
    }

    public async Task ActualizarAsync(CategoriaViewModel vm)
    {
        var categoria = await _repositorio.ObtenerPorIdAsync(vm.Id);
        if (categoria == null) return;
        categoria.Nombre = vm.Nombre;
        categoria.Descripcion = vm.Descripcion;
        categoria.PesoMinimo = vm.PesoMinimo;
        categoria.PesoMaximo = vm.PesoMaximo;
        categoria.EdadMinima = vm.EdadMinima;
        categoria.EdadMaxima = vm.EdadMaxima;
        await _repositorio.ActualizarAsync(categoria);
    }

    public async Task EliminarAsync(int id) => await _repositorio.EliminarAsync(id);

    private static CategoriaViewModel MapearAViewModel(Categoria c) => new()
    {
        Id = c.Id,
        Nombre = c.Nombre,
        Descripcion = c.Descripcion,
        PesoMinimo = c.PesoMinimo,
        PesoMaximo = c.PesoMaximo,
        EdadMinima = c.EdadMinima,
        EdadMaxima = c.EdadMaxima,
        CantidadCompetidores = c.Competidores.Count
    };
}
