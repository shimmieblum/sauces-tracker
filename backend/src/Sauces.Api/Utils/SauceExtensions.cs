using Microsoft.EntityFrameworkCore;
using Sauces.Core.Model;

namespace Sauces.Api;

public static class SauceExtensions
{
    public static IQueryable<Sauce> IncludeEverything(this DbSet<Sauce> sauces)
        => sauces
            .Include(r => r.Fermentation)
                .ThenInclude(f => f.FermentationRecipe)
                    .ThenInclude(f => f.Ingredients)
                        .ThenInclude(i => i.Ingredient)
            .Include(r => r.NonFermentedIngredients)
                .ThenInclude(n => n.Ingredient);

    public static IQueryable<FermentationRecipe> IncludeEverything(this DbSet<FermentationRecipe> fermentationRecipes)
        => fermentationRecipes
            .Include(r => r.Ingredients)
            .ThenInclude(i => i.Ingredient);
    
    
    public static string GetLoggingString(this Sauce sauce)
    {
        var fermentationPercentage = sauce.Fermentation.Percentage;
        var fermentationIngredients =
            sauce.Fermentation.FermentationRecipe?.Ingredients
                .Select(i => i.Ingredient.Name) ?? new string[] { };
        var vinegar = sauce.NonFermentedIngredients.FirstOrDefault();
        return $"""
               Name: {sauce.Name}
               fermentation ingredients ({fermentationPercentage}%): {string.Join(", ", fermentationIngredients)}
               vinegar ({vinegar?.Percentage ?? 0})%: {vinegar?.Ingredient.Name ?? ""}
               notes: {sauce.Notes}
               """;
    }
}