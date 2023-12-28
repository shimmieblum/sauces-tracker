namespace Sauces.Core.Model;

public class Sauce
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public SauceRecipe Recipe { get; set; }
    public string Notes { get; set; }
}