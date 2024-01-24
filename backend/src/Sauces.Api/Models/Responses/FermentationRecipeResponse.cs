using System.Text.Json.Serialization;

namespace Sauces.Api.Models.Responses;

public class FermentationRecipeResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    [JsonPropertyName("ingredients")]
    public IngredientModel[] Ingredients { get; set; }
    
    [JsonPropertyName("lengthInDays")]
    public int LengthInDays { get; set; }
    
    [JsonPropertyName("lastUpdated")]
    public DateTime LastUpdated { get; set; }
    
    [JsonPropertyName("Created")]
    public DateTime Created { get; set; }
}