
using Sauces.Api.Models;

namespace Sauces.Api.Tests;

public class FermentationRequestBuilder
{
    private int _lengthInDays;
    private List<IngredientsModel> _ingredients;

    public FermentationRequestBuilder WithLengthInDays(int length)
    {
        _lengthInDays = length;
        return this;
    }

    public FermentationRequestBuilder WithIngredients(params IngredientsModel[] ingredients)
    {
        _ingredients = ingredients.ToList();
        return this;
    }

    public FermentationRequestBuilder FromDefault()
    {
        _lengthInDays = 30;
        _ingredients = [new IngredientsModel { Ingredient = Guid.NewGuid().ToString(), Percentage = 100 }];
        return this;
    }

    public FermentationRecipeRequest Build() => new()
    {
        Ingredients = _ingredients,
        LengthInDays = _lengthInDays
    };
}