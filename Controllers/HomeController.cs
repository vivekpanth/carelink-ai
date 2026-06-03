using CareLink.Data;
using CareLink.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CareLink.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _db;
    public HomeController(ApplicationDbContext db) => _db = db;

    public async Task<IActionResult> Index()
    {
        ViewBag.ServiceCount = await _db.Services.CountAsync();
        ViewBag.Categories  = await _db.Services.Select(s => s.Category).Distinct().CountAsync();
        ViewBag.FreeCount   = await _db.Services.CountAsync(s => s.IsFree);
        ViewBag.Featured    = await _db.Services.Take(3).ToListAsync();
        return View();
    }

    public IActionResult Privacy() => View();

    [ResponseCache(Duration=0, Location=ResponseCacheLocation.None, NoStore=true)]
    public IActionResult Error() =>
        View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
