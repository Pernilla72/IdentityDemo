using System.ComponentModel.DataAnnotations;

namespace IdentityDemo.Views.Account
{
    public class RegisterVM
    {
        [Required]
        public string Username { get; set; } = null!;
        [Display(Name = "Förnamn")]
        public string? FirstName { get; set; }
        [Display(Name = "Efternamn")]
        public string? LastName { get; set; }
        [Display(Name = "Födelsedatum")]
        public DateTime BirthDate { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Repeat password")]
        [Compare(nameof(Password))]
        public string PasswordRepeat { get; set; } = null!;
    }
}
//Todo fixa datumformat, ta bort tid