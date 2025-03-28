using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERecrutement.Models
{
    public class Recruteur
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nom { get; set; }

        [Required]
        public string Entreprise { get; set; }

        // Clé étrangère vers ApplicationUser
        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        // Initialisation de la liste pour éviter des erreurs NullReferenceException
        public List<Offre> Offres { get; set; } = new List<Offre>();
    }
}
