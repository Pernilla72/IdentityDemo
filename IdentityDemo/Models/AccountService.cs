using IdentityDemo.Views.Account;
using Microsoft.AspNetCore.Identity;

namespace IdentityDemo.Models;

public class AccountService(UserManager<ApplicationUser> userManager,// Hanterar användare
             SignInManager<ApplicationUser> signInManager,           // Hanterar inlogging
             RoleManager<IdentityRole> roleManager                   // Hanterar roller
             )

{
    internal async Task<string?> TryRegisterUserAsync(RegisterVM viewModel)
    {
        var user = new ApplicationUser
        {
            UserName = viewModel.Username,
        };

        IdentityResult result = await
            userManager.CreateAsync(user, viewModel.Password);

        bool wasUserCreated = result.Succeeded;
        return wasUserCreated ? null : result.Errors.First().Description;
    }
    //public string? TryRegister(RegisterVM viewModel)
    //{
    //    // Todo: Try to create a new user
    //    return "Failed to create user";
    //}

    internal async Task<string?> TryLoginAsync(LoginVM viewModel)
    {
        SignInResult result = await signInManager.PasswordSignInAsync(
            viewModel.Username,
            viewModel.Password,
            isPersistent: false,
            lockoutOnFailure: false);

        bool wasUserSignedIn = result.Succeeded;
        return wasUserSignedIn ? null : "Invalid username or password";
    }
}
