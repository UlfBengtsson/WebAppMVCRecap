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
            var SignInResultr = await _signInManager.PasswordSignInAsync(username, password, false, false);

            switch (SignInResultr.ToString())
            {
                case "Succeeded":
                    return RedirectToAction("Index", "Home");

                case "Failed":
                    ViewBag.msg = "Failed - Username of/and Password is incorrect";
                    break;
                case "Lockedout":
                    ViewBag.msg = "Locked Out";
                    break;
                default:
                    ViewBag.msg = SignInResultr.ToString();
                    break;
            }

            return View();

            /*
             * Alternative way to use SignInResult
             * 
            if (SignInResultr.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (SignInResultr.IsLockedOut)
            {
                ViewBag.msg = "Locked Out";
            }
            else if (SignInResultr.RequiresTwoFactor)
            {
                ViewBag.msg = "Twofactor authorizon requierd";
            }
            else if (SignInResultr.IsNotAllowed)
            {
                ViewBag.msg = "Not allowed to login";
            }
            else//Failed
            {
                ViewBag.msg = "Failed - Username of/and Password is incorrect";
            }
            */
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