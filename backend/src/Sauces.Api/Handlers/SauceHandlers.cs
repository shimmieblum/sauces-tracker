using Microsoft.AspNetCore.Mvc;
using Sauces.Api.Models.ExtensionsAndUtils;
using Sauces.Api.Models.Requests;
using Sauces.Api.Repositories;
using static Microsoft.AspNetCore.Http.Results;

namespace Sauces.Api.Handlers;

public static class SauceHandlers
{
    public static RouteGroupBuilder MapSauceApi(this RouteGroupBuilder app)
    {
        app.MapGet("", GetAllHandler);
        app.MapGet("/{id:guid}", GetOneHandler);
        app.MapPost("", PostHandler);
        app.MapPost("/with-fermentation", PostWithFermentationHandler);
        app.MapDelete("/{id:guid}", DeleteHandler);
        app.MapPut("/{id:guid}", UpdateHandler);

        return app;
    }
    
    private static async Task<IResult> GetAllHandler(ISaucesRepository saucesRepository)
    {
        var sauces = await saucesRepository.GetAsync();
        var sauceResponses = sauces.Select(s => s.ToSauceResponse());
        return Ok(sauceResponses);
    }

    private static async Task<IResult> GetOneHandler(ISaucesRepository saucesRepository, [FromRoute] Guid id)
    {
        var sauce = await saucesRepository.GetAsync(id);
        
        return sauce is null 
            ? NotFound($"Sauce with id {id} not found")
            : Ok(sauce.ToSauceResponse());
    }

    private static async Task<IResult> PostHandler(
        ISaucesRepository saucesRepository, [FromBody] SauceRequest request)
    {
        var id = await saucesRepository.CreateAsync(request);
        return id is null ? BadRequest("failed to add sauce") : Ok(id);
    }

    private static async Task<IResult> PostWithFermentationHandler(
        ISaucesRepository saucesRepository,
        IFermentationRepository fermentationRepository,
        [FromBody] SauceWithFermentationRequest request)
    {
        var fermentation = request.Fermentation;
        var fermentationId = await fermentationRepository.CreateAsync(fermentation);
        if (fermentationId is null)
        {
            return BadRequest("failure creating fermentation");
        }

        var sauceRequest = new SauceRequest
        {
            Name = request.Name,
            Fermentation = fermentationId.Value,
            FermentationPercentage = request.FermentationPercentage,
            NonFermentedIngredients = request.NonFermentedIngredients,
            Notes = request.Notes
        };
        
        return await PostHandler(saucesRepository, sauceRequest);
    }

    private static async Task<IResult> UpdateHandler(
        ISaucesRepository saucesRepository,
        IFermentationRepository fermentationRepository,
        [FromRoute] Guid id,
        [FromBody] SauceUpdateRequest request)
    {
        var sauce = await saucesRepository.UpdateAsync(id, request);

        return sauce is null ? NotFound() : Ok(sauce);
    }

    private static async Task<IResult> DeleteHandler(ISaucesRepository repository, [FromRoute] Guid id)
    {
        var sauce = await repository.DeleteAsync(id);
        return sauce is not null ? Ok(sauce) : NotFound($"sauce with id ${id} not found");
    }
}