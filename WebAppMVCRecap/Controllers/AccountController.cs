using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAppMVCRecap.Models;

namespace WebAppMVCRecap.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, 
                                 SignInManager<IdentityUser> signInManager)// Constructor Injection´s
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignIn(string username, string password)
        {
            IdentityUser user = new IdentityUser() { UserName = username };

            Microsoft.AspNetCore.Identity.SignInResult SignInResultr = await _signInManager.PasswordSignInAsync(user, password, false, false);

            if (SignInResultr == Microsoft.AspNetCore.Identity.SignInResult.Success)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.msg = true;

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]//temp
        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }
        [AllowAnonymous]//temp
        [HttpPost]
        public async Task<IActionResult> CreateUser(string username, string email, string password)
        {
            IdentityUser user = new IdentityUser() { UserName = username, Email = email };
            await _userManager.CreateAsync(user, password);

            return View();
        }




    }
}