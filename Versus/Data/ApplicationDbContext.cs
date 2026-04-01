using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Versus.Models;

namespace Versus.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Competidor> Competidores { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Competencia> Competencias { get; set; }
    public DbSet<Llave> Llaves { get; set; }
    public DbSet<Combate> Combates { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Combate>()
            .HasOne(c => c.Competidor1)
            .WithMany(cp => cp.CombatesComoCompetidor1)
            .HasForeignKey(c => c.Competidor1Id)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Combate>()
            .HasOne(c => c.Competidor2)
            .WithMany(cp => cp.CombatesComoCompetidor2)
            .HasForeignKey(c => c.Competidor2Id)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Combate>()
            .HasOne(c => c.Ganador)
            .WithMany()
            .HasForeignKey(c => c.GanadorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
