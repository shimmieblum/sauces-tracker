using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sauces.Api.Migrations
{
    /// <inheritdoc />
    public partial class addedcontraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FermentationRecipeAsIngredient_FermentationRecipes_Fermenta~",
                table: "FermentationRecipeAsIngredient");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Sauces",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Sauces",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "IngredientName",
                table: "RecipeIngredient",
                type: "character varying(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Ingredients",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<Guid>(
                name: "FermentationRecipeId",
                table: "FermentationRecipeAsIngredient",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FermentationRecipeAsIngredient_FermentationRecipes_Fermenta~",
                table: "FermentationRecipeAsIngredient",
                column: "FermentationRecipeId",
                principalTable: "FermentationRecipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FermentationRecipeAsIngredient_FermentationRecipes_Fermenta~",
                table: "FermentationRecipeAsIngredient");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Sauces",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Sauces",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "IngredientName",
                table: "RecipeIngredient",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Ingredients",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<Guid>(
                name: "FermentationRecipeId",
                table: "FermentationRecipeAsIngredient",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_FermentationRecipeAsIngredient_FermentationRecipes_Fermenta~",
                table: "FermentationRecipeAsIngredient",
                column: "FermentationRecipeId",
                principalTable: "FermentationRecipes",
                principalColumn: "Id");
        }
    }
}
