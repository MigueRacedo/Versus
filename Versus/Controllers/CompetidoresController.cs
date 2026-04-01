using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Versus.Interfaces;
using Versus.ViewModels;

namespace Versus.Controllers;

[Authorize]
public class CompetidoresController : Controller
{
    private readonly IServicioCompetidor _servicioCompetidor;
    private readonly IServicioCategoria _servicioCategoria;

    public CompetidoresController(IServicioCompetidor servicioCompetidor, IServicioCategoria servicioCategoria)
    {
        _servicioCompetidor = servicioCompetidor;
        _servicioCategoria = servicioCategoria;
    }

    public async Task<IActionResult> Index()
    {
        var competidores = await _servicioCompetidor.ObtenerTodosAsync();
        return View(competidores);
    }

    public async Task<IActionResult> Details(int id)
    {
        var competidor = await _servicioCompetidor.ObtenerPorIdAsync(id);
        if (competidor == null) return NotFound();
        await CargarCategoriasAsync();
        return View(competidor);
    }

    public async Task<IActionResult> Create()
    {
        await CargarCategoriasAsync();
        return View(new CompetidorViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CompetidorViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            await CargarCategoriasAsync();
            return View(viewModel);
        }
        await _servicioCompetidor.CrearAsync(viewModel);
        TempData["Exito"] = "Competidor creado correctamente.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var competidor = await _servicioCompetidor.ObtenerPorIdAsync(id);
        if (competidor == null) return NotFound();
        await CargarCategoriasAsync();
        return View(competidor);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CompetidorViewModel viewModel)
    {
        if (id != viewModel.Id) return BadRequest();
        if (!ModelState.IsValid)
        {
            await CargarCategoriasAsync();
            return View(viewModel);
        }
        await _servicioCompetidor.ActualizarAsync(viewModel);
        TempData["Exito"] = "Competidor actualizado correctamente.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var competidor = await _servicioCompetidor.ObtenerPorIdAsync(id);
        if (competidor == null) return NotFound();
        return View(competidor);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _servicioCompetidor.EliminarAsync(id);
        TempData["Exito"] = "Competidor eliminado correctamente.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AssignCategory(int competidorId, int? categoriaId)
    {
        await _servicioCompetidor.AsignarCategoriaAsync(competidorId, categoriaId);
        TempData["Exito"] = "Categoría asignada correctamente.";
        return RedirectToAction(nameof(Details), new { id = competidorId });
    }

    private async Task CargarCategoriasAsync()
    {
        var categorias = await _servicioCategoria.ObtenerTodosAsync();
        ViewBag.Categories = new SelectList(categorias, "Id", "Nombre");
    }
}
