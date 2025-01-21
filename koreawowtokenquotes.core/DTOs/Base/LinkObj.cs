namespace koreawowtokenquotes.core.DTOs.Base;

public record LinkObj
{
    [JsonPropertyName("href")]
    public required Uri Href { get; set; }
}