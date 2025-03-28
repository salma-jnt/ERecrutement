namespace ERecrutement.Models
{
    public class Candidat
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public int Age { get; set; }
        public string Titre { get; set; }
        public string Diplome { get; set; }
        public int NombreAnneeExperience { get; set; }
        public string CV { get; set; }

        public string UserId { get; set; }  // Lien avec le compte utilisateur
        public ApplicationUser User { get; set; }

        public List<Candidature> Candidatures { get; set; }
    }
}
