using Sauces.Api.Models;
using Sauces.Api.Models.Requests;
using Sauces.Core.Model;

namespace Sauces.Api.Tests.FixturesAndUtils.Builders;

public class SauceUpdateRequestBuilder
{
    private string _name;
    private FermentationRecipeUpdateRequest _fermentationRecipe;
    private List<IngredientModel> _otherIngredients;
    private int _fermentationPercentage;
    private string _notes;
    
    public SauceUpdateRequestBuilder WithName(string name)
    {
        _name = name;
        return this;
    }
    
    public SauceUpdateRequestBuilder WithFermentationRecipe(FermentationRecipeUpdateRequest fermentationRecipe)
    {
        _fermentationRecipe = fermentationRecipe;
        return this;
    }
    
    public SauceUpdateRequestBuilder WithNonFermentedIngredients(params  IngredientModel[] otherIngredients)
    {
        _otherIngredients = otherIngredients.ToList();
        return this;
    }
    
    public SauceUpdateRequestBuilder WithFermentationPercentage(int fermentationPercentage)
    {
        _fermentationPercentage = fermentationPercentage;
        return this;
    }
    
    public SauceUpdateRequestBuilder WithNotes(string notes)
    {
        _notes = notes;
        return this;
    }

    public SauceUpdateRequestBuilder FromSauce(Sauce sauce)
    {
        _name = new string(sauce.Name);
        _fermentationRecipe = new FermentationRecipeUpdateRequestBuilder()
            .FromFermentationRecipe(sauce.Fermentation.FermentationRecipe).Build();
        _otherIngredients = sauce.NonFermentedIngredients.Select(i => new IngredientModel
        {
            Percentage = i.Percentage, Ingredient = new string(i.Ingredient.Name)
        }).ToList();
        _fermentationPercentage = sauce.Fermentation.Percentage;
        _notes = new string(sauce.Notes);
        return this;
    }

    public SauceUpdateRequest Build() => new SauceUpdateRequest
    {
        Name = _name,
        FermentationRecipe = _fermentationRecipe,
        NonFermentedIngredients = _otherIngredients,
        FermentationPercentage = _fermentationPercentage,
        Notes = _notes
    };
}