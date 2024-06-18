using Microsoft.EntityFrameworkCore;
using PropertyInventorySystem.Models;
using System;

namespace PropertyInventorySystem.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Property> Properties { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<SoldProperty> SoldProperties { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define composite primary key for SoldProperty
            modelBuilder.Entity<SoldProperty>()
                .HasKey(sp => new { sp.PropertyId, sp.ContactId });

            // Declare GUIDs for seeding
            var contact1Id = Guid.NewGuid();
            var contact2Id = Guid.NewGuid();
            var property1Id = Guid.NewGuid();
            var property2Id = Guid.NewGuid();

            // Seed data for Contacts
            modelBuilder.Entity<Contact>().HasData(
                new Contact
                {
                    Id = contact1Id,
                    FirstName = "John",
                    LastName = "Doe",
                    PhoneNumber = "123456789",
                    Email = "john.doe@example.com"
                },
                new Contact
                {
                    Id = contact2Id,
                    FirstName = "Jane",
                    LastName = "Smith",
                    PhoneNumber = "987654321",
                    Email = "jane.smith@example.com"
                }
            );

            // Seed data for Properties
            modelBuilder.Entity<Property>().HasData(
                new Property
                {
                    Id = property1Id,
                    Name = "Maisonette",
                    Address = "123 Main St",
                    Price = 130000m,
                    DateOfRegistration = DateTime.Now
                },
                new Property
                {
                    Id = property2Id,
                    Name = "Penthouse",
                    Address = "456 Elm St",
                    Price = 430000m,
                    DateOfRegistration = DateTime.Now
                }
            );

            // Seed data for SoldProperties
            modelBuilder.Entity<SoldProperty>().HasData(
                new SoldProperty
                {
                    PropertyId = property1Id,
                    ContactId = contact1Id,
                    AcquisitionPrice = 120000m,
                    DateOfRegistration = DateTime.Now.AddDays(-30)
                },
                new SoldProperty
                {
                    PropertyId = property2Id,
                    ContactId = contact2Id,
                    AcquisitionPrice = 420000m,
                    DateOfRegistration = DateTime.Now.AddDays(-15)
                }
            );

            // Define the one-to-many relationship between Property and SoldProperty
            modelBuilder.Entity<SoldProperty>()
                .HasOne(sp => sp.Property)
                .WithMany(p => p.SoldProperties)
                .HasForeignKey(sp => sp.PropertyId);

            // Define the one-to-many relationship between Contact and SoldProperty
            modelBuilder.Entity<SoldProperty>()
                .HasOne(sp => sp.Contact)
                .WithMany()
                .HasForeignKey(sp => sp.ContactId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
