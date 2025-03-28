using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ERecrutement.Data;
using ERecrutement.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ERecrutement.Controllers
{
    [Authorize(Roles = "Candidat")]
    public class CandidatsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CandidatsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // ✅ Ajouter un candidat
        public IActionResult AjouterCandidat()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var candidatExist = _context.Candidats.Any(c => c.UserId == userId);

            if (!candidatExist)
            {
                var candidat = new Candidat
                {
                    Nom = "Nom du Candidat",
                    Prenom = "Prénom du Candidat",
                    Age = 25,
                    Titre = "Ingénieur",
                    Diplome = "Master",
                    NombreAnneeExperience = 3,
                    CV = "cv.pdf",
                    UserId = userId
                };

                _context.Candidats.Add(candidat);
                _context.SaveChanges();
            }

            return RedirectToAction("Offres");
        }

        // ✅ Voir les offres disponibles
        public IActionResult Offres()
        {
            var offres = _context.Offres.ToList();
            return View(offres);
        }

        // ✅ Postuler à une offre
        public IActionResult Postuler(int offreId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var candidat = _context.Candidats.FirstOrDefault(c => c.UserId == userId);

            if (candidat == null) return NotFound("Candidat non trouvé !");

            var candidature = new Candidature
            {
                OffreId = offreId,
                CandidatId = candidat.Id
            };

            _context.Candidatures.Add(candidature);
            _context.SaveChanges();

            return RedirectToAction("Historique");
        }

        public IActionResult Historique()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var candidat = _context.Candidats.FirstOrDefault(c => c.UserId == userId);

            if (candidat == null)
            {
                return Content("Candidat non trouvé !");
            }

            var candidatures = _context.Candidatures
                .Include(c => c.Offre)  // 🔥 Ajout du Include pour charger l'offre associée
                .Where(c => c.CandidatId == candidat.Id)
                .ToList();

            return View(candidatures);
        }


        // ✅ Annuler une candidature
        public IActionResult Annuler(int id)
        {
            var candidature = _context.Candidatures.Find(id);
            if (candidature == null) return NotFound();

            _context.Candidatures.Remove(candidature);
            _context.SaveChanges();

            return RedirectToAction("Historique");
        }
    }
}
