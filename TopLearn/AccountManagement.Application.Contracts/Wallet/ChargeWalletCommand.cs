using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Application.Contracts.Wallet;

public class ChargeWalletCommand
{
    [Display(Name = "مبلغ")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [Range(1000,1000000,ErrorMessage = "مبلغ باید از 1000 تومان تا 1000000 تومان باشد")]
    public double Amount { get; set; }

    public long AccountId { get; set; }

    public bool IsPayed { get; set; }
}