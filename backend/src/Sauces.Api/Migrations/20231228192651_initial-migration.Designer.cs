﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Sauces.Api.Repositories;
using Sauces.Core;

#nullable disable

namespace Sauces.Api.Migrations
{
    [DbContext(typeof(SaucesContext))]
    [Migration("20231228192651_initial-migration")]
    partial class initialmigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Sauces.Core.Model.FermentationRecipe", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("LengthInDays")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("FermentationRecipes");
                });

            modelBuilder.Entity("Sauces.Core.Model.FermentationRecipeAsIngredient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("FermentationRecipeId")
                        .HasColumnType("uuid");

                    b.Property<int>("Percentage")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FermentationRecipeId");

                    b.ToTable("FermentationRecipeAsIngredient");
                });

            modelBuilder.Entity("Sauces.Core.Model.Ingredient", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Name");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("Sauces.Core.Model.RecipeIngredient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("FermentationRecipeId")
                        .HasColumnType("uuid");

                    b.Property<string>("IngredientName")
                        .HasColumnType("text");

                    b.Property<int>("Percentage")
                        .HasColumnType("integer");

                    b.Property<Guid?>("SauceId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("FermentationRecipeId");

                    b.HasIndex("IngredientName");

                    b.HasIndex("SauceId");

                    b.ToTable("RecipeIngredient");
                });

            modelBuilder.Entity("Sauces.Core.Model.Sauce", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("FermentationId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("FermentationId");

                    b.ToTable("Sauces");
                });

            modelBuilder.Entity("Sauces.Core.Model.FermentationRecipeAsIngredient", b =>
                {
                    b.HasOne("Sauces.Core.Model.FermentationRecipe", "FermentationRecipe")
                        .WithMany()
                        .HasForeignKey("FermentationRecipeId");

                    b.Navigation("FermentationRecipe");
                });

            modelBuilder.Entity("Sauces.Core.Model.RecipeIngredient", b =>
                {
                    b.HasOne("Sauces.Core.Model.FermentationRecipe", null)
                        .WithMany("Ingredients")
                        .HasForeignKey("FermentationRecipeId");

                    b.HasOne("Sauces.Core.Model.Ingredient", "Ingredient")
                        .WithMany()
                        .HasForeignKey("IngredientName");

                    b.HasOne("Sauces.Core.Model.Sauce", null)
                        .WithMany("NonFermentedIngredients")
                        .HasForeignKey("SauceId");

                    b.Navigation("Ingredient");
                });

            modelBuilder.Entity("Sauces.Core.Model.Sauce", b =>
                {
                    b.HasOne("Sauces.Core.Model.FermentationRecipeAsIngredient", "Fermentation")
                        .WithMany()
                        .HasForeignKey("FermentationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Fermentation");
                });

            modelBuilder.Entity("Sauces.Core.Model.FermentationRecipe", b =>
                {
                    b.Navigation("Ingredients");
                });

            modelBuilder.Entity("Sauces.Core.Model.Sauce", b =>
                {
                    b.Navigation("NonFermentedIngredients");
                });
#pragma warning restore 612, 618
        }
    }
}
