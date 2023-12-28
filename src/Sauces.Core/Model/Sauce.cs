namespace Sauces.Core.Model;

public class Sauce
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public FermentationRecipeAsIngredient Fermentation { get; set; }
    public List<RecipeIngredient> NonFermentedIngredients { get; set; }
    public string Notes { get; set; }
}