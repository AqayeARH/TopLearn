﻿using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Application.Contracts.Account;

public class ResetPasswordCommand
{
    [Display(Name = "کلمه عبور")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(500, ErrorMessage = "{0} نمیتواند بیشتر از {1} کراکتر باشد")]
    [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "{0} باید شامل حروف انگلیسی یا عدد باشد")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = "تکرار کلمه عبور")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [Compare("Password", ErrorMessage = "کلمه عبور با تکرار آن مطابقت ندارد")]
    [DataType(DataType.Password)]
    public string RePassword { get; set; }

    public string ActiveCode { get; set; }
}