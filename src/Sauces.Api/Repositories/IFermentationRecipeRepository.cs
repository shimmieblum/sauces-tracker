using Microsoft.EntityFrameworkCore;
using Sauces.Core;
using Sauces.Core.Model;

namespace Sauces.Api.Repositories;

public interface IFermentationRecipeRepository
{
    public Task<List<FermentationRecipe>> GetFermentationRecipes();
    public Task<FermentationRecipe?> GetFermentationRecipe(Guid id);

    public Task<Guid?> AddFermentationRecipe(FermentationRecipeRequest fermentationRecipeRequest);
}


public class FermentationRecipeRepository(SaucesContext dbContext) : IFermentationRecipeRepository
{
    public async Task<Guid?> AddFermentationRecipe(FermentationRecipeRequest fermentationRecipeRequest)
    {
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
    
    
    public async Task<List<FermentationRecipe>> GetFermentationRecipes()
        => await dbContext.FermentationRecipes.IncludeEverything().ToListAsync();
    
    public async Task<FermentationRecipe?> GetFermentationRecipe(Guid id)
        => await dbContext.FermentationRecipes.IncludeEverything().FirstOrDefaultAsync(r => r.Id == id);
}
