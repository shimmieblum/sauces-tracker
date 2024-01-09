using System.Text.Json.Serialization;
using Sauces.Api.Models;

public class FermentationRecipeResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    [JsonPropertyName("ingredients")]
    public IngredientsModel[] Ingredients { get; set; }
    
    [JsonPropertyName("lengthInDays")]
    public int LengthInDays { get; set; }
    
    [JsonPropertyName("lastUpdated")]
    public DateTime LastUpdated { get; set; }
    
    [JsonPropertyName("Created")]
    public DateTime Created { get; set; }
}