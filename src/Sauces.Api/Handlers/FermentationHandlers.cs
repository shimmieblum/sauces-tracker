using Microsoft.AspNetCore.Mvc;
using Sauces.Api.Repositories;
using Sauces.Core.Model;
using static Microsoft.AspNetCore.Http.Results;

namespace Sauces.Api.Handlers;

public static class FermentationHandlers
{
    public static RouteGroupBuilder MapFermentationApi(this RouteGroupBuilder app)
    {
        app.MapPost("", PostFermentationHandler);
        app.MapGet("", GetFermentationsHandler);
        app.MapGet("/{id:guid}", GetFermentationHandler);
        
        return app;
    }

    private static FermentationRecipeResponse ToResponse(this FermentationRecipe recipe)
        => new()
        {
            Id = recipe.Id,
            Ingredients = recipe.Ingredients.ToDictionary(i => i.Ingredient.Name, i => i.Percentage),
            LengthInDays = recipe.LengthInDays
        };

    private static async Task<IResult> PostFermentationHandler(
        IFermentationRecipeRepository fermentationRecipeRepository, 
        [FromBody] FermentationRecipeRequest fermentationRecipeRequest)
    {
        var id = await fermentationRecipeRepository.AddFermentationRecipe(fermentationRecipeRequest) ;
        return id is null ? BadRequest("failed to add fermentation recipe") : Ok(id);
    }

    private static async Task<IResult> GetFermentationsHandler(IFermentationRecipeRepository fermentationRecipeRepository)
    {
        var recipes = await fermentationRecipeRepository.GetFermentationRecipes();
        var response = recipes.Select(r => r.ToResponse());
        return Ok(response); 
    }

    private static async Task<IResult> GetFermentationHandler(
        IFermentationRecipeRepository fermentationRecipeRepository, 
        [FromRoute] Guid id)
    {
        var recipe = await fermentationRecipeRepository.GetFermentationRecipe(id);
        return recipe is null 
            ? NotFound($"No Fermentation with id {id} found") 
            : Ok(recipe.ToResponse());
    }
}