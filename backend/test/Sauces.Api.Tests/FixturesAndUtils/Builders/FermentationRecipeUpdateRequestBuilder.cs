using Sauces.Api.Models;
using Sauces.Api.Models.Requests;
using Sauces.Core.Model;

namespace Sauces.Api.Tests.FixturesAndUtils.Builders;

public class FermentationRecipeUpdateRequestBuilder
{
    private Guid _id;
    private List<IngredientModel> _ingredients;
    private int _lengthInDays;

    public FermentationRecipeUpdateRequestBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public FermentationRecipeUpdateRequestBuilder WithIngredients(params IngredientModel[] ingredients)
    {
        _ingredients = ingredients.ToList();
        return this;
    }

    public FermentationRecipeUpdateRequestBuilder WithLengthInDays(int lengthInDays)
    {
        _lengthInDays = lengthInDays;
        return this;
    }

    public FermentationRecipeUpdateRequestBuilder FromFermentationRecipe(FermentationRecipe recipe)
    {
        _id = recipe.Id;
        _ingredients = recipe.Ingredients.Select(i => new IngredientModel
        {
            Percentage = i.Percentage, Ingredient = new string(i.Ingredient.Name)
        }).ToList();
        _lengthInDays = recipe.LengthInDays;
        return this; 
    }
    
    public FermentationRecipeUpdateRequest Build() => new()
    {
        Ingredients = _ingredients,
        LengthInDays = _lengthInDays
    };
}