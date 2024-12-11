using IdentityDemo.Models;
using IdentityDemo.Views.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityDemo.Controllers;

public class AccountController(AccountService accountService) : Controller
{
    [Authorize]
    [HttpGet("members")]
    public IActionResult Members()
    {
        var model = accountService.GetMembers();
        return View(model);
    }

    [HttpGet("")]
    [HttpGet("register")]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost("register")] //Ändrade till async
    public async Task<IActionResult> Register(RegisterVM viewModel)
    {
        if (!ModelState.IsValid)
            return View();

        var errorMessage = await accountService.TryRegisterUserAsync(viewModel);
        if (errorMessage != null)
        {
            ModelState.AddModelError(string.Empty, errorMessage);
            return View();
        }

        await accountService.TryLoginAfterRegisterAsync(viewModel);

        return RedirectToAction(nameof(Members));
    }

    [HttpGet("login")]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginVM viewModel)
    {
        if (!ModelState.IsValid)
            return View();

        var errorMessage = await accountService.TryLoginAsync(viewModel);
        if (errorMessage != null)
        {
            ModelState.AddModelError(string.Empty, errorMessage);
            return View();
        }

        return RedirectToAction(nameof(Members));
    }

    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        accountService.LogoutAsync();
        return RedirectToAction(nameof(Login));
    }

    [HttpGet("update")]
    public async Task<IActionResult> Update()
    {
        var model = await accountService.LoggedInUserToUpdateVm();
        return View(model);
    }
    [HttpPost("update")]
    public async Task<IActionResult> Update(UpdateVm model)
    {
        if (!ModelState.IsValid)
            return View(model);
        var errorMessage = await accountService.UpdateUser(model);
        if (errorMessage != null)
        {
            ModelState.AddModelError(string.Empty, errorMessage);
            return View();
        }
        return RedirectToAction(nameof(Members));
    }
}
