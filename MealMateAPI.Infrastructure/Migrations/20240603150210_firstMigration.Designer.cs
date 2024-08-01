﻿// <auto-generated />
using System;
using System.Collections.Generic;
using MenuMasterAPI.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MenuMasterAPI.Infrastructure.Migrations
{
    [DbContext(typeof(MealMateAPIDbContext))]
    [Migration("20240603150210_firstMigration")]
    partial class firstMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FoodRecipeUser", b =>
                {
                    b.Property<int>("FoodRecipesId")
                        .HasColumnType("integer");

                    b.Property<int>("UsersId")
                        .HasColumnType("integer");

                    b.HasKey("FoodRecipesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("FoodRecipeUser");
                });

            modelBuilder.Entity("MenuMasterAPI.Domain.Entities.FoodRecipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<List<string>>("Ingredients")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<bool>("IsLiked")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<int>("MealType")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Recipe")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("FoodRecipes");
                });

            modelBuilder.Entity("MenuMasterAPI.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ActivityStatus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<List<string>>("CuisineNames")
                        .HasColumnType("text[]");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int[]>("DietTypes")
                        .IsRequired()
                        .HasColumnType("integer[]");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<int>("Height")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<DateTime?>("LastLogin")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasDefaultValue("User");

                    b.Property<DateTime?>("UpdatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Weight")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FoodRecipeUser", b =>
                {
                    b.HasOne("MenuMasterAPI.Domain.Entities.FoodRecipe", null)
                        .WithMany()
                        .HasForeignKey("FoodRecipesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MenuMasterAPI.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
