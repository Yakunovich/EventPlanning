using Microsoft.EntityFrameworkCore;
using EventPlanningBackend.Models;

namespace EventPlanningBackend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<AdditionalField> AdditionalFields { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Configurations.EventConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.AccountConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.RegistrationConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.AdditionalFieldConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
