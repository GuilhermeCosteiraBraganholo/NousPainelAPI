using Microsoft.EntityFrameworkCore;
using NousPainelAPI.Domain;

namespace NousPainelAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }

        public DbSet<Aluno> Alunos => Set<Aluno>();
        public DbSet<Checkin> Checkins => Set<Checkin>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Aluno>()
                .HasIndex(a => a.Email)
                .IsUnique();
        }
    }
}
