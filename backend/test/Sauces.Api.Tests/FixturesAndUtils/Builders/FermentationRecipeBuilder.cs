using Sauces.Core.Model;

namespace Sauces.Api.Tests.FixturesAndUtils.Builders;

public class FermentationRecipeBuilder
{
    private DateTime _created;
    private Guid _id;
    private List<RecipeIngredient> _recipeIngredients;
    private DateTime _lastUpdated;
    private int _lengthInDays;

    public FermentationRecipeBuilder FromDefault()
    {
        _created = DateTime.Now;
        _id = Guid.NewGuid();
        var ingredientName = Guid.NewGuid().ToString();
        _recipeIngredients =
        [
            new RecipeIngredient
            {
                Id = Guid.NewGuid(), Ingredient = new() { Name = ingredientName }, Percentage = 100
            }
        ];
        _lastUpdated = DateTime.Now;
        _lengthInDays = 30;

        return this;
    }
    
    public FermentationRecipeBuilder WithCreated(DateTime created)
    {
        _created = created;
        return this;
    }

    public FermentationRecipeBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public FermentationRecipeBuilder WithIngredients(params RecipeIngredient[] ingredients)
    {
        _recipeIngredients = ingredients.ToList();
        return this;
    }
    
    
    public FermentationRecipeBuilder WithLastUpdated(DateTime lastUpdated)
    {
        _lastUpdated = lastUpdated;
        return this;
    }
    
    
    public FermentationRecipeBuilder WithLengthInDays(int days)
    {
        _lengthInDays = days;
        return this;
    }
    
    
    public FermentationRecipeBuilder DeepCopyRecipe(FermentationRecipe recipe)
    {
        _created = recipe.Created;
        _id = recipe.Id;
        _recipeIngredients = recipe.Ingredients
            .Select(i => new RecipeIngredient
            {
                Id = i.Id, Ingredient = new Ingredient { Name = i.Ingredient.Name }, Percentage = i.Percentage
            })
            .ToList();
        _lastUpdated = recipe.LastUpdate;
        _lengthInDays = recipe.LengthInDays;

        return this;
    }
    
    public FermentationRecipe Build() => new FermentationRecipe
    {
        Created = _created,
        Id = _id,
        Ingredients = _recipeIngredients,
        LastUpdate = _lastUpdated,
        LengthInDays = _lengthInDays
    };

}