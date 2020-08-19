using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using PickupAnnouncer.Models;

namespace PickupAnnouncer.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IConfiguration configuration;
        public LoginModel(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [BindProperty]
        public string UserName { get; set; }
        [BindProperty, DataType(DataType.Password)]
        public string Password { get; set; }
        public string Message { get; set; }
        public async Task<IActionResult> OnPost()
        {
            var siteConfig = configuration.GetSection("SiteConfig").Get<SiteConfig>();

            if (UserName == siteConfig.AdminUser && Password == siteConfig.AdminPass)
            {
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, UserName),
                        new Claim(ClaimTypes.Role, "Admin")
                    };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return RedirectToPage("/Admin");
            }
            Message = "Invalid attempt";
            return Page();
        }
    }
}