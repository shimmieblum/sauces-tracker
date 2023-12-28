using System.ComponentModel.DataAnnotations;

namespace Sauces.Core.Model;

public class Ingredient
{
    [Key]
    public string Name { get; set; }
}