namespace koreawowtokenquotes.core.DTOs.Base;

public abstract class AbsRealmAPI
{
    [JsonPropertyName("_links")]
    public required _Links Links { get; set; }

    public record _Links
    {
        [JsonPropertyName("self")]
        public required LinkObj Self { get; set; }
    }
}
