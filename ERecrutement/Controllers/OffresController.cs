using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ERecrutement.Data;
using ERecrutement.Models;

[Authorize(Roles = "Recruteur")]
public class OffresController : Controller
{
    private readonly ApplicationDbContext _context;

    public OffresController(ApplicationDbContext context)
    {
        _context = context;
    }

    // ✅ LISTE DES OFFRES (Page principale)
    public async Task<IActionResult> Index()
    {
        var offres = await _context.Offres.ToListAsync();
        Console.WriteLine($"📌 Nombre d'offres récupérées : {offres.Count}");

        return View(offres);
    }


    // ✅ PAGE CRÉATION OFFRE
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // ✅ ENREGISTRER UNE OFFRE (POST)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Offre offre)
    {
        if (ModelState.IsValid)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var recruteur = await _context.Recruteurs.FirstOrDefaultAsync(r => r.UserId == userId);

            if (recruteur == null)
            {
                ModelState.AddModelError("", "Erreur : Impossible de trouver le recruteur.");
                return View(offre);
            }

            offre.RecruteurId = recruteur.Id;
            _context.Offres.Add(offre);
            await _context.SaveChangesAsync();

            return RedirectToAction("MesOffres", "Recruteurs");
        }
        return View(offre);
    }


    public IActionResult Edit(int id)
    {
        var offre = _context.Offres.Find(id);
        if (offre == null) return NotFound();

        return View(offre);
    }

    [HttpPost]
    public IActionResult Edit(Offre offre)
    {
        _context.Offres.Update(offre);
        _context.SaveChanges();

        return RedirectToAction("MesOffres");
    }


    // ✅ SUPPRIMER UNE OFFRE (GET)
    public IActionResult Delete(int id)
    {
        var offre = _context.Offres.Find(id);
        if (offre == null) return NotFound();

        _context.Offres.Remove(offre);
        _context.SaveChanges();

        return RedirectToAction("MesOffres");
    }


    // ✅ SUPPRIMER UNE OFFRE (POST)
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var offre = await _context.Offres.FindAsync(id);
        if (offre == null)
            return NotFound();

        _context.Offres.Remove(offre);
        await _context.SaveChangesAsync();
        return RedirectToAction("MesOffres", "Recruteurs");
    }

}
