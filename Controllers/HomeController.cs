using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Adopcion_mascotas.Models;
using Adopcion_mascotas.Helpers;

namespace Adopcion_mascotas.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [TempData]
    public string Message { get; set; }

    public IActionResult Index()
    {
        var cookieOption = new CookieOptions
        {
            Expires = DateTimeOffset.UtcNow.AddDays(7),
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict
        };
        Response.Cookies.Append("CookieDeAdopcion", "ValorDelLaCookie", cookieOption);
        return View();
    }

    public IActionResult Privacy()
    {
        var adoptante = HttpContext.Session.Get<Adoptante>("AdoptanteRegistrado");
    
        if (adoptante != null)
        {
            ViewData["Mensaje"] = $"Gracias por registrarte como adoptante, {adoptante.NombreAdoptante}. Te contactaremos a {adoptante.CorreoElectronico}.";
        }
        else if (TempData["AdoptanteNombre"] != null)
        {
            ViewData["Mensaje"] = $"Gracias por registrarte como adoptante, {TempData["AdoptanteNombre"]}.";
        }

        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
