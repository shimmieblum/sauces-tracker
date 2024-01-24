using System.Text.Json.Serialization;

namespace Sauces.Api.Models.Requests;

public class SauceRequest
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("fermentation")]
    public Guid Fermentation { get; set; }

    [JsonPropertyName("fermentationPercentage")]
    public int FermentationPercentage { get; set; }
    
    [JsonPropertyName("nonFermentedIngredients")]
    public List<IngredientModel> NonFermentedIngredients { get; set; }
    
    [JsonPropertyName("notes")]
    public string Notes { get; set; }
}
