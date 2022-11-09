using System.Text.Json.Serialization;

namespace TaskManager.Models.Tag
{
    public class TagDataModel
    {
        [JsonPropertyName("tag_id")]
        [JsonPropertyOrder(0)]
        public int TagId { get; set; }

        [JsonPropertyName("tag_name")]
        [JsonPropertyOrder(1)]
        public string TagName { get; set; } = null!;

        [JsonPropertyName("tag_description")]
        [JsonPropertyOrder(2)]
        public string TagDescription { get; set; } = null!;
    }
}
