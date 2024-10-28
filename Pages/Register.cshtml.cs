using FinancialTracker.Data;
using FinancialTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinancialTracker.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public RegisterModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    username = Username,
                    email = Email,
                    passwordhash = Password,
                    createdat = DateTime.UtcNow,
                    updatedat = DateTime.UtcNow 
                };

                if (await _context.users.AnyAsync(u => u.username == user.username || u.email == user.email))
                {
                    ErrorMessage = "Username or Email already exists.";
                    return Page();
                }

                _context.users.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Login");
            }
            return Page();
        }
    }
}
