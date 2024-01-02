using Microsoft.AspNetCore.Mvc;
using Sauces.Api.Models;
using Sauces.Api.Repositories;
using Sauces.Core.Model;
using static Microsoft.AspNetCore.Http.Results;

namespace Sauces.Api.Handlers;

public static class SauceHandlers
{
    public static RouteGroupBuilder MapSauceApi(this RouteGroupBuilder app)
    {
        app.MapGet("", GetSaucesHandler);
        app.MapGet("/{id:guid}", GetSauceHandler);
        app.MapPost("", PostSauceHandler);

        return app;
    }

    private static SauceResponse ToSauceResponse(this Sauce sauce) =>
        new()
        {
            Id = sauce.Id,
            Name = sauce.Name,
            Fermentation = new() 
            {
                Ingredients = sauce.Fermentation.FermentationRecipe.Ingredients
                    .Select(i => new IngredientsModel{
                        
                            Ingredient = i.Ingredient.Name, Percentage = i.Percentage
                    }).ToArray(),
                lengthInDays  = sauce.Fermentation.FermentationRecipe.LengthInDays
            },
            FermentationPercentage = sauce.Fermentation.Percentage,
            NonFermentedIngredients =
                sauce.NonFermentedIngredients.Select( i => new IngredientsModel
                {
                    Ingredient = i.Ingredient.Name,
                    Percentage = i.Percentage
                }).ToArray(),
            Notes = sauce.Notes
        };

    private static async Task<IResult> GetSaucesHandler(ISaucesRepository saucesRepository)
    {
        var sauces = await saucesRepository.GetAsync();
        var sauceResponses = sauces.Select(s => s.ToSauceResponse());
        return Ok(sauceResponses);
    }

    private static async Task<IResult> GetSauceHandler(ISaucesRepository saucesRepository, [FromRoute] Guid id)
    {
        var sauce = await saucesRepository.GetAsync(id);
        
        return sauce is null 
            ? NotFound($"Sauce with id {id} not found")
            : Ok(sauce.ToSauceResponse());
    }

    private static async Task<IResult> PostSauceHandler(
        ISaucesRepository saucesRepository, [FromBody] SauceRequest request)
    {
        var id = await saucesRepository.CreateAsync(request);
        return id is null ? BadRequest("failed to add sauce") : Ok(id);
    }
}