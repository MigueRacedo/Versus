using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Versus.Interfaces;
using Versus.ViewModels;

namespace Versus.Controllers;

[Authorize]
public class CategoriesController : Controller
{
    private readonly ICategoryService _categoryService;
    private readonly ICompetitorService _competitorService;

    public CategoriesController(ICategoryService categoryService, ICompetitorService competitorService)
    {
        _categoryService = categoryService;
        _competitorService = competitorService;
    }

    public async Task<IActionResult> Index()
    {
        var categories = await _categoryService.GetAllAsync();
        return View(categories);
    }

    public async Task<IActionResult> Details(int id)
    {
        var category = await _categoryService.GetByIdAsync(id);
        if (category == null) return NotFound();
        var competitors = await _competitorService.GetByCategoryAsync(id);
        ViewBag.Competitors = competitors;
        return View(category);
    }

    public IActionResult Create() => View(new CategoryViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CategoryViewModel viewModel)
    {
        if (!ModelState.IsValid) return View(viewModel);
        await _categoryService.CreateAsync(viewModel);
        TempData["Success"] = "Category created successfully.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var category = await _categoryService.GetByIdAsync(id);
        if (category == null) return NotFound();
        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CategoryViewModel viewModel)
    {
        if (id != viewModel.Id) return BadRequest();
        if (!ModelState.IsValid) return View(viewModel);
        await _categoryService.UpdateAsync(viewModel);
        TempData["Success"] = "Category updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var category = await _categoryService.GetByIdAsync(id);
        if (category == null) return NotFound();
        return View(category);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _categoryService.DeleteAsync(id);
        TempData["Success"] = "Category deleted successfully.";
        return RedirectToAction(nameof(Index));
    }
}
