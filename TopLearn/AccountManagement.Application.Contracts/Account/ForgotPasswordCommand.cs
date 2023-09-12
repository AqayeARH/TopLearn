using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Application.Contracts.Account;

public class ForgotPasswordCommand
{
    [Display(Name = "ایمیل")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمیباشد")]
    public string Email { get; set; }
}