using Sauces.Api.Models;
using Sauces.Api.Models.Requests;

namespace Sauces.Api.Tests.FixturesAndUtils.Builders;

public class SauceRequestBuilder
{
    private Guid _fermentationId;
    private int _fermentationPercentage;
    private List<IngredientModel> _otherIngredients;
    private string _name;
    private string _notes;

    public SauceRequestBuilder FromDefault()
    {
        _fermentationId = Guid.NewGuid();
        _fermentationPercentage = 100;
        var ingredient = Guid.NewGuid().ToString();
        _otherIngredients = [new IngredientModel { Ingredient = ingredient, Percentage = 100 }];
        _name = "name";
        _notes = "notes";

        return this;
    }

    public SauceRequestBuilder WithNotes(string notes)
    {
        _notes = notes;
        return this;
    }
    
    public SauceRequestBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public SauceRequestBuilder WithIngredients(params IngredientModel[] ingredientsModels)
    {
        _otherIngredients = ingredientsModels.ToList();
        return this;
    }

    public SauceRequestBuilder WithFermentationPercentage(int percentage)
    {
        _fermentationPercentage = percentage;
        return this;
    }

    public SauceRequestBuilder WithRecipeId(Guid id)
    {
        _fermentationId = id;
        return this;
    }


    public SauceRequest Build() => new ()
    {
        Fermentation = _fermentationId,
        FermentationPercentage = _fermentationPercentage,
        NonFermentedIngredients = _otherIngredients,
        Name = _name,
        Notes = _notes
    };


}