using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sauces.Core.Migrations
{
    /// <inheritdoc />
    public partial class changeconnection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FermentationRecipeAsIngredient_FermentationRecipes_FermentationRecipeId",
                table: "FermentationRecipeAsIngredient");

            migrationBuilder.AlterColumn<Guid>(
                name: "FermentationRecipeId",
                table: "FermentationRecipeAsIngredient",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_FermentationRecipeAsIngredient_FermentationRecipes_FermentationRecipeId",
                table: "FermentationRecipeAsIngredient",
                column: "FermentationRecipeId",
                principalTable: "FermentationRecipes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FermentationRecipeAsIngredient_FermentationRecipes_FermentationRecipeId",
                table: "FermentationRecipeAsIngredient");

            migrationBuilder.AlterColumn<Guid>(
                name: "FermentationRecipeId",
                table: "FermentationRecipeAsIngredient",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FermentationRecipeAsIngredient_FermentationRecipes_FermentationRecipeId",
                table: "FermentationRecipeAsIngredient",
                column: "FermentationRecipeId",
                principalTable: "FermentationRecipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
