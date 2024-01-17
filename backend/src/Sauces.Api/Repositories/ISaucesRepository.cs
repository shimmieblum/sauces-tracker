using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Sauces.Api.Models;
using Sauces.Core;
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

public class SauceRepository(SaucesContext dbContext) : ISaucesRepository
{
    public async Task<List<Sauce>> GetAsync()
        =>  await dbContext.Sauces.IncludeEverything().ToListAsync();
    
    public async Task<Sauce?> GetAsync(Guid id)
        => await dbContext.Sauces.IncludeEverything().FirstOrDefaultAsync(s => s.Id == id);

    public async Task<Sauce?> DeleteAsync(Guid id)
    {
        var sauce = await GetAsync(id);
        if (sauce is null)
            return null;
        dbContext.Sauces.Remove(sauce);
        await dbContext.SaveChangesAsync();
        return sauce;
    }

    public async Task<Sauce?> UpdateAsync(Guid id, SauceUpdateRequest request)
    {
        var ogSauce = await dbContext.Sauces
            .IncludeEverything()
            .FirstOrDefaultAsync(s => s.Id == id);
        if (ogSauce is null)
            return null;
        ogSauce.Name = request.Name;
        ogSauce.Notes = request.Notes;
        ogSauce.LastUpdated = DateTime.Now;
        if (ogSauce.Fermentation is FermentationRecipeAsIngredient recipeAsIngredient)
        {
            recipeAsIngredient.Percentage = request.FermentationPercentage;
            if (recipeAsIngredient.FermentationRecipe is FermentationRecipe recipe)
            {
                recipe.Ingredients = request.FermentationRecipe.Ingredients.ToList();
                recipe.LengthInDays = request.FermentationRecipe.LengthInDays;
                recipe.LastUpdate = DateTime.Now;
            }
        }
        ogSauce.NonFermentedIngredients = request.NonFermentedIngredients;
        await dbContext.SaveChangesAsync();
        return await dbContext.Sauces.IncludeEverything().FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Guid?> CreateAsync(SauceRequest sauceRequest)
    {
        var id = Guid.NewGuid();
        var sauce = await ToSauceAsync(sauceRequest, id);
        if (sauce is null) 
            return null;
        await dbContext.Sauces.AddAsync(sauce);
        await dbContext.SaveChangesAsync();
        return id;
    }
    
    


    private async Task<Sauce?> ToSauceAsync(SauceRequest request, Guid id)
    {
        var fermentation = await dbContext.FermentationRecipes
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
                var ingredient =  dbContext.Ingredients.Find(i.Ingredient)
                                 ??  dbContext.Ingredients.Add(new() { Name = i.Ingredient }).Entity;
                return new RecipeIngredient
                {
                    Id = Guid.NewGuid(), Ingredient = ingredient, Percentage = i.Percentage
                };
            }).ToList(),
            Notes = request.Notes
        };
    }
}