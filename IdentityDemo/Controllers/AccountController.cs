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
        return View();
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
        return RedirectToAction(nameof(Login));
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


}
