using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ERecrutement.Data;
using ERecrutement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;

namespace ERecrutement.Controllers
{
    [Authorize(Roles = "Recruteur")]
    public class RecruteursController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public RecruteursController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult AjouterRecruteur()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var recruteur = new Recruteur
            {
                Nom = "Mon Nom",
                Entreprise = "Mon Entreprise",
                UserId = userId
            };

            _context.Recruteurs.Add(recruteur);
            _context.SaveChanges();

            return Content("✅ Recruteur ajouté avec succès !");
        }
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Offre offre)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Console.WriteLine($"🔍 Création d'une offre par UserID : {userId}");

            var recruteur = _context.Recruteurs.FirstOrDefault(r => r.UserId == userId);
            if (recruteur == null)
            {
                Console.WriteLine("❌ Recruteur non trouvé !");
                return NotFound();
            }

            // Vérification des données de l'offre
            Console.WriteLine($"📌 Offre reçue : Poste = {offre.Poste}, TypeContrat = {offre.TypeContrat}, Remuneration = {offre.Remuneration}");

            offre.RecruteurId = recruteur.Id;
            _context.Offres.Add(offre);
            _context.SaveChanges();

            Console.WriteLine("✅ Offre créée avec succès !");

            return RedirectToAction("MesOffres");
        }

        public IActionResult MesOffres()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Console.WriteLine($"🔍 Chargement des offres pour UserID : {userId}");

            var recruteur = _context.Recruteurs.FirstOrDefault(r => r.UserId == userId);

            if (recruteur == null)
            {
                Console.WriteLine("❌ Recruteur introuvable !");
                return NotFound();
            }

            Console.WriteLine($"✅ Recruteur trouvé : ID = {recruteur.Id}");

            var offres = _context.Offres.Where(o => o.RecruteurId == recruteur.Id).ToList();
            Console.WriteLine($"📌 Nombre d'offres trouvées : {offres.Count}");

            return View(offres);
        }


        public IActionResult Edit(int id)
        {
            var offre = _context.Offres.Find(id);
            if (offre == null)
            {
                return NotFound();
            }
            return View(offre);
        }

        [HttpPost]
        public IActionResult Edit(int id, Offre offre)
        {
            if (id != offre.Id)
            {
                return BadRequest();
            }

            var existingOffre = _context.Offres.Find(id);
            if (existingOffre == null)
            {
                return NotFound();
            }

            existingOffre.TypeContrat = offre.TypeContrat;
            existingOffre.Secteur = offre.Secteur;
            existingOffre.Profil = offre.Profil;
            existingOffre.Poste = offre.Poste;
            existingOffre.Remuneration = offre.Remuneration;

            _context.SaveChanges();
            return RedirectToAction("MesOffres");
        }

        public IActionResult Delete(int id)
        {
            var offre = _context.Offres.Find(id);
            if (offre == null)
            {
                return NotFound();
            }

            return View(offre);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var offre = _context.Offres.Find(id);
            if (offre == null)
            {
                return NotFound();
            }

            _context.Offres.Remove(offre);
            _context.SaveChanges();

            return RedirectToAction("MesOffres");
        }

        public IActionResult Supprimer(int id)
        {
            var candidature = _context.Candidatures.Find(id);
            if (candidature == null)
                return NotFound("Candidature non trouvée !");

            _context.Candidatures.Remove(candidature);
            _context.SaveChanges();

            return RedirectToAction("Historique");
        }


    }
}
