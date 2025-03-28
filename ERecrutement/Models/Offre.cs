using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace ERecrutement.Models
{
    public class Offre
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string TypeContrat { get; set; }

        [Required]
        public string Secteur { get; set; }

        [Required]
        public string Profil { get; set; }

        [Required]
        public string Poste { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")] // Assure que la BD accepte la valeur sans erreur de précision
        public decimal Remuneration { get; set; }

        // Clé étrangère vers Recruteur
        [Required]
        [ForeignKey("Recruteur")]
        public int RecruteurId { get; set; }

        public Recruteur Recruteur { get; set; }

        // Initialisation de la liste pour éviter des erreurs NullReferenceException
        public List<Candidature> Candidatures { get; set; } = new List<Candidature>();
    }
}
