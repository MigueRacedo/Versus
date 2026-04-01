using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Versus.Interfaces;
using Versus.ViewModels;

namespace Versus.Controllers;

[Authorize]
public class CategoriasController : Controller
{
    private readonly IServicioCategoria _servicioCategoria;
    private readonly IServicioCompetidor _servicioCompetidor;

    public CategoriasController(IServicioCategoria servicioCategoria, IServicioCompetidor servicioCompetidor)
    {
        _servicioCategoria = servicioCategoria;
        _servicioCompetidor = servicioCompetidor;
    }

    public async Task<IActionResult> Index()
    {
        var categorias = await _servicioCategoria.ObtenerTodosAsync();
        return View(categorias);
    }

    public async Task<IActionResult> Details(int id)
    {
        var categoria = await _servicioCategoria.ObtenerPorIdAsync(id);
        if (categoria == null) return NotFound();
        var competidores = await _servicioCompetidor.ObtenerPorCategoriaAsync(id);
        ViewBag.Competidores = competidores;
        return View(categoria);
    }

    public IActionResult Create() => View(new CategoriaViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CategoriaViewModel viewModel)
    {
        if (!ModelState.IsValid) return View(viewModel);
        await _servicioCategoria.CrearAsync(viewModel);
        TempData["Exito"] = "Categoría creada correctamente.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var categoria = await _servicioCategoria.ObtenerPorIdAsync(id);
        if (categoria == null) return NotFound();
        return View(categoria);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CategoriaViewModel viewModel)
    {
        if (id != viewModel.Id) return BadRequest();
        if (!ModelState.IsValid) return View(viewModel);
        await _servicioCategoria.ActualizarAsync(viewModel);
        TempData["Exito"] = "Categoría actualizada correctamente.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var categoria = await _servicioCategoria.ObtenerPorIdAsync(id);
        if (categoria == null) return NotFound();
        return View(categoria);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _servicioCategoria.EliminarAsync(id);
        TempData["Exito"] = "Categoría eliminada correctamente.";
        return RedirectToAction(nameof(Index));
    }
}
