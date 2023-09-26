﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace AccountManagement.Application.Contracts.Account;

public class EditProfileCommand
{
    public long Id { get; set; }

    [Display(Name = "نام و نام خانوادگی")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کراکتر باشد")]
    public string FullName { get; set; }

    [Display(Name = "ایمیل")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(500, ErrorMessage = "{0} نمیتواند بیشتر از {1} کراکتر باشد")]
    [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمیباشد")]
    public string Email { get; set; }

    [Display(Name = "نام کاربری")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(200, ErrorMessage = "{0} نمیتواند بیشتر از {1} کراکتر باشد")]
    [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "{0} باید شامل حروف انگلیسی یا عدد باشد")]
    public string Username { get; set; }

    [Display(Name = "تصویر")]
    public IFormFile Image { get; set; }

    public string ImageName { get; set; }
}