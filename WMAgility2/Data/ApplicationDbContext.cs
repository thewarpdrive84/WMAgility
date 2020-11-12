using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WMAgility2.Models;

namespace WMAgility2.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Dog> Dogs { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Practice> Practices { get; set; }
        public DbSet<PracticeSkill> PracticeSkills { get; set; }
        //public DbSet<GraphPoint> GraphPoints { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Fault> Faults { get; set; }
        public DbSet<CompFault> CompFaults { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CompFault>().HasKey(f => new { f.CompId, f.FaultId });

            builder.Entity<CompFault>()
                .HasOne(cf => cf.Comp)
                .WithMany(f => f.CompFaults)
                .HasForeignKey(cf => cf.CompId);

            builder.Entity<CompFault>()
                .HasOne(cf => cf.Fault)
                .WithMany(f => f.CompFaults)
                .HasForeignKey(cf => cf.FaultId);

            builder.Entity<ApplicationUser>()
                .HasIndex(user => user.Email)
                .IsUnique(true);

            builder.Entity<Email>(em =>
            {
                em.HasNoKey();
            });
        }

        public DbSet<WMAgility2.Models.Email> Email { get; set; }
    }
}
