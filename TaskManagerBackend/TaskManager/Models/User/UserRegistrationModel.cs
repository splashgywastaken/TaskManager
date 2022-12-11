using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TaskManager.Models.User
{
    public class UserRegistrationModel
    {
        [JsonPropertyOrder(0)]
        [JsonPropertyName("user_name")]
        public string UserName { get; set; } = null!;
        [JsonPropertyOrder(1)]
        [JsonPropertyName("user_email")]
        [RegularExpression(@"(\S{1,})@(\S{1,}).(\S{1,})")]
        public string UserEmail { get; set; } = null!;
        [JsonPropertyOrder(2)]
        [JsonPropertyName("user_password")]
        public string UserPassword { get; set; } = null!;  
    }
}
