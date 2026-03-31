using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Versus.Interfaces;
using Versus.ViewModels;

namespace Versus.Controllers;

[Authorize]
public class CompetitorsController : Controller
{
    private readonly ICompetitorService _competitorService;
    private readonly ICategoryService _categoryService;

    public CompetitorsController(ICompetitorService competitorService, ICategoryService categoryService)
    {
        _competitorService = competitorService;
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        var competitors = await _competitorService.GetAllAsync();
        return View(competitors);
    }

    public async Task<IActionResult> Details(int id)
    {
        var competitor = await _competitorService.GetByIdAsync(id);
        if (competitor == null) return NotFound();
        await PopulateCategoriesAsync();
        return View(competitor);
    }

    public async Task<IActionResult> Create()
    {
        await PopulateCategoriesAsync();
        return View(new CompetitorViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CompetitorViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            await PopulateCategoriesAsync();
            return View(viewModel);
        }
        await _competitorService.CreateAsync(viewModel);
        TempData["Success"] = "Competitor created successfully.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var competitor = await _competitorService.GetByIdAsync(id);
        if (competitor == null) return NotFound();
        await PopulateCategoriesAsync();
        return View(competitor);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CompetitorViewModel viewModel)
    {
        if (id != viewModel.Id) return BadRequest();
        if (!ModelState.IsValid)
        {
            await PopulateCategoriesAsync();
            return View(viewModel);
        }
        await _competitorService.UpdateAsync(viewModel);
        TempData["Success"] = "Competitor updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var competitor = await _competitorService.GetByIdAsync(id);
        if (competitor == null) return NotFound();
        return View(competitor);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _competitorService.DeleteAsync(id);
        TempData["Success"] = "Competitor deleted successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AssignCategory(int competitorId, int? categoryId)
    {
        await _competitorService.AssignCategoryAsync(competitorId, categoryId);
        TempData["Success"] = "Category assigned successfully.";
        return RedirectToAction(nameof(Details), new { id = competitorId });
    }

    private async Task PopulateCategoriesAsync()
    {
        var categories = await _categoryService.GetAllAsync();
        ViewBag.Categories = new SelectList(categories, "Id", "Name");
    }
}
