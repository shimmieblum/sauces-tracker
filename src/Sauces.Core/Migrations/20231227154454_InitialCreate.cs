using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sauces.Core.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FermentationRecipes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    LengthInDays = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FermentationRecipes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredient",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "SauceRecipe",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SauceRecipe", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FermentationRecipeAsIngredient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FermentationRecipeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Percentage = table.Column<int>(type: "INTEGER", nullable: false),
                    SauceRecipeId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FermentationRecipeAsIngredient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FermentationRecipeAsIngredient_FermentationRecipes_FermentationRecipeId",
                        column: x => x.FermentationRecipeId,
                        principalTable: "FermentationRecipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FermentationRecipeAsIngredient_SauceRecipe_SauceRecipeId",
                        column: x => x.SauceRecipeId,
                        principalTable: "SauceRecipe",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    IngredientName = table.Column<string>(type: "TEXT", nullable: false),
                    Percentage = table.Column<int>(type: "INTEGER", nullable: false),
                    FermentationRecipeId = table.Column<Guid>(type: "TEXT", nullable: true),
                    SauceRecipeId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingredients_FermentationRecipes_FermentationRecipeId",
                        column: x => x.FermentationRecipeId,
                        principalTable: "FermentationRecipes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ingredients_Ingredient_IngredientName",
                        column: x => x.IngredientName,
                        principalTable: "Ingredient",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ingredients_SauceRecipe_SauceRecipeId",
                        column: x => x.SauceRecipeId,
                        principalTable: "SauceRecipe",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sauces",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    RecipeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sauces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sauces_SauceRecipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "SauceRecipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FermentationRecipeAsIngredient_FermentationRecipeId",
                table: "FermentationRecipeAsIngredient",
                column: "FermentationRecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_FermentationRecipeAsIngredient_SauceRecipeId",
                table: "FermentationRecipeAsIngredient",
                column: "SauceRecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_FermentationRecipeId",
                table: "Ingredients",
                column: "FermentationRecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_IngredientName",
                table: "Ingredients",
                column: "IngredientName");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_SauceRecipeId",
                table: "Ingredients",
                column: "SauceRecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Sauces_RecipeId",
                table: "Sauces",
                column: "RecipeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FermentationRecipeAsIngredient");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Sauces");

            migrationBuilder.DropTable(
                name: "FermentationRecipes");

            migrationBuilder.DropTable(
                name: "Ingredient");

            migrationBuilder.DropTable(
                name: "SauceRecipe");
        }
    }
}
