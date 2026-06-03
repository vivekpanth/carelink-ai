using CareLink.Data;
using CareLink.Models;
using CareLink.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CareLink.Controllers;

public class ServicesController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly IAIService _ai;
    public ServicesController(ApplicationDbContext db, IAIService ai) { _db=db; _ai=ai; }

    public async Task<IActionResult> Index(string? category, string? query, string? suburb)
    {
        var q = _db.Services.AsQueryable();
        if (!string.IsNullOrWhiteSpace(category)) q = q.Where(s=>s.Category==category);
        if (!string.IsNullOrWhiteSpace(suburb))   q = q.Where(s=>s.Suburb.Contains(suburb));
        if (!string.IsNullOrWhiteSpace(query))     q = q.Where(s=>s.Title.Contains(query)||s.Description.Contains(query)||s.Tags.Contains(query));
        ViewBag.Category=category; ViewBag.Query=query; ViewBag.Suburb=suburb;
        ViewBag.Categories=new[]{"Health","Mental Health","Housing","Food","Employment","Legal"};
        return View(await q.ToListAsync());
    }

    public async Task<IActionResult> Details(int id)
    {
        var s = await _db.Services.FindAsync(id);
        if (s==null) return NotFound();
        return View(s);
    }

    public IActionResult AiMatch() => View();

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> AiMatch(string needs, string? suburb)
    {
        if (string.IsNullOrWhiteSpace(needs))
        { ModelState.AddModelError("","Please describe what you need."); return View(); }

        var isCrisis = await _ai.IsCrisisAsync(needs);
        var all = await _db.Services.ToListAsync();
        var matches = await _ai.MatchServicesAsync(needs, suburb, all);

        ViewBag.Needs=needs; ViewBag.Suburb=suburb;
        ViewBag.IsCrisis=isCrisis;
        ViewBag.HasResults=matches.Any();
        return View("AiResults", matches);
    }

    [Authorize] public IActionResult Create() => View();

    [Authorize, HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Service s)
    {
        if (!ModelState.IsValid) return View(s);
        s.CreatedAt=DateTime.UtcNow;
        _db.Services.Add(s);
        await _db.SaveChangesAsync();
        TempData["Success"]="Service added successfully!";
        return RedirectToAction(nameof(Index));
    }

    [Authorize] public async Task<IActionResult> Edit(int id)
    { var s=await _db.Services.FindAsync(id); if(s==null) return NotFound(); return View(s); }

    [Authorize, HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Service s)
    {
        if (id!=s.Id) return BadRequest();
        if (!ModelState.IsValid) return View(s);
        _db.Services.Update(s);
        await _db.SaveChangesAsync();
        TempData["Success"]="Service updated!";
        return RedirectToAction(nameof(Index));
    }

    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var s=await _db.Services.FindAsync(id);
        if(s!=null){_db.Services.Remove(s);await _db.SaveChangesAsync();}
        TempData["Success"]="Service deleted.";
        return RedirectToAction(nameof(Index));
    }
}
