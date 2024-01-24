using System.Text.Json.Serialization;

namespace Sauces.Api.Models.Responses;

public class SauceResponse 
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("fermentation")]
    public Fermentation Fermentation { get; set; }

    [JsonPropertyName("fermentationPercentage")]
    public int FermentationPercentage { get; set; }
    
    [JsonPropertyName("nonFermentedIngredients")]
    public IngredientModel[] NonFermentedIngredients { get; set; }

    [JsonPropertyName("notes")]
    public string Notes { get; set; }
    
    [JsonPropertyName("lastUpdated")]
    public DateTime LastUpdated { get; set; }
    
    [JsonPropertyName("Created")]
    public DateTime Created { get; set; }
}

public class Fermentation 
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    [JsonPropertyName("ingredients")]
    public IngredientModel[] Ingredients { get; set;}

    [JsonPropertyName("lengthInDays")]
    public int lengthInDays { get; set; }
}