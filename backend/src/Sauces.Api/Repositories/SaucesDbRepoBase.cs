using Sauces.Api.Models;
using Sauces.Core.Model;

namespace Sauces.Api.Repositories;

public abstract class SaucesDbRepoBase(SaucesContext dbContext)
{
    protected readonly SaucesContext DbContext = dbContext;

    protected RecipeIngredient AsRecipeIngredient(IngredientModel ingredient) {
        var recipeIngredient = new RecipeIngredient
        {
            Ingredient = EnsureIngredient(ingredient.Ingredient), Percentage = ingredient.Percentage
        };
        DbContext.Add(recipeIngredient);
        return recipeIngredient;
    }
    
    
    protected Ingredient EnsureIngredient(string name) => 
        DbContext.Ingredients.Find(name) ?? DbContext.Ingredients.Add(new Ingredient { Name = name }).Entity;

}