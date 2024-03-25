﻿// <auto-generated />
using System;
using HomeAccounting.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HomeAccounting.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240323145424_AddAdminDataInDatabase")]
    partial class AddAdminDataInDatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("HomeAccounting.Domain.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("HomeAccounting.Domain.Entities.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<int>("ClientId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TransactionType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ClientId");

                    b.ToTable("Transactions");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("HomeAccounting.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("HomeAccounting.Domain.Entities.ExistingTransaction", b =>
                {
                    b.HasBaseType("HomeAccounting.Domain.Entities.Transaction");

                    b.ToTable("ExistingTransaction", (string)null);
                });

            modelBuilder.Entity("HomeAccounting.Domain.Entities.PlanningTransaction", b =>
                {
                    b.HasBaseType("HomeAccounting.Domain.Entities.Transaction");

                    b.Property<bool>("IsDone")
                        .HasColumnType("boolean");

                    b.ToTable("PlanningTransaction", (string)null);
                });

            modelBuilder.Entity("HomeAccounting.Domain.Entities.Admin", b =>
                {
                    b.HasBaseType("HomeAccounting.Domain.Entities.User");

                    b.ToTable("Admins", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "xolmatovabdurahim1987@gmail.com",
                            FullName = "Xolmatov Raximjon",
                            Gender = 1,
                            PasswordHash = "WZRHGrsBESr8wYFZ9sx0tPURuZgG2lmzyvWpwXPKz8U=",
                            PhoneNumber = "994779050",
                            UserName = "Master"
                        });
                });

            modelBuilder.Entity("HomeAccounting.Domain.Entities.Client", b =>
                {
                    b.HasBaseType("HomeAccounting.Domain.Entities.User");

                    b.ToTable("Clients", (string)null);
                });

            modelBuilder.Entity("HomeAccounting.Domain.Entities.Transaction", b =>
                {
                    b.HasOne("HomeAccounting.Domain.Entities.Category", "Category")
                        .WithMany("Transactions")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HomeAccounting.Domain.Entities.Client", "Client")
                        .WithMany("Transactions")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("HomeAccounting.Domain.Entities.ExistingTransaction", b =>
                {
                    b.HasOne("HomeAccounting.Domain.Entities.Transaction", null)
                        .WithOne()
                        .HasForeignKey("HomeAccounting.Domain.Entities.ExistingTransaction", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HomeAccounting.Domain.Entities.PlanningTransaction", b =>
                {
                    b.HasOne("HomeAccounting.Domain.Entities.Transaction", null)
                        .WithOne()
                        .HasForeignKey("HomeAccounting.Domain.Entities.PlanningTransaction", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HomeAccounting.Domain.Entities.Admin", b =>
                {
                    b.HasOne("HomeAccounting.Domain.Entities.User", null)
                        .WithOne()
                        .HasForeignKey("HomeAccounting.Domain.Entities.Admin", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HomeAccounting.Domain.Entities.Client", b =>
                {
                    b.HasOne("HomeAccounting.Domain.Entities.User", null)
                        .WithOne()
                        .HasForeignKey("HomeAccounting.Domain.Entities.Client", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HomeAccounting.Domain.Entities.Category", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("HomeAccounting.Domain.Entities.Client", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
