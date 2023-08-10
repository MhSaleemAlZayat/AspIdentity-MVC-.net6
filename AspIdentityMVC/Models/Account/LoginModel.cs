using System.ComponentModel.DataAnnotations;

namespace AspIdentityMVC.Models.Account;

public class LoginModel
{
    [Required(ErrorMessage = "The Email field is required.")]
    public string Username { get; set; }
    [Required(ErrorMessage ="Password required.")]
    public string Password { get; set; }
    public bool RememberLogin { get; set; }
    public string ReturnUrl { get; set; }
}
