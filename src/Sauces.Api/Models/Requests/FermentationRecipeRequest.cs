using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

public class FermentationRecipeRequest
{
    [JsonPropertyName("ingredients")]
    public Dictionary<string, int> Ingredients { get; set; }
    
    [JsonPropertyName("lengthInDays")]
    public int LengthInDays { get; set; }
}