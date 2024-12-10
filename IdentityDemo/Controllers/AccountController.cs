using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using IdentityDemo.Models;
using IdentityDemo.Views.Account;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace IdentityDemo.Controllers;

public class AccountController(AccountService accountService) : Controller
{
    [Authorize]
    [HttpGet("members")]
    public IActionResult Members()
    {
        return View();
    }

    [HttpGet("")]
    [HttpGet("register")]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterVM viewModel)
    {
        if (!ModelState.IsValid)
            return View();

        // Try to register user
        var errorMessage = accountService.TryRegisterUserAsync(viewModel);
        //if (errorMessage != null)
        //{
        //    // Show error
        //    ModelState.AddModelError(string.Empty, errorMessage); //TODO
        //    return View();
        //}

        // Redirect user
        return RedirectToAction(nameof(Login));
    }

    [HttpGet("login")]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost("login")]
    public IActionResult Login(LoginVM viewModel)
    {
        if (!ModelState.IsValid)
            return View();

        // Check if credentials is valid (and set auth cookie)
        var errorMessage = accountService.TryLoginAsync(viewModel);
        //if (errorMessage != null)
        //{
        //    // Show error
        //    ModelState.AddModelError(string.Empty, errorMessage);
        //    return View();
        //}

        // Redirect user
        return RedirectToAction(nameof(Members));
    }
}
