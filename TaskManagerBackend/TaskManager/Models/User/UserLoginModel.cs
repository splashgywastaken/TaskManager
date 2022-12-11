using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models.User;

public class UserLoginModel
{
    [Required]
    [DataType(DataType.EmailAddress)]
    [RegularExpression(@"(\S{1,})@(\S{1,}).(\S{1,})")]
    [Display(Name = "UserEmail")]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "UserPassword")]
    public string Password { get; set; }

    public UserLoginModel(string email, string password)
    {
        Email = email;
        Password = password;
    }
}