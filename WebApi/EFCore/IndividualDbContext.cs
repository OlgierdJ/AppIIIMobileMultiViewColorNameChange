
using Microsoft.EntityFrameworkCore;
using CoreLib.Models;

namespace TodoWebApi.EFCore
{
    public class IndividualDbContext : DbContext
    {
        public IndividualDbContext(DbContextOptions<IndividualDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Individual>  Individuals { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Individual>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd().IsRequired();

                entity.Property(e => e.Color).IsRequired();
                entity.Property(e => e.Relative).HasConversion<int>().IsRequired();
                entity.Property(e => e.Name).IsRequired(true);
                entity.HasIndex(e => e.Name).IsUnique();
            });
        }
    }
}
