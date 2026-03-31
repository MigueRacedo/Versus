using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Versus.Interfaces;
using Versus.ViewModels;

namespace Versus.Controllers;

[Authorize]
public class CompetitionsController : Controller
{
    private readonly ICompetitionService _competitionService;
    private readonly ICategoryService _categoryService;
    private readonly IBracketService _bracketService;

    public CompetitionsController(
        ICompetitionService competitionService,
        ICategoryService categoryService,
        IBracketService bracketService)
    {
        _competitionService = competitionService;
        _categoryService = categoryService;
        _bracketService = bracketService;
    }

    public async Task<IActionResult> Index()
    {
        var competitions = await _competitionService.GetAllAsync();
        return View(competitions);
    }

    public async Task<IActionResult> Details(int id)
    {
        var competition = await _competitionService.GetByIdAsync(id);
        if (competition == null) return NotFound();
        var bracket = await _bracketService.GetByCompetitionAsync(id);
        ViewBag.Bracket = bracket;
        return View(competition);
    }

    public async Task<IActionResult> Create()
    {
        await PopulateCategoriesAsync();
        return View(new CompetitionViewModel { Date = DateTime.Today });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CompetitionViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            await PopulateCategoriesAsync();
            return View(viewModel);
        }
        await _competitionService.CreateAsync(viewModel);
        TempData["Success"] = "Competition created successfully.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var competition = await _competitionService.GetByIdAsync(id);
        if (competition == null) return NotFound();
        await PopulateCategoriesAsync();
        return View(competition);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CompetitionViewModel viewModel)
    {
        if (id != viewModel.Id) return BadRequest();
        if (!ModelState.IsValid)
        {
            await PopulateCategoriesAsync();
            return View(viewModel);
        }
        await _competitionService.UpdateAsync(viewModel);
        TempData["Success"] = "Competition updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var competition = await _competitionService.GetByIdAsync(id);
        if (competition == null) return NotFound();
        return View(competition);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _competitionService.DeleteAsync(id);
        TempData["Success"] = "Competition deleted successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> GenerateBracket(int id)
    {
        try
        {
            await _bracketService.GenerateAsync(id);
            TempData["Success"] = "Bracket generated successfully!";
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
            await _bracketService.RecordResultAsync(matchId, winnerId);
            TempData["Success"] = "Result recorded successfully.";
        }
        catch (InvalidOperationException ex)
        {
            TempData["Error"] = ex.Message;
        }
        return RedirectToAction(nameof(Details), new { id = competitionId });
    }

    private async Task PopulateCategoriesAsync()
    {
        var categories = await _categoryService.GetAllAsync();
        ViewBag.Categories = new SelectList(categories, "Id", "Name");
    }
}
