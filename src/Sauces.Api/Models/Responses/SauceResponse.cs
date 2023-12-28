using System.Text.Json.Serialization;

namespace Sauces.Api.Models;

public class SauceResponse 
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("fermentation")]
    public Dictionary<string, int> Fermentation { get; set; }

    [JsonPropertyName("fermentationPercentage")]
    public int FermentationPercentage { get; set; }
    
    [JsonPropertyName("nonFermentedIngredients")]
    public Dictionary<string, int>? NonFermentedIngredients { get; set; }

    [JsonPropertyName("notes")]
    public string Notes { get; set; }
}
