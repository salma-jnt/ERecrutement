using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ERecrutement.Models;

namespace ERecrutement.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Recruteur> Recruteurs { get; set; }
        public DbSet<Candidat> Candidats { get; set; }
        public DbSet<Offre> Offres { get; set; }
        public DbSet<Candidature> Candidatures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuration de la relation Recruteur - ApplicationUser (One-to-One)
            modelBuilder.Entity<Recruteur>()
                .HasOne(r => r.User)
                .WithOne()
                .HasForeignKey<Recruteur>(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuration de la relation Recruteur - Offres (One-to-Many)
            modelBuilder.Entity<Offre>()
                .HasOne(o => o.Recruteur)
                .WithMany(r => r.Offres)
                .HasForeignKey(o => o.RecruteurId)
                .OnDelete(DeleteBehavior.Cascade); // Supprime les offres si le recruteur est supprimé

            // Configuration de la relation Candidature - Candidat
            modelBuilder.Entity<Candidature>()
                .HasOne(c => c.Candidat)
                .WithMany(c => c.Candidatures)
                .HasForeignKey(c => c.CandidatId)
                .OnDelete(DeleteBehavior.Restrict); // Empêche la suppression en cascade

            // Configuration de la relation Candidature - Offre
            modelBuilder.Entity<Candidature>()
                .HasOne(c => c.Offre)
                .WithMany(o => o.Candidatures)
                .HasForeignKey(c => c.OffreId)
                .OnDelete(DeleteBehavior.Restrict); // Empêche la suppression en cascade

            // Configuration de la colonne Remuneration
            modelBuilder.Entity<Offre>()
                .Property(o => o.Remuneration)
                .HasColumnType("decimal(18,2)");

           modelBuilder.Entity<Recruteur>()
                .HasMany(r => r.Offres)
                .WithOne(o => o.Recruteur)
                .HasForeignKey(o => o.RecruteurId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
