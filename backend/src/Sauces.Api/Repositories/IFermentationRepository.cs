using Microsoft.EntityFrameworkCore;
using Sauces.Api.Models.ExtensionsAndUtils;
using Sauces.Api.Models.Requests;
using Sauces.Core.Model;

namespace Sauces.Api.Repositories;

public interface IFermentationRepository
{
    public Task<List<FermentationRecipe>> GetAsync();
    public Task<FermentationRecipe?> GetAsync(Guid id);

    public Task<Guid?> CreateAsync(FermentationRecipeRequest fermentationRecipeRequest);

    public Task<FermentationRecipe?> DeleteAsync(Guid id);

    public Task<FermentationRecipe?> UpdateAsync(Guid id, FermentationRecipeUpdateRequest request);
}


public class FermentationRepository(SaucesContext dbContext) 
    : SaucesDbRepoBase( dbContext), IFermentationRepository
{
    public async Task<List<FermentationRecipe>> GetAsync()
        => await DbContext.FermentationRecipes.IncludeEverything().ToListAsync();
    
    public async Task<FermentationRecipe?> GetAsync(Guid id)
        => await DbContext.FermentationRecipes.IncludeEverything().FirstOrDefaultAsync(r => r.Id == id);

    public async Task<Guid?> CreateAsync(FermentationRecipeRequest fermentationRecipeRequest)
    {
        var id = Guid.NewGuid();
        await DbContext.FermentationRecipes.AddAsync(ToRecipe(fermentationRecipeRequest, id));
        await DbContext.SaveChangesAsync();
        return id;
    }

    public async Task<FermentationRecipe?> DeleteAsync(Guid id)
    {
        var recipe = await GetAsync(id);
        if (recipe is null) return null;
        DbContext.FermentationRecipes.Remove(recipe);
        await DbContext.SaveChangesAsync();
        return recipe;
    }

    public async Task<FermentationRecipe?> UpdateAsync(Guid id, FermentationRecipeUpdateRequest request )
    {
        var recipe = await GetAsync(id);
        if (recipe is null || recipe.IsEquivalentTo(request))
        {
            return recipe;
        }

        if (recipe.LengthInDays != request.LengthInDays) recipe.LengthInDays = request.LengthInDays;
        if (recipe.Ingredients.IngredientsDiffer(request.Ingredients)) 
            recipe.Ingredients = request.Ingredients.Select(AsRecipeIngredient).ToList();
        recipe.LastUpdate = DateTime.Now;

        return recipe;
    }

    
    private FermentationRecipe ToRecipe(FermentationRecipeRequest request, Guid id)
    {
        var ingredients = request.Ingredients.Select(entry =>
        {
            var ingredient = DbContext.Ingredients.Find(entry.Ingredient)
                             ?? DbContext.Ingredients.Add(new Ingredient{Name = entry.Ingredient}).Entity;
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
