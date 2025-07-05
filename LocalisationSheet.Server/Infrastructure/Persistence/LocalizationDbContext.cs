using LocalisationSheet.Server.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LocalisationSheet.Server.Infrastructure.Data
{
    public class LocalizationDbContext : DbContext
    {
        public LocalizationDbContext(DbContextOptions<LocalizationDbContext> options)
            : base(options)
        { }

        public DbSet<Language> Languages { get; set; } = null!;
        public DbSet<Key> Keys { get; set; } = null!;
        public DbSet<Translation> Translations { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Language>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(5);
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });
            modelBuilder.Entity<Key>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.HasIndex(e => e.Name)
                    .IsUnique();
            });
            modelBuilder.Entity<Translation>(entity =>
            {
                entity.HasKey(e => new { e.KeyId, e.LanguageId });

                entity.Property(e => e.Value)
                .IsRequired(false)
                .HasMaxLength(1000);

                entity.HasOne(e => e.Key)
                    .WithMany(k => k.Translations)
                    .HasForeignKey(e => e.KeyId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Language)
                    .WithMany(l => l.Translations)
                    .HasForeignKey(e => e.LanguageId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}