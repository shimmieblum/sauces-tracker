namespace Sauces.Core.Model;

public class RecipeIngredient
{
    public Guid Id { get; set; }
    public Ingredient Ingredient { get; set; }
    public int Percentage { get; set; }
}