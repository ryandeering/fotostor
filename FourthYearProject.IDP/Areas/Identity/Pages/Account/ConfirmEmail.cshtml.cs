using FourthYearProject.IDP.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace FourthYearProject.IDP.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _configuration;

        public ConfirmEmailModel(UserManager<ApplicationUser> userManager, IWebHostEnvironment configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [TempData] public string StatusMessage { get; set; }

        [TempData] public string Link { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null) return RedirectToPage("/Index");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound($"Unable to load user with ID '{userId}'.");

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (_configuration.IsDevelopment())
            {
                StatusMessage = result.Succeeded ? "Thank you for confirming your email. Go to the following link to login." : "Error confirming your email.";
                Link = "https://localhost:44366";
            }
            else
            {
                StatusMessage = result.Succeeded ? "Thank you for confirming your email. Go to the following link to login." : "Error confirming your email.";
                Link = "https://red-pebble-0ad568c03.azurestaticapps.net";
            }
            
            return Page();
        }
    }
}