using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sauces.Core.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_FermentationRecipes_FermentationRecipeId",
                table: "Ingredients");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Ingredient_IngredientName",
                table: "Ingredients");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_SauceRecipe_SauceRecipeId",
                table: "Ingredients");

            migrationBuilder.DropTable(
                name: "Ingredient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_FermentationRecipeId",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_IngredientName",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_SauceRecipeId",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "FermentationRecipeId",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "Percentage",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "SauceRecipeId",
                table: "Ingredients");

            migrationBuilder.RenameColumn(
                name: "IngredientName",
                table: "Ingredients",
                newName: "Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients",
                column: "Name");

            migrationBuilder.CreateTable(
                name: "RecipeIngredient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    IngredientName = table.Column<string>(type: "TEXT", nullable: true),
                    Percentage = table.Column<int>(type: "INTEGER", nullable: false),
                    FermentationRecipeId = table.Column<Guid>(type: "TEXT", nullable: true),
                    SauceRecipeId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeIngredient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeIngredient_FermentationRecipes_FermentationRecipeId",
                        column: x => x.FermentationRecipeId,
                        principalTable: "FermentationRecipes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RecipeIngredient_Ingredients_IngredientName",
                        column: x => x.IngredientName,
                        principalTable: "Ingredients",
                        principalColumn: "Name");
                    table.ForeignKey(
                        name: "FK_RecipeIngredient_SauceRecipe_SauceRecipeId",
                        column: x => x.SauceRecipeId,
                        principalTable: "SauceRecipe",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredient_FermentationRecipeId",
                table: "RecipeIngredient",
                column: "FermentationRecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredient_IngredientName",
                table: "RecipeIngredient",
                column: "IngredientName");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredient_SauceRecipeId",
                table: "RecipeIngredient",
                column: "SauceRecipeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeIngredient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Ingredients",
                newName: "IngredientName");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Ingredients",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "FermentationRecipeId",
                table: "Ingredients",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Percentage",
                table: "Ingredients",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "SauceRecipeId",
                table: "Ingredients",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_FermentationRecipes_FermentationRecipeId",
                table: "Ingredients",
                column: "FermentationRecipeId",
                principalTable: "FermentationRecipes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Ingredient_IngredientName",
                table: "Ingredients",
                column: "IngredientName",
                principalTable: "Ingredient",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_SauceRecipe_SauceRecipeId",
                table: "Ingredients",
                column: "SauceRecipeId",
                principalTable: "SauceRecipe",
                principalColumn: "Id");
        }
    }
}
