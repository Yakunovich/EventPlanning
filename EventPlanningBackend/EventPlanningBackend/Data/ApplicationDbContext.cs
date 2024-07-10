using Microsoft.EntityFrameworkCore;
using EventPlanningBackend.Models;

namespace EventPlanningBackend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<EventAdditionalField> EventAdditionalField { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Configurations.EventConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.AccountConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.RegistrationConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.EventAdditionalFieldConfiguration());

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Event>().HasData(
            new Event
            {
                Id = 1,
                Name = "Sport Event",
                Location = "Stadium",
                Date = new DateTime(2024, 7, 10),
                Theme = "Sport"
            },
            new Event
            {
                Id = 2,
                Name = "Music Concert",
                Location = "Concert Hall",
                Date = new DateTime(2024, 8, 15),
                Theme = "Music"
            },
            new Event
            {
                Id = 3,
                Name = "Tech Conference",
                Location = "Convention Center",
                Date = new DateTime(2024, 9, 20),
                Theme = "Technology"
            },
            new Event
            {
                Id = 4,
                Name = "Art Expo",
                Location = "Art Gallery",
                Date = new DateTime(2024, 10, 5),
                Theme = "Art"
            }
        );

            modelBuilder.Entity<Account>().HasData(
                new Account
                {
                    Id = 1,
                    Email = "user1@example.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("password"),
                    FullName = "John Doe",
                    Role = "User",
                    IsEmailConfirmed = true,
                    ConfirmationToken = "token"
                },
                new Account
                {
                    Id = 2,
                    Email = "user2@example.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("password"),
                    FullName = "Jane Smith",
                    Role = "User",
                    IsEmailConfirmed = true,
                    ConfirmationToken = "token"
                },
                new Account
                {
                    Id = 3,
                    Email = "Manager@example.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("password"),
                    FullName = "Manager One",
                    Role = "Manager",
                    IsEmailConfirmed = true,
                    ConfirmationToken = "token"
                }
            );

            modelBuilder.Entity<EventAdditionalField>().HasData(
                new EventAdditionalField { Id = 1, EventId = 1, FieldName = "Topic", FieldValue = "Sports" },
                new EventAdditionalField { Id = 2, EventId = 1, FieldName = "Participants", FieldValue = "Athletes" },
                new EventAdditionalField { Id = 3, EventId = 2, FieldName = "Genre", FieldValue = "Rock" },
                new EventAdditionalField { Id = 4, EventId = 2, FieldName = "Dress Code", FieldValue = "Casual" },
                new EventAdditionalField { Id = 5, EventId = 3, FieldName = "Focus Area", FieldValue = "AI & ML" },
                new EventAdditionalField { Id = 6, EventId = 3, FieldName = "Keynote Speaker", FieldValue = "Dr. Tech Guru" },
                new EventAdditionalField { Id = 7, EventId = 4, FieldName = "Art Styles", FieldValue = "Modern" },
                new EventAdditionalField { Id = 8, EventId = 4, FieldName = "Exhibitors", FieldValue = "Various Artists" }
            );
        }
    }
}
