using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FinancialTracker.Data;
using FinancialTracker.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace FinancialTracker.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public LoginModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = _context.users.FirstOrDefault(u => u.username == Username);

            if (user != null && user.passwordhash == Password)
            {
                // Set session and authentication cookie
                HttpContext.Session.SetInt32("userid", user.id);

                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, user.username),
                    new Claim(ClaimTypes.NameIdentifier, user.id.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, "MyAuth");
                await HttpContext.SignInAsync("MyAuth", new ClaimsPrincipal(claimsIdentity));

                return RedirectToPage("/Index");
            }

            // If authentication fails
            ErrorMessage = "Invalid username or password.";
            return Page();
        }
    }
}
