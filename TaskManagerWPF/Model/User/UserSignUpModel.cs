using Newtonsoft.Json;

namespace TaskManagerWPF.Model.User
{
    class UserSignUpModel
    {
        [JsonProperty("user_name")] 
        public string UserName { get; set; } = null!;

        [JsonProperty("user_email")] 
        public string UserEmail { get; set; } = null!;
        [JsonProperty("user_password")] 
        public string UserPassword { get; set; } = null!;

        public UserSignUpModel(string userName, string userEmail, string userPassword)
        {
            UserName = userName;
            UserEmail = userEmail;
            UserPassword = userPassword;
        }
    }
}
