using System.Data;
using System.Net.Mail;

namespace Sauces.Core.Model;

public class SauceRecipe
{
    public Guid Id { get; set; }
    public List<FermentationRecipeAsIngredient> Fermentations { get; set; } 
    public List<RecipeIngredient> NonFermentedIngredients { get; set; }

    public bool IngredientsAreConsistent =>
        NonFermentedIngredients.Sum(i => i.Percentage) + Fermentations.Sum(f => f.Percentage) == 100;
    
    

}