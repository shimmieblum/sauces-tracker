using Microsoft.AspNetCore.Mvc;
using Sauces.Api.Models.ExtensionsAndUtils;
using Sauces.Api.Models.Requests;
using Sauces.Api.Repositories;
using static Microsoft.AspNetCore.Http.Results;

namespace Sauces.Api.Handlers;

public static class FermentationHandlers
{
    public static RouteGroupBuilder MapFermentationApi(this RouteGroupBuilder app)
    {
        app.MapPost("", PostFermentationHandler);
        app.MapGet("", GetFermentationsHandler);
        app.MapGet("/{id:guid}", GetFermentationHandler);
        app.MapPut("/{id:guid}", UpdateFermentationHandler);
        return app;
    }
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

    private static async Task<IResult> UpdateFermentationHandler(
        IFermentationRepository fermentationRepository,
        [FromRoute] Guid id,
        [FromBody] FermentationRecipeUpdateRequest request)
    {
        var updated = await fermentationRepository.UpdateAsync(id, request);
        return updated is null
            ? NotFound($"Update of fermentation failed")
            : Ok(updated.ToResponse());
    }
}