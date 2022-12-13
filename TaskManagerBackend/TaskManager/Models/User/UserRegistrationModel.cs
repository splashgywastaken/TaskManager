using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TaskManager.Models.User
{
    public class UserRegistrationModel
    {
        [Required]
        [JsonPropertyOrder(0)]
        [JsonPropertyName("user_name")]
        public string UserName { get; set; } = null!;

        [Required]
        [JsonPropertyOrder(1)]
        [DataType(DataType.EmailAddress)]
        [JsonPropertyName("user_email")]
        [RegularExpression(@"(\S{1,})@(\S{1,}).(\S{1,})")]
        public string UserEmail { get; set; } = null!;

        [Required]
        [JsonPropertyOrder(2)]
        [DataType(DataType.Password)]
        [JsonPropertyName("user_password")]
        public string UserPassword { get; set; } = null!;  
    }
}
