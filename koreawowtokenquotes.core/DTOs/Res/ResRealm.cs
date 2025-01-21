namespace koreawowtokenquotes.core.DTOs.Res;

using koreawowtokenquotes.core.DTOs.Base;

public class ResRealm : AbsRealmAPI
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("region")]
    public required _Region Region { get; set; }

    [JsonPropertyName("connected_realm")]
    public required LinkObj ConnectedRealm { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("category")]
    public required string Category { get; set; }

    [JsonPropertyName("locale")]
    public required string Locale { get; set; }

    [JsonPropertyName("timezone")]
    public required string Timezone { get; set; }

    [JsonPropertyName("type")]
    public required _Type Type { get; set; }

    [JsonPropertyName("is_tournament")]
    public bool IsTournament { get; set; }

    [JsonPropertyName("slug")]
    public required string Slug { get; set; }

    public record _Region
    {
        [JsonPropertyName("key")]
        public required LinkObj Key { get; set; }

        [JsonPropertyName("name")]
        public required string Name { get; set; }

        [JsonPropertyName("id")]
        public long Id { get; set; }
    }

    public record _Type
    {
        [JsonPropertyName("type")]
        public required string Type { get; set; }

        [JsonPropertyName("name")]
        public required string Name { get; set; }
    }
}
