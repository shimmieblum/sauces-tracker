using System.Text.Json.Serialization;

namespace Sauces.Api.Models;

public class IngredientModel
{
    [JsonPropertyName("ingredient")]
    public string Ingredient { get; set; }
    
    [JsonPropertyName("percentage")]
    public int Percentage { get; set; }

}