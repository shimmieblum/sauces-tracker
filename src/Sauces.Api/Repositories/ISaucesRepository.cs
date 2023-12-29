using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Sauces.Api.Models;
using Sauces.Core;
using Sauces.Core.Model;

namespace Sauces.Api.Repositories;


public interface ISaucesRepository
{
    public Task<List<Sauce>> GetSauces();
    public Task<Guid?> AddSauce(SauceRequest sauceRequest);
    public Task<Sauce?> GetSauce(Guid id);
}

public class SauceRepository(SaucesContext dbContext) : ISaucesRepository
{
    public async Task<List<Sauce>> GetSauces()
        =>  await dbContext.Sauces.IncludeEverything().ToListAsync();
    
    public async Task<Sauce?> GetSauce(Guid id)
        => await dbContext.Sauces.IncludeEverything().FirstOrDefaultAsync(s => s.Id == id);
    
    
    public async Task<Guid?> AddSauce(SauceRequest sauceRequest)
    {
        var fermentation = await dbContext.FermentationRecipes.FindAsync(sauceRequest.Fermentation);
        if (fermentation is null)
            return null;
        var id = Guid.NewGuid();

        var nonFermentedIngredients = sauceRequest.NonFermentedIngredients.Select(kv =>
        {
            var (ingredientName, percentage) = kv;
            var ingredient = dbContext.Ingredients.Find(ingredientName)
                             ?? dbContext.Ingredients.Add(new Ingredient() { Name = ingredientName }).Entity;
            return new RecipeIngredient { Ingredient = ingredient, Percentage = percentage };
        }).ToList();
        
        var sauce = new Sauce
        {
            Name = sauceRequest.Name,
            Notes = sauceRequest.Notes,
            Id = id,
            Fermentation = new()
            {
                Id = Guid.NewGuid(),
                FermentationRecipe = fermentation,
                Percentage = sauceRequest.FermentationPercentage
            },
            NonFermentedIngredients = nonFermentedIngredients
        };
        await dbContext.Sauces.AddAsync(sauce);
        await dbContext.SaveChangesAsync();
        return id;
    }

}