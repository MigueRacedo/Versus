using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Versus.Interfaces;
using Versus.Models;

namespace Versus.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICompetitorService _competitorService;
    private readonly ICategoryService _categoryService;
    private readonly ICompetitionService _competitionService;

    public HomeController(
        ILogger<HomeController> logger,
        ICompetitorService competitorService,
        ICategoryService categoryService,
        ICompetitionService competitionService)
    {
        _logger = logger;
        _competitorService = competitorService;
        _categoryService = categoryService;
        _competitionService = competitionService;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.CompetitorCount = (await _competitorService.GetAllAsync()).Count();
        ViewBag.CategoryCount = (await _categoryService.GetAllAsync()).Count();
        ViewBag.CompetitionCount = (await _competitionService.GetAllAsync()).Count();
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
