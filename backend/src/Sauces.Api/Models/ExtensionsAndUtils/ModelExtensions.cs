using Microsoft.EntityFrameworkCore;
using Sauces.Api.Models.Requests;
using Sauces.Api.Models.Responses;
using Sauces.Core.Model;

namespace Sauces.Api.Models.ExtensionsAndUtils;

public static class ModelExtensions
{
    public static bool IsEquivalentTo(this Sauce sauce, SauceUpdateRequest updateRequest)
        => sauce.Name == updateRequest.Name
           && sauce.Notes == updateRequest.Notes
           && sauce.Fermentation.IsEquivalentTo(updateRequest.FermentationRecipe, updateRequest.FermentationPercentage)
           && !sauce.NonFermentedIngredients.IngredientsDiffer(updateRequest.NonFermentedIngredients);

    public static bool IsEquivalentTo(this FermentationRecipeAsIngredient recipe,
        FermentationRecipeUpdateRequest request,
        int fermentationPercentage)
        => recipe.Percentage == fermentationPercentage
           && recipe.FermentationRecipe.IsEquivalentTo(request);
    
    public static bool IsEquivalentTo(this FermentationRecipe recipe, FermentationRecipeUpdateRequest request) =>
        recipe.LengthInDays == request.LengthInDays && !recipe.Ingredients.IngredientsDiffer(request.Ingredients);
    
    public static bool IngredientsDiffer(this IEnumerable<RecipeIngredient> ingredients,
        IEnumerable<IngredientModel> ingredientModels)
        => ingredients
            .Select(i => (i.Ingredient.Name, i.Percentage))
            .Except(ingredientModels.Select(i => (i.Ingredient, i.Percentage))).Any();

    public static SauceResponse ToSauceResponse(this Sauce sauce) => new()
    {
        Id = sauce.Id,
        Name = sauce.Name,
        Fermentation = new() 
        {
            Id = sauce.Fermentation.Id,
            Ingredients = sauce.Fermentation.FermentationRecipe.Ingredients
                .Select(i => new IngredientModel{
                    
                    Ingredient = i.Ingredient.Name, Percentage = i.Percentage
                }).ToArray(),
            lengthInDays  = sauce.Fermentation.FermentationRecipe.LengthInDays
        },
        FermentationPercentage = sauce.Fermentation.Percentage,
        NonFermentedIngredients =
            sauce.NonFermentedIngredients.Select( i => new IngredientModel
            {
                Ingredient = i.Ingredient.Name,
                Percentage = i.Percentage
            }).ToArray(),
        Notes = sauce.Notes,
        Created = sauce.Created,
        LastUpdated = sauce.LastUpdated
    };


    public static FermentationRecipeResponse ToResponse(this FermentationRecipe recipe) => new()
     {
         Id = recipe.Id,
         Ingredients = recipe.Ingredients.Select(i => new IngredientModel
         {
             Ingredient = i.Ingredient.Name, Percentage = i.Percentage
         }).ToArray(),
         LengthInDays = recipe.LengthInDays,
         LastUpdated = recipe.LastUpdate,
         Created = recipe.Created
     };

    
    public static IQueryable<Sauce> IncludeEverything(this DbSet<Sauce> sauces)
        => sauces
            .Include(r => r.Fermentation).ThenInclude(f => f.FermentationRecipe).ThenInclude(f => f.Ingredients).ThenInclude(i => i.Ingredient)
            .Include(r => r.NonFermentedIngredients).ThenInclude(n => n.Ingredient);

    public static IQueryable<FermentationRecipe> IncludeEverything(this DbSet<FermentationRecipe> fermentationRecipes)
        => fermentationRecipes
            .Include(r => r.Ingredients).ThenInclude(i => i.Ingredient);


}