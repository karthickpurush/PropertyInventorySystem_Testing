﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PropertyInventorySystem.Data;

#nullable disable

namespace PropertyInventorySystem.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240618103555_AddSeedDataForSoldProperties")]
    partial class AddSeedDataForSoldProperties
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PropertyInventorySystem.Models.Contact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Contacts");

                    b.HasData(
                        new
                        {
                            Id = new Guid("74919c93-0a43-4a7d-a85e-04712af77d47"),
                            Email = "john.doe@example.com",
                            FirstName = "John",
                            LastName = "Doe",
                            PhoneNumber = "123456789"
                        },
                        new
                        {
                            Id = new Guid("2ec8ed47-4281-422e-9173-6c334239c9ae"),
                            Email = "jane.smith@example.com",
                            FirstName = "Jane",
                            LastName = "Smith",
                            PhoneNumber = "987654321"
                        });
                });

            modelBuilder.Entity("PropertyInventorySystem.Models.Property", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfRegistration")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Properties");

                    b.HasData(
                        new
                        {
                            Id = new Guid("0c8beea9-4db7-41bb-9e39-f5a30abc9f32"),
                            Address = "123 Main St",
                            DateOfRegistration = new DateTime(2024, 6, 18, 12, 35, 54, 587, DateTimeKind.Local).AddTicks(4574),
                            Name = "Maisonette",
                            Price = 130000m
                        },
                        new
                        {
                            Id = new Guid("4cec5cdc-830f-42f6-8a13-cd761b76b6e2"),
                            Address = "456 Elm St",
                            DateOfRegistration = new DateTime(2024, 6, 18, 12, 35, 54, 587, DateTimeKind.Local).AddTicks(4635),
                            Name = "Penthouse",
                            Price = 430000m
                        });
                });

            modelBuilder.Entity("PropertyInventorySystem.Models.SoldProperty", b =>
                {
                    b.Property<Guid>("PropertyId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnOrder(0);

                    b.Property<Guid>("ContactId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("AcquisitionPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("DateOfRegistration")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EffectiveTill")
                        .HasColumnType("datetime2");

                    b.HasKey("PropertyId", "ContactId");

                    b.HasIndex("ContactId");

                    b.ToTable("SoldProperties");

                    b.HasData(
                        new
                        {
                            PropertyId = new Guid("0c8beea9-4db7-41bb-9e39-f5a30abc9f32"),
                            ContactId = new Guid("74919c93-0a43-4a7d-a85e-04712af77d47"),
                            AcquisitionPrice = 120000m,
                            DateOfRegistration = new DateTime(2024, 5, 19, 12, 35, 54, 587, DateTimeKind.Local).AddTicks(4678)
                        },
                        new
                        {
                            PropertyId = new Guid("4cec5cdc-830f-42f6-8a13-cd761b76b6e2"),
                            ContactId = new Guid("2ec8ed47-4281-422e-9173-6c334239c9ae"),
                            AcquisitionPrice = 420000m,
                            DateOfRegistration = new DateTime(2024, 6, 3, 12, 35, 54, 587, DateTimeKind.Local).AddTicks(4684)
                        });
                });

            modelBuilder.Entity("PropertyInventorySystem.Models.SoldProperty", b =>
                {
                    b.HasOne("PropertyInventorySystem.Models.Contact", "Contact")
                        .WithMany()
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PropertyInventorySystem.Models.Property", "Property")
                        .WithMany("SoldProperties")
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contact");

                    b.Navigation("Property");
                });

            modelBuilder.Entity("PropertyInventorySystem.Models.Property", b =>
                {
                    b.Navigation("SoldProperties");
                });
#pragma warning restore 612, 618
        }
    }
}
