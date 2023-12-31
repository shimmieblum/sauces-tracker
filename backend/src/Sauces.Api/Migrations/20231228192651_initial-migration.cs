using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sauces.Api.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FermentationRecipes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LengthInDays = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FermentationRecipes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "FermentationRecipeAsIngredient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FermentationRecipeId = table.Column<Guid>(type: "uuid", nullable: true),
                    Percentage = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FermentationRecipeAsIngredient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FermentationRecipeAsIngredient_FermentationRecipes_Fermenta~",
                        column: x => x.FermentationRecipeId,
                        principalTable: "FermentationRecipes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sauces",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    FermentationId = table.Column<Guid>(type: "uuid", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sauces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sauces_FermentationRecipeAsIngredient_FermentationId",
                        column: x => x.FermentationId,
                        principalTable: "FermentationRecipeAsIngredient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeIngredient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IngredientName = table.Column<string>(type: "text", nullable: true),
                    Percentage = table.Column<int>(type: "integer", nullable: false),
                    FermentationRecipeId = table.Column<Guid>(type: "uuid", nullable: true),
                    SauceId = table.Column<Guid>(type: "uuid", nullable: true)
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
                        name: "FK_RecipeIngredient_Sauces_SauceId",
                        column: x => x.SauceId,
                        principalTable: "Sauces",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FermentationRecipeAsIngredient_FermentationRecipeId",
                table: "FermentationRecipeAsIngredient",
                column: "FermentationRecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredient_FermentationRecipeId",
                table: "RecipeIngredient",
                column: "FermentationRecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredient_IngredientName",
                table: "RecipeIngredient",
                column: "IngredientName");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredient_SauceId",
                table: "RecipeIngredient",
                column: "SauceId");

            migrationBuilder.CreateIndex(
                name: "IX_Sauces_FermentationId",
                table: "Sauces",
                column: "FermentationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecipeIngredient");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Sauces");

            migrationBuilder.DropTable(
                name: "FermentationRecipeAsIngredient");

            migrationBuilder.DropTable(
                name: "FermentationRecipes");
        }
    }
}
