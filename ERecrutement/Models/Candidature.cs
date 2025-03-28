namespace ERecrutement.Models
{
    public class Candidature
    {
        public int Id { get; set; }
        public DateTime DatePostulation { get; set; } = DateTime.Now;

        public int OffreId { get; set; }
        public Offre Offre { get; set; }

        public int CandidatId { get; set; }
        public Candidat Candidat { get; set; }
    }
}
