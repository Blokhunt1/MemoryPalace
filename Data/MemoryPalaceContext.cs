using Microsoft.EntityFrameworkCore;
using MemoryPalaceApp.Models;

namespace MemoryPalaceApp.Data;

public class MemoryPalaceContext : DbContext
{
    public MemoryPalaceContext(DbContextOptions<MemoryPalaceContext> options) : base(options)
    {
    }

    public DbSet<MemoryPalace> MemoryPalaces { get; set; }
    public DbSet<Loci> Loci { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MemoryPalace>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Subject).HasMaxLength(200);
        });

        modelBuilder.Entity<Loci>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.MemoryPalace)
                  .WithMany(e => e.PointsOfLoci)
                  .HasForeignKey(e => e.MemoryPalaceId)
                  .OnDelete(DeleteBehavior.Cascade);
            
            entity.Property(e => e.Category).HasMaxLength(100);
            entity.Property(e => e.Location).HasMaxLength(500);
            entity.Property(e => e.ImagePath).HasMaxLength(500);
            entity.Property(e => e.WrittenKey).HasMaxLength(500);
            entity.Property(e => e.ImageKey).HasMaxLength(500);
            entity.Property(e => e.WrittenValue).HasMaxLength(1000);
            entity.Property(e => e.ImageValue).HasMaxLength(500);
        });

        base.OnModelCreating(modelBuilder);
    }
}