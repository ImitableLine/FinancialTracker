using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;

namespace FinancialTracker.Pages
{
    public class LogoutModel : PageModel
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync()
        {
            await HttpContext.SignOutAsync("MyAuth");
            HttpContext.Session.Clear(); 
            return RedirectToPage("/Index"); 
        }
    }
}
