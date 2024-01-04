using Microsoft.EntityFrameworkCore;
using Sauces.Core;
using Sauces.Core.Model;

namespace Sauces.Api.Repositories;

public interface IFermentationRepository
{
    public Task<List<FermentationRecipe>> GetAsync();
    public Task<FermentationRecipe?> GetAsync(Guid id);

    public Task<Guid?> CreateAsync(FermentationRecipeRequest fermentationRecipeRequest);

    public Task<FermentationRecipe?> DeleteAsync(Guid id);

    public Task<FermentationRecipe?> UpdateAsync(Guid id, FermentationRecipeRequest request);
}


public class FermentationRepository(SaucesContext dbContext) : IFermentationRepository
{
    public async Task<List<FermentationRecipe>> GetAsync()
        => await dbContext.FermentationRecipes.IncludeEverything().ToListAsync();
    
    public async Task<FermentationRecipe?> GetAsync(Guid id)
        => await dbContext.FermentationRecipes.IncludeEverything().FirstOrDefaultAsync(r => r.Id == id);

    public async Task<Guid?> CreateAsync(FermentationRecipeRequest fermentationRecipeRequest)
    {
        var id = Guid.NewGuid();
        await dbContext.FermentationRecipes.AddAsync(ToRecipe(fermentationRecipeRequest, id));
        await dbContext.SaveChangesAsync();
        return id;
    }

    public async Task<FermentationRecipe?> DeleteAsync(Guid id)
    {
        var recipe = await GetAsync(id);
        if (recipe is null) return null;
        dbContext.FermentationRecipes.Remove(recipe);
        await dbContext.SaveChangesAsync();
        return recipe;
    }

    public async Task<FermentationRecipe?> UpdateAsync(Guid id, FermentationRecipeRequest request)
    {
        var found = await GetAsync(id);
        if (found is null)
            return null;
        var replacement = ToRecipe(request, id);
        dbContext.Entry(found).CurrentValues.SetValues(replacement);
        await dbContext.SaveChangesAsync();
        return replacement;
    }

    
    private FermentationRecipe ToRecipe(FermentationRecipeRequest request, Guid id)
    {
        var ingredients = request.Ingredients.Select(entry =>
        {
            var ingredient = dbContext.Ingredients.Find(entry.Ingredient)
                             ?? dbContext.Ingredients.Add(new Ingredient{Name = entry.Ingredient}).Entity;
            return new RecipeIngredient{ Ingredient = ingredient, Percentage = entry.Percentage};
        }).ToList();

        return new FermentationRecipe
        {
            Id = id,
            LengthInDays = request.LengthInDays, 
            Ingredients = ingredients
            
        };
    }
}
