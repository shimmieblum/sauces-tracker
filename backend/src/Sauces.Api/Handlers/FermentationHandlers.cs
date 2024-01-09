using Microsoft.AspNetCore.Mvc;
using Sauces.Api.Models;
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
            Ingredients = recipe.Ingredients.Select(i => new IngredientsModel
            {
                Ingredient = i.Ingredient.Name, Percentage = i.Percentage
            }).ToArray(),
            LengthInDays = recipe.LengthInDays,
            LastUpdated = recipe.LastUpdate,
            Created = recipe.Created
        };

    private static async Task<IResult> PostFermentationHandler(
        IFermentationRepository fermentationRepository, 
        [FromBody] FermentationRecipeRequest fermentationRecipeRequest)
    {
        var id = await fermentationRepository.CreateAsync(fermentationRecipeRequest) ;
        return id is null ? BadRequest("failed to add fermentation recipe") : Ok(id);
    }

    private static async Task<IResult> GetFermentationsHandler(IFermentationRepository fermentationRepository)
    {
        var recipes = await fermentationRepository.GetAsync();
        var response = recipes.Select(r => r.ToResponse());
        return Ok(response); 
    }

    private static async Task<IResult> GetFermentationHandler(
        IFermentationRepository fermentationRepository, 
        [FromRoute] Guid id)
    {
        var recipe = await fermentationRepository.GetAsync(id);
        return recipe is null 
            ? NotFound($"No Fermentation with id {id} found") 
            : Ok(recipe.ToResponse());
    }
}