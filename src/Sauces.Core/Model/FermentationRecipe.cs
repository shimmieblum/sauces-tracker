namespace Sauces.Core.Model;

public class FermentationRecipe
{
    public Guid Id { get; set; }
    public int LengthInDays { get; set; }
    public List<RecipeIngredient> Ingredients { get; set; }
    public bool IngredientsAreConsistent => Ingredients.Sum(i => i.Percentage) == 100;
}

