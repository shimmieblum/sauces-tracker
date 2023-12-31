using System.ComponentModel.DataAnnotations;

namespace Sauces.Core.Model;

public class Sauce
{
    public Guid Id { get; set; } 
    [MaxLength(50)]
    public string Name { get; set; } 
    public FermentationRecipeAsIngredient Fermentation { get; set; }
    public List<RecipeIngredient> NonFermentedIngredients { get; set; } 
    [MaxLength(1000)]
    public string Notes { get; set; }
}