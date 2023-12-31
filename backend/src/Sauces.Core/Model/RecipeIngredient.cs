namespace Sauces.Core.Model;

public class RecipeIngredient
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Ingredient Ingredient { get; set; }
    public int Percentage { get; set; }
}