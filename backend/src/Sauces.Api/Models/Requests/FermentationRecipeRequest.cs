using System.Text.Json.Serialization;
using Sauces.Api.Models;
using Sauces.Core.Model;

public class FermentationRecipeRequest
{
    [JsonPropertyName("ingredients")]
    public List<IngredientsModel> Ingredients { get; set; }
    
    [JsonPropertyName("lengthInDays")]
    public int LengthInDays { get; set; }
}