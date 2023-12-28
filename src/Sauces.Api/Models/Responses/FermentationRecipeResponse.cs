using System.Text.Json.Serialization;

public class FermentationRecipeResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    [JsonPropertyName("ingredients")]
    public Dictionary<string, int> Ingredients { get; set; }
    
    [JsonPropertyName("lengthInDays")]
    public int LengthInDays { get; set; }
}