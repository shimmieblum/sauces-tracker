using System.Text.Json.Serialization;

namespace Sauces.Api.Models.Requests;

public class SauceUpdateRequest
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("fermentationRecipe")] 
    public FermentationRecipeUpdateRequest FermentationRecipe { get; set; }
    
    [JsonPropertyName("nonFermentedIngredients")]
    public List<IngredientModel> NonFermentedIngredients { get; set; }
    
    [JsonPropertyName("fermentationPercentage")]
    public int FermentationPercentage { get; set; }
    
    [JsonPropertyName("notes")]
    public string Notes { get; set; }
}

public class FermentationRecipeUpdateRequest
{   
    [JsonPropertyName("ingredients")]
    public List<IngredientModel> Ingredients { get; set; }
    
    [JsonPropertyName("lengthInDays")]
    public int LengthInDays { get; set; }
    
} 