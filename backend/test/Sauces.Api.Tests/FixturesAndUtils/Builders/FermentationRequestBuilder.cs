
using Sauces.Api.Models;
using Sauces.Api.Models.Requests;

namespace Sauces.Api.Tests.FixturesAndUtils.Builders;

public class FermentationRequestBuilder
{
    private int _lengthInDays;
    private List<IngredientModel> _ingredients;

    public FermentationRequestBuilder WithLengthInDays(int length)
    {
        _lengthInDays = length;
        return this;
    }

    public FermentationRequestBuilder WithIngredients(params IngredientModel[] ingredients)
    {
        _ingredients = ingredients.ToList();
        return this;
    }

    public FermentationRequestBuilder FromDefault()
    {
        _lengthInDays = 30;
        _ingredients = [new IngredientModel { Ingredient = Guid.NewGuid().ToString(), Percentage = 100 }];
        return this;
    }

    public FermentationRecipeRequest Build() => new()
    {
        Ingredients = _ingredients,
        LengthInDays = _lengthInDays
    };
}