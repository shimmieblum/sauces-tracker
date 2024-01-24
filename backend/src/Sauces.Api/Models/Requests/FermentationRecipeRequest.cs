using System.Text.Json.Serialization;

namespace Sauces.Api.Models.Requests;

public class FermentationRecipeRequest
{
    [JsonPropertyName("ingredients")]
    public List<IngredientModel> Ingredients { get; set; }
    
    [JsonPropertyName("lengthInDays")]
    public int LengthInDays { get; set; }
}