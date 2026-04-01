using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Versus.Interfaces;
using Versus.Models;

namespace Versus.Controllers;

public class InicioController : Controller
{
    private readonly ILogger<InicioController> _logger;
    private readonly IServicioCompetidor _servicioCompetidor;
    private readonly IServicioCategoria _servicioCategoria;
    private readonly IServicioCompetencia _servicioCompetencia;

    public InicioController(
        ILogger<InicioController> logger,
        IServicioCompetidor servicioCompetidor,
        IServicioCategoria servicioCategoria,
        IServicioCompetencia servicioCompetencia)
    {
        _logger = logger;
        _servicioCompetidor = servicioCompetidor;
        _servicioCategoria = servicioCategoria;
        _servicioCompetencia = servicioCompetencia;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.TotalCompetidores = (await _servicioCompetidor.ObtenerTodosAsync()).Count();
        ViewBag.TotalCategorias = (await _servicioCategoria.ObtenerTodosAsync()).Count();
        ViewBag.TotalCompetencias = (await _servicioCompetencia.ObtenerTodosAsync()).Count();
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
