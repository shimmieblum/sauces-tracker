using System.Text.Json.Serialization;
using Sauces.Core.Model;

namespace Sauces.Api.Models;

public class SauceUpdateRequest
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("fermentationRecipe")] 
    public FermentationRecipeUpdateRequest FermentationRecipe { get; set; }
    
    [JsonPropertyName("nonFermentedIngredients")]
    public List<RecipeIngredient> NonFermentedIngredients { get; set; }
    
    [JsonPropertyName("fermentationPercentage")]
    public int FermentationPercentage { get; set; }
    
    [JsonPropertyName("notes")]
    public string Notes { get; set; }
}

public class FermentationRecipeUpdateRequest
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    [JsonPropertyName("ingredients")]
    public RecipeIngredient[] Ingredients { get; set; }
    
    [JsonPropertyName("lengthInDays")]
    public int LengthInDays { get; set; }
    
} 