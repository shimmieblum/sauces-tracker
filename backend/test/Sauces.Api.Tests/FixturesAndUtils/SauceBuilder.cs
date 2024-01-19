using Sauces.Api.Models;
using Sauces.Core.Model;

namespace Sauces.Api.Tests.FixturesAndUtils;

public class SauceBuilder
{
    private Guid _id;
    private DateTime _created;
    private DateTime _lastUpdated;
    private string  _name;
    private FermentationRecipeAsIngredient _fermentation;
    private List<RecipeIngredient> _otherIngredients;
    private string _notes;

    public SauceBuilder FromDefault()
    {
        _id = Guid.NewGuid(); 
        _created = DateTime.Now; 
        _lastUpdated = DateTime.Now;
        _name = "name";
        _fermentation = new() { Id = Guid.NewGuid() };
        var ingredientName = Guid.NewGuid().ToString();
        _otherIngredients =
        [
            new() { Id = Guid.NewGuid(), Ingredient = new Ingredient {Name  = ingredientName}, Percentage = 67 }
        ];
        _notes = "notes";
        
        return this;
    }
     public SauceBuilder WithId(Guid id){
         _id = id;
         return this;
     }
    
    public SauceBuilder CreatedAt(DateTime created){
        _created = created;
        return this;
    }
    
     public SauceBuilder UpdatedAt(DateTime lastUpdated){
         _lastUpdated = lastUpdated;
         return this;
     }
    
     public SauceBuilder WithName(string name){
         _name = name;
         return this;
     }
    
     public SauceBuilder WithFermentationRecipe(FermentationRecipe fermentation){
         _fermentation.FermentationRecipe = fermentation;
         return this;
     }

     public SauceBuilder WithFermentationPercentage(int percentage)
     {
         _fermentation.Percentage = percentage;
         return this;
     }
    
    public SauceBuilder WithOtherIngredients( params RecipeIngredient[] otherIngredients){
        _otherIngredients = otherIngredients.ToList();
        return this;
    }
    
     public SauceBuilder WithNotes ( string notes){
         _notes = notes;
         return this;
     }


     public Sauce Build() => new()
     {
         Id = _id,
         Created = _created,
         LastUpdated = _lastUpdated,
         Name = _name,
         Fermentation = _fermentation,
         NonFermentedIngredients = _otherIngredients,
         Notes = _notes
     };
}