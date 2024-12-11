using IdentityDemo.Views.Account;
using Microsoft.AspNetCore.Identity;

namespace IdentityDemo.Models;

public class AccountService(
             UserManager<ApplicationUser> userManager,// Hanterar användare
             SignInManager<ApplicationUser> signInManager,           // Hanterar inlogging
             RoleManager<IdentityRole> roleManager,                   // Hanterar roller
             IHttpContextAccessor contextAccessor
             )

{
    //public ApplicationUser GetUserById(string id)
    //{
    //    var user = new ApplicationUser
    //    {
    //        UserName = 
    //    }
    //}

    internal async Task<MembersVM> GetMembersAsync()
    {
        var loggedInUserId = userManager.GetUserId(contextAccessor.HttpContext.User);

        ApplicationUser user = await userManager.FindByIdAsync(loggedInUserId);

        return new MembersVM
        {
            Username = contextAccessor.HttpContext!.User.Identity!.Name!,
            BirthDate = user.BirthDate,
            FirstName = user.FirstName,
        };
    }
    internal async Task<string?> TryRegisterUserAsync(RegisterVM viewModel)
    {
        var user = new ApplicationUser
        {
            UserName = viewModel.Username,
            FirstName = viewModel.FirstName,
            LastName = viewModel.LastName,
            BirthDate = viewModel.BirthDate,
        };

        IdentityResult result = await
            userManager.CreateAsync(user, viewModel.Password);

        bool wasUserCreated = result.Succeeded;
        return wasUserCreated ? null : result.Errors.First().Description;
    }
    internal async Task TryLoginAfterRegisterAsync(RegisterVM viewModel)
    {
        var userLoginInfo = new LoginVM
        {
            Username = viewModel.Username,
            Password = viewModel.Password,
        };
        await TryLoginAsync(userLoginInfo);
    }
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

    internal async Task LogoutAsync()
    {
        await signInManager.SignOutAsync();
    }

    internal async Task<UpdateVm> LoggedInUserToUpdateVm()
    {
        var loggedInUserId = userManager.GetUserId(contextAccessor.HttpContext.User);
        ApplicationUser user = await userManager.FindByIdAsync(loggedInUserId);
        return new UpdateVm
        {
            Username = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            BirthDate = user.BirthDate
        };
    }
    internal async Task<string?> UpdateUser(UpdateVm model)
    {
        var loggedInUserId = userManager.GetUserId(contextAccessor.HttpContext.User);
        ApplicationUser user = await userManager.FindByIdAsync(loggedInUserId);

        user.UserName = model.Username;
        user.FirstName = model.FirstName;
        user.LastName = model.LastName;
        user.BirthDate = model.BirthDate;

        var result = await userManager.UpdateAsync(user);
        bool wasUserCreated = result.Succeeded;
        return wasUserCreated ? null : result.Errors.First().Description;
    }


}
