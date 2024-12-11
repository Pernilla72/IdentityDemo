using System.ComponentModel.DataAnnotations;

namespace IdentityDemo.Views.Account
{
    public class UpdateVm
    {
        [Required]
        public string Username { get; set; } = null!;
        [Display(Name = "Förnamn")]
        public string? FirstName { get; set; }
        [Display(Name = "Efternamn")]
        public string? LastName { get; set; }
        [Display(Name = "Födelsedatum")]
        public DateTime BirthDate { get; set; }
    }
}
