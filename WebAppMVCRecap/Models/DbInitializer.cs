using Microsoft.AspNetCore.Identity;
using System;
using WebAppMVCRecap.Models;

namespace WebAppMVCRecap
{
    internal class DbInitializer
    {
        internal static void Initialize(AppDbContext context, Microsoft.AspNetCore.Identity.RoleManager<Microsoft.AspNetCore.Identity.IdentityRole> roleManager, Microsoft.AspNetCore.Identity.UserManager<Microsoft.AspNetCore.Identity.IdentityUser> userManager)
        {

            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole("Admin");

                roleManager.CreateAsync(role).Wait();
            }

            if (!roleManager.RoleExistsAsync("NormalUser").Result)
            {
                IdentityRole role = new IdentityRole("NormalUser");

                roleManager.CreateAsync(role).Wait();
            }

            //-------------------------------------------------------------------

            if (userManager.FindByNameAsync("Guru").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.Email = "guru@admin.se";
                user.UserName = "Guru";

                IdentityResult result = userManager.CreateAsync(user, "Password!123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }

            if (userManager.FindByNameAsync("Sven").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.Email = "sven@admin.se";
                user.UserName = "Sven";

                IdentityResult result = userManager.CreateAsync(user, "Password!123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "NormalUser").Wait();
                }
            }

            //-----------------------------------------------------------------------
            
            // Don´t forget to save
            //context.SaveChanges();
        }
    }
}