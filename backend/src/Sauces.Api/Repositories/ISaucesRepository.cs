using Microsoft.EntityFrameworkCore;
using Sauces.Api.Models.ExtensionsAndUtils;
using Sauces.Api.Models.Requests;
using Sauces.Core.Model;

namespace Sauces.Api.Repositories;


public interface ISaucesRepository
{
    public Task<List<Sauce>> GetAsync();
    public Task<Guid?> CreateAsync(SauceRequest sauceRequest);
    public Task<Sauce?> GetAsync(Guid id);
    public Task<Sauce?> DeleteAsync(Guid id);
    public Task<Sauce?> UpdateAsync(Guid id, SauceUpdateRequest request);
}

public class SauceRepository(SaucesContext dbContext) 
    : SaucesDbRepoBase(dbContext), ISaucesRepository
{
    public async Task<List<Sauce>> GetAsync()
        =>  await DbContext.Sauces.IncludeEverything().ToListAsync();
    
    public async Task<Sauce?> GetAsync(Guid id)
        => await DbContext.Sauces.IncludeEverything().FirstOrDefaultAsync(s => s.Id == id);

    public async Task<Sauce?> DeleteAsync(Guid id)
    {
        var sauce = await GetAsync(id);
        if (sauce is null)
            return null;
        DbContext.Sauces.Remove(sauce);
        await DbContext.SaveChangesAsync();
        return sauce;
    }

    public async Task<Sauce?> UpdateAsync(Guid id, SauceUpdateRequest request)
    {
        var sauce = await DbContext.Sauces
            .IncludeEverything()
            .FirstOrDefaultAsync(s => s.Id == id);
        if (sauce is null)
            return null;

        if (sauce.IsEquivalentTo(request))
        {
            return sauce;
        }

        UpdateSauceDetails(sauce, request);
        UpdateFermentationRecipeDetails(sauce.Fermentation, request);
        
        await DbContext.SaveChangesAsync();
        return sauce;
    }

    private Sauce UpdateSauceDetails(Sauce sauce, SauceUpdateRequest request)
    {
        if (sauce.IsEquivalentTo(request))
        {
            return sauce;
        }

        if (sauce.Name != request.Name) sauce.Name = request.Name;
        if (sauce.Notes != request.Notes) sauce.Notes = request.Notes;
        if (sauce.NonFermentedIngredients.IngredientsDiffer(request.NonFermentedIngredients))
            sauce.NonFermentedIngredients = request.NonFermentedIngredients.Select(AsRecipeIngredient).ToList();
        sauce.LastUpdated = DateTime.Now;
        return sauce;
    }

    private FermentationRecipeAsIngredient UpdateFermentationRecipeDetails(FermentationRecipeAsIngredient recipeAsIngredient, SauceUpdateRequest request)
    {
        if (recipeAsIngredient.IsEquivalentTo(request.FermentationRecipe, request.FermentationPercentage))
        {
            return recipeAsIngredient;
        }
        var recipe = recipeAsIngredient.FermentationRecipe;
        if (recipeAsIngredient.Percentage != request.FermentationPercentage) recipeAsIngredient.Percentage = request.FermentationPercentage;
        if (recipe.LengthInDays != request.FermentationRecipe.LengthInDays)
            recipe.LengthInDays = request.FermentationRecipe.LengthInDays;
        if (recipe.Ingredients.IngredientsDiffer(request.FermentationRecipe.Ingredients))
            recipe.Ingredients = request.FermentationRecipe.Ingredients.Select(AsRecipeIngredient).ToList();
        recipe.LastUpdate = DateTime.Now;

        return recipeAsIngredient;
    }
    
    public async Task<Guid?> CreateAsync(SauceRequest sauceRequest)
    {
        var id = Guid.NewGuid();
        var sauce = await ToSauceAsync(sauceRequest, id);
        if (sauce is null) 
            return null;
        await DbContext.Sauces.AddAsync(sauce);
        await DbContext.SaveChangesAsync();
        return id;
    }

    private async Task<Sauce?> ToSauceAsync(SauceRequest request, Guid id)
    {
        var fermentation = await DbContext.FermentationRecipes
            .IncludeEverything()
            .FirstOrDefaultAsync(f => f.Id == request.Fermentation);
        
        if (fermentation is null) 
            return null;
        return new Sauce
        {
            Id = id,
            Name = request.Name,
            Fermentation = new FermentationRecipeAsIngredient
            {
                Id = Guid.NewGuid(),
                FermentationRecipe = fermentation,
                Percentage = request.FermentationPercentage
            },
            NonFermentedIngredients = request.NonFermentedIngredients.Select(i =>
            {
                var ingredient = EnsureIngredient(i.Ingredient);
                return new RecipeIngredient
                {
                    Id = Guid.NewGuid(), Ingredient = ingredient, Percentage = i.Percentage
                };
            }).ToList(),
            Notes = request.Notes
        };
    }
}