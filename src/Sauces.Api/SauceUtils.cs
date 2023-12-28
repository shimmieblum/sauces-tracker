using Sauces.Core.Model;

namespace Sauces.Api;

public static class SauceUtils
{
    public static Sauce CreateSauce(
        string name,
        string notes,
        Dictionary<string, int> fermentationIngredients,
        int fermentationLength,
        int fermentationPercentage,
        string vinegar)
    {
        var fermentationIngredientsList = fermentationIngredients.Select(kv =>
        {
            var (ingr, perc) = kv;
            var ingredient = new Ingredient() { Name = ingr };
            return new RecipeIngredient() { Ingredient = ingredient, Percentage = perc };
        }).ToList();

        var fermentationRecipe = new FermentationRecipe()
        {
            Ingredients = fermentationIngredientsList,
            LengthInDays = fermentationLength
        };

        var fermentation = new FermentationRecipeAsIngredient() { FermentationRecipe = fermentationRecipe, };

        var nonFermentedIngredient = new RecipeIngredient()
        {
            Ingredient = new() { Name = vinegar },
            Percentage = 100 - fermentationPercentage
        };
        return new Sauce
        {
            Name = name,
            Notes = notes,
            Recipe = new SauceRecipe
            {
                Fermentations = new() { fermentation },
                NonFermentedIngredients = new() { nonFermentedIngredient }
            }
        };
    }

    public static string GetLoggingString(this Sauce sauce)
    {
        var fermentationPercentage = sauce.Recipe.Fermentations.Sum(f => f.Percentage);
        var fermentationIngredients =
            sauce.Recipe.Fermentations
                .FirstOrDefault()?.FermentationRecipe.Ingredients
                .Select(i => i.Ingredient.Name) ?? new string[] { };
        var vinegar = sauce.Recipe.NonFermentedIngredients.FirstOrDefault();
        return $"""
               Name: {sauce.Name}
               fermentation ingredients ({fermentationPercentage}%): {string.Join(", ", fermentationIngredients)}
               vinegar ({vinegar?.Percentage ?? 0})%: {vinegar?.Ingredient.Name ?? ""}
               notes: {sauce.Notes}
               """;
    }
}