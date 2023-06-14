using GabionCalculator.DAL.Entities.Common.Interfaces;
using GabionCalculator.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection;

namespace GabionCalculator.DAL.Data
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Gabion> Gabions { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<CostWork> CostWorks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new CostWorkSeedConfiguration());
            SetNullBehaviour(builder);
        }

        private static void SetNullBehaviour(ModelBuilder builder)
        {
            builder.Entity<Gabion>()
             .HasOne(b => b.Material)
             .WithMany(a => a.Gabions)
             .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Gabion>()
             .HasOne(b => b.User)
             .WithMany(a => a.Gabions)
             .OnDelete(DeleteBehavior.SetNull);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            foreach (var entry in ChangeTracker.Entries<IDateFixEntity>())
            {
                entry.Entity.DateStart = entry.State switch
                {
                    EntityState.Added => DateTime.Now,
                    _ => entry.Entity.DateStart
                };
                entry.Entity.DateUpdate = entry.State switch
                {
                    EntityState.Modified => DateTime.Now,
                    _ => entry.Entity.DateStart
                };
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
