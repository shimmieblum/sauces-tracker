using System.ComponentModel.DataAnnotations;

namespace Sauces.Core.Model;

public class Ingredient
{
    [Key]
    [MaxLength(50)]
    public string Name { get; set; }
}