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

    private static async Task<IResult> PostFermentationHandler(ISaucesRepository sauceRepository, [FromBody] FermentationRecipeRequest fermentationRecipeRequest)
    {
        var id = await sauceRepository.AddFermentationRecipe(fermentationRecipeRequest) ;
        return id is null ? BadRequest("failed to add fermentation recipe") : Ok(id);
    }

    private static async Task<IResult> GetFermentationsHandler(ISaucesRepository sauceRepository)
    {
        var recipes = await sauceRepository.GetFermentationRecipes();
        var response = recipes.Select(r => r.ToResponse());
        return Ok(response); 
    }

    private static async Task<IResult> GetFermentationHandler(ISaucesRepository saucesRepository, [FromRoute] Guid id)
    {
        var recipe = await saucesRepository.GetFermentationRecipe(id);
        return recipe is null 
            ? NotFound($"No Fermentation with id {id} found") 
            : Ok(recipe.ToResponse());
    }
}