using System.Text.Json.Serialization;

public class FermentationRecipeRequest
{
    [JsonPropertyName("ingredients")]
    public Dictionary<string, int> Ingredients { get; set; }
    
    [JsonPropertyName("lengthInDays")]
    public int LengthInDays { get; set; }
}