using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Versus.Interfaces;
using Versus.ViewModels;

namespace Versus.Controllers;

[Authorize]
public class CompetenciasController : Controller
{
    private readonly IServicioCompetencia _servicioCompetencia;
    private readonly IServicioCategoria _servicioCategoria;
    private readonly IServicioLlave _servicioLlave;

    public CompetenciasController(
        IServicioCompetencia servicioCompetencia,
        IServicioCategoria servicioCategoria,
        IServicioLlave servicioLlave)
    {
        _servicioCompetencia = servicioCompetencia;
        _servicioCategoria = servicioCategoria;
        _servicioLlave = servicioLlave;
    }

    public async Task<IActionResult> Index()
    {
        var competencias = await _servicioCompetencia.ObtenerTodosAsync();
        return View(competencias);
    }

    public async Task<IActionResult> Details(int id)
    {
        var competencia = await _servicioCompetencia.ObtenerPorIdAsync(id);
        if (competencia == null) return NotFound();
        var llave = await _servicioLlave.ObtenerPorCompetenciaAsync(id);
        ViewBag.Llave = llave;
        return View(competencia);
    }

    public async Task<IActionResult> Create()
    {
        await CargarCategoriasAsync();
        return View(new CompetenciaViewModel { Fecha = DateTime.Today });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CompetenciaViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            await CargarCategoriasAsync();
            return View(viewModel);
        }
        await _servicioCompetencia.CrearAsync(viewModel);
        TempData["Exito"] = "Competencia creada correctamente.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var competencia = await _servicioCompetencia.ObtenerPorIdAsync(id);
        if (competencia == null) return NotFound();
        await CargarCategoriasAsync();
        return View(competencia);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CompetenciaViewModel viewModel)
    {
        if (id != viewModel.Id) return BadRequest();
        if (!ModelState.IsValid)
        {
            await CargarCategoriasAsync();
            return View(viewModel);
        }
        await _servicioCompetencia.ActualizarAsync(viewModel);
        TempData["Exito"] = "Competencia actualizada correctamente.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var competencia = await _servicioCompetencia.ObtenerPorIdAsync(id);
        if (competencia == null) return NotFound();
        return View(competencia);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _servicioCompetencia.EliminarAsync(id);
        TempData["Exito"] = "Competencia eliminada correctamente.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> GenerateBracket(int id)
    {
        try
        {
            await _servicioLlave.GenerarAsync(id);
            TempData["Exito"] = "¡Llave generada correctamente!";
        }
        catch (InvalidOperationException ex)
        {
            TempData["Error"] = ex.Message;
        }
        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RecordResult(int matchId, int winnerId, int competitionId)
    {
        try
        {
            await _servicioLlave.RegistrarResultadoAsync(matchId, winnerId);
            TempData["Exito"] = "Resultado registrado correctamente.";
        }
        catch (InvalidOperationException ex)
        {
            TempData["Error"] = ex.Message;
        }
        return RedirectToAction(nameof(Details), new { id = competitionId });
    }

    private async Task CargarCategoriasAsync()
    {
        var categorias = await _servicioCategoria.ObtenerTodosAsync();
        ViewBag.Categories = new SelectList(categorias, "Id", "Nombre");
    }
}
