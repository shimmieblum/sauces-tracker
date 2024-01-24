using Sauces.Core.Model;

namespace Sauces.Api.Tests.FixturesAndUtils.Builders;

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
        _fermentation = new() { Id = Guid.NewGuid(), FermentationRecipe = new FermentationRecipeBuilder().FromDefault().Build(), Percentage = 100};
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
    
    public SauceBuilder WithOtherIngredients( params RecipeIngredient[] otherIngredients)
    {
        _otherIngredients = otherIngredients.ToList();
        return this;
    }
    
     public SauceBuilder WithNotes ( string notes)
     {
         _notes = notes;
         return this;
     }

     public SauceBuilder DeepCopySauce(Sauce sauce)
     {
         _id = sauce.Id;
         _created = sauce.Created;
         _name = sauce.Name;
         _lastUpdated = sauce.LastUpdated;
         _fermentation = new FermentationRecipeAsIngredient()
         {
             Id = sauce.Fermentation.Id,
             Percentage = sauce.Fermentation.Percentage,
             FermentationRecipe = new FermentationRecipeBuilder().DeepCopyRecipe(sauce.Fermentation.FermentationRecipe).Build(),
         };
         _otherIngredients = sauce.NonFermentedIngredients.Select(i => new RecipeIngredient
         {
             Id = i.Id, Percentage = i.Percentage, Ingredient = new Ingredient { Name = i.Ingredient.Name }
         }).ToList();
         _notes = sauce.Notes;

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