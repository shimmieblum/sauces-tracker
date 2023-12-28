using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Sauces.Api.Models;
using Sauces.Core;
using Sauces.Core.Model;

namespace Sauces.Api.Repositories;

public interface ISaucesRepository
{
    public Task<List<Sauce>> GetSauces();
    public Task<List<FermentationRecipe>> GetFermentationRecipes();
    public Task<FermentationRecipe?> GetFermentationRecipe(Guid id);
    public Task<Ingredient?> TryGetIngredient(string ingredient);
    public Task<List<Ingredient>> GetIngredients();

    public Task<Guid?> AddSauce(SauceRequest sauceRequest);
    public Task<Guid?> AddFermentationRecipe(FermentationRecipeRequest fermentationRecipeRequest);
    public Task<Sauce?> GetSauce(Guid id);
}


public class SauceRepository : ISaucesRepository
{
    public async Task<List<Sauce>> GetSauces()
    {
        await using var dbContext = new SaucesContext();
        return await dbContext.Sauces.IncludeEverything().ToListAsync();
    }
    
    public async Task<Sauce?> GetSauce(Guid id)
    {
        await using var dbContext = new SaucesContext();

        return await dbContext.Sauces.IncludeEverything().FirstOrDefaultAsync(s => s.Id == id);
    }
    
    public async Task<List<FermentationRecipe>> GetFermentationRecipes()
    {
        await using var dbContext = new SaucesContext();

        return await dbContext.FermentationRecipes.IncludeEverything().ToListAsync();
    }
    
    public async Task<FermentationRecipe?> GetFermentationRecipe(Guid id)
    {
        await using var dbContext = new SaucesContext();
    
        return await dbContext.FermentationRecipes.IncludeEverything().FirstOrDefaultAsync(r => r.Id == id);
    }
    
    public async Task<Ingredient?> TryGetIngredient(string ingredient)
    {
        await using var dbContext = new SaucesContext();

        return await dbContext.Ingredients.FindAsync(ingredient);
    }
    
    public async Task<List<Ingredient>> GetIngredients()
    {
        await using var dbContext = new SaucesContext();

        return await dbContext.Ingredients.ToListAsync();
    }
    
    public async Task<Guid?> AddSauce(SauceRequest sauceRequest)
    {
        await using var dbContext = new SaucesContext();
        var fermentation = await GetFermentationRecipe(sauceRequest.Fermentation);
        if (fermentation is null)
            return null;
        var id = Guid.NewGuid();
        var sauce = new Sauce
        {
            Id = id,
            Fermentation = new()
            {
                Id = Guid.NewGuid(),
                FermentationRecipe = fermentation,
                Percentage = sauceRequest.FermentationPercentage
            }
        };
        await dbContext.Sauces.AddAsync(sauce);
        await dbContext.SaveChangesAsync();
        return id;
    }

    public async Task<Guid?> AddFermentationRecipe(FermentationRecipeRequest fermentationRecipeRequest)
    {
        await using var dbContext = new SaucesContext();
        var id = Guid.NewGuid();
        var ingredients = fermentationRecipeRequest.Ingredients.Select(kv =>
        {
            var (ingredientName, percentage) = kv;
            var ingredient = dbContext.Ingredients.Find(ingredientName)
                             ?? dbContext.Ingredients.Add(new Ingredient { Name = ingredientName }).Entity;
            return new RecipeIngredient
            {
                Id = Guid.NewGuid(),
                Ingredient = ingredient,
                Percentage = percentage
            };
        }).ToList();

        var fermentationRecipe = new FermentationRecipe
        {
            Id = id,
            Ingredients = ingredients,
            LengthInDays = fermentationRecipeRequest.LengthInDays
        };
        
        await dbContext.FermentationRecipes.AddAsync(fermentationRecipe);
        await dbContext.SaveChangesAsync();
        return id;
    }
}