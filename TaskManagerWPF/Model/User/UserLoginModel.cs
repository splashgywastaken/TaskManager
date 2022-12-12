using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TaskManagerWPF.Model.User
{
    public class UserLoginModel
    {
        public UserLoginModel(string userEmail, string userPassword)
        {
            UserEmail = userEmail;
            UserPassword = userPassword;
        }

        [Newtonsoft.Json.JsonProperty("email")]
        public string UserEmail { get; set; }
        [Newtonsoft.Json.JsonProperty("password")]
        public string UserPassword { get; set; }
    }
}
