using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ERecrutement.Models;

namespace ERecrutement.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegisterModel(UserManager<ApplicationUser> userManager,
                             SignInManager<ApplicationUser> signInManager,
                             RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel(); // Ajoute Input

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "Les mots de passe ne correspondent pas.")]
            public string ConfirmPassword { get; set; }

            [Required]
            public string Role { get; set; } // Rôle sélectionné (Recruteur ou Candidat)
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Veuillez remplir tous les champs correctement.";
                return Page();
            }

            // ✅ Vérifier que le rôle est bien sélectionné
            string selectedRole = string.IsNullOrEmpty(Input.Role) ? "Candidat" : Input.Role;

            var user = new ApplicationUser
            {
                UserName = Input.Email,
                Email = Input.Email,
                Role = selectedRole // ✅ On s'assure que le rôle est bien assigné
            };

            var result = await _userManager.CreateAsync(user, Input.Password);

            if (result.Succeeded)
            {
                // ✅ Vérifie que le rôle existe avant de l'assigner
                if (!await _roleManager.RoleExistsAsync(selectedRole))
                {
                    await _roleManager.CreateAsync(new IdentityRole(selectedRole));
                }

                await _userManager.AddToRoleAsync(user, selectedRole); // ✅ Assigner le rôle sélectionné

                TempData["Message"] = $"Inscription réussie en tant que {selectedRole} !"; // ✅ Message de confirmation

                return RedirectToPage("/Account/Login"); // ✅ Redirection vers connexion
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }



    }
}
