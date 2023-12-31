using Microsoft.EntityFrameworkCore;
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
    public Task<Sauce?> UpdateAsync(Guid id, SauceRequest request);
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
    
    public async Task<Sauce?> UpdateAsync(Guid id, SauceRequest request)
    {
        var found = await dbContext.Sauces.FindAsync(id);
        if (found is null)
            return null;
        var replacement = await ToSauceAsync(request, id);
        if (replacement is null)
            return null;
        dbContext.Entry(found).CurrentValues.SetValues(replacement);
        await dbContext.SaveChangesAsync();
        return replacement;
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
            NonFermentedIngredients = request.NonFermentedIngredients.Select(kv =>
            {
                var (ingredientName, percentage) = kv;
                var ingredient = dbContext.Ingredients.Find(ingredientName)
                                 ?? dbContext.Add(new Ingredient{Name = ingredientName}).Entity;
                return new RecipeIngredient
                {
                    Ingredient = ingredient, Percentage = percentage
                };
            }).ToList(),
            Notes = request.Notes
        };
    }
}