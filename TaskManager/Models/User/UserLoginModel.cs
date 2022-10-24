using System.ComponentModel.DataAnnotations;

namespace TaskManager.Service.User;

public class UserLoginModel
{
    [Required]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    public UserLoginModel(string email, string password)
    {
        Email = email;
        Password = password;
    }
}