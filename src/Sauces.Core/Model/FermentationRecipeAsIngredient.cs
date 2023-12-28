namespace Sauces.Core.Model;

public class FermentationRecipeAsIngredient
{
    public Guid Id { get; set; }
    public FermentationRecipe? FermentationRecipe { get; set; }
    public int Percentage { get; set; }
}