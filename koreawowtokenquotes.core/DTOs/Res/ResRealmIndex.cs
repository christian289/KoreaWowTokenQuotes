namespace koreawowtokenquotes.core.DTOs.Res;

using koreawowtokenquotes.core.DTOs.Base;

public class ResRealmIndex : AbsRealmAPI
{
    [JsonPropertyName("realms")]
    public required Realm[] Realms { get; set; }

    public record Realm
    {
        [JsonPropertyName("key")]
        public required LinkObj Key { get; set; }

        [JsonPropertyName("name")]
        public required string Name { get; set; }

        [JsonPropertyName("id")]
        public required long Id { get; set; }

        [JsonPropertyName("slug")]
        public required string Slug { get; set; }
    }
}
