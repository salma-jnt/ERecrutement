using Microsoft.AspNetCore.Identity;

namespace ERecrutement.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Role { get; set; } = "Candidat"; // ✅ Valeur par défaut pour éviter NULL
    }
}
