using Microsoft.EntityFrameworkCore;

using BimaTech.Parking.Models;

namespace BimaTech.Parking.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    // Ajouts pour la gestion des véhicules
    public DbSet<Entreprise> Entreprises => Set<Entreprise>();
    public DbSet<Employe> Employes => Set<Employe>();
    public DbSet<Vehicule> Vehicules => Set<Vehicule>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(u => u.Email).IsUnique();
            entity.Property(u => u.Email).IsRequired();
            entity.Property(u => u.PasswordHash).IsRequired();
        });

        // Ajouts pour Vehicule / Employe
        modelBuilder.Entity<Vehicule>(entity =>
        {
            entity.HasIndex(v => v.Immatriculation).IsUnique();

            entity.HasOne(v => v.Employe)
                .WithMany(e => e.Vehicules)
                .HasForeignKey(v => v.EmployeId);
        });

     modelBuilder.Entity<Employe>(entity =>
{
    entity.HasOne(e => e.Entreprise)
        .WithMany(ent => ent.Employes)   // au lieu de .WithMany()
        .HasForeignKey(e => e.EntrepriseId);
});
    }
}