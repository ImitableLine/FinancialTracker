using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FinancialTracker.Models;
using FinancialTracker.Data;
using Microsoft.Extensions.Configuration.UserSecrets;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using FinancialTransaction = FinancialTracker.Models.Transaction;

namespace FinancialTracker.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public User LoggedInUser { get; set; }
        public List<Account> Accounts { get; set; } = new List<Account>();
        public List<Budget> Budgets { get; set; } = new List<Budget>();
        public List<Reminder> Reminders { get; set; } = new List<Reminder>();
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["userid"] = HttpContext.Session.GetInt32("userid");
            int? userId = HttpContext.Session.GetInt32("userid");

            if (userId.HasValue)
            {
                LoggedInUser = await _context.users
                    .Include(u => u.accounts)
                    .Include(u => u.budgets)
                    .Include(u => u.reminders)
                    .Include(u => u.transactions)
                    .FirstOrDefaultAsync(u => u.id == userId.Value);

                if (LoggedInUser == null)
                {
                    return RedirectToPage("/Account/Login");
                }

                // Populate lists from the logged-in user
                Accounts = LoggedInUser.accounts.ToList();
                Budgets = LoggedInUser.budgets.ToList();
                Reminders = LoggedInUser.reminders.ToList();
                Transactions = Accounts.SelectMany(a => a.transactions).ToList();

                // Fetch budgets for the user if needed
                var budgets = await _context.budgets.Where(b => b.userid == userId).ToListAsync();
                LoggedInUser.budgets = budgets;
            }

            return Page();
        }


        public async Task<IActionResult> OnPostDeleteAccountAsync(int accountId)
        {
            var account = await _context.accounts.FindAsync(accountId);

            if (account != null)
            {
                // Remove the account from the database
                _context.accounts.Remove(account);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSaveAsync(int accountId, string accountName, string accountType, decimal balance)
        {
            var account = await _context.accounts.FindAsync(accountId);

            if (account != null)
            {
                account.accountname = accountName;
                account.accounttype = accountType;
                account.balance = balance;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }



        public async Task<IActionResult> OnPostBudgetDeleteAsync(int budgetId)
        {
            var budget = await _context.budgets.FindAsync(budgetId);

            if (budget != null)
            {
                _context.budgets.Remove(budget);
                await _context.SaveChangesAsync();
            }

            
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostTransactionDeleteAsync(int transactionId)
        {
            var transaction_delete = await _context.transactions.FindAsync(transactionId);

            if (transaction_delete != null)
            {
                _context.transactions.Remove(transaction_delete);
                await _context.SaveChangesAsync();
            }


            return RedirectToPage();
        }


        public async Task<IActionResult> OnPostSaveBudgetAsync(int budgetId, string category, decimal amount, DateTime startDate, DateTime endDate)
        {
            var budget = await _context.budgets.FindAsync(budgetId);

            if (budget != null)
            {
                budget.category = category;
                budget.amount = amount;
                budget.startdate = DateTime.SpecifyKind(startDate, DateTimeKind.Utc);
                budget.enddate = DateTime.SpecifyKind(endDate, DateTimeKind.Utc);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostCreateAccountAsync(string accountName, string accountType, decimal balance)
        {
            int? userId = HttpContext.Session.GetInt32("userid");
            if (userId.HasValue && !string.IsNullOrWhiteSpace(accountName) && !string.IsNullOrWhiteSpace(accountType))
            {
                var newAccount = new Account
                {
                    userid = userId.Value,
                    accountname = accountName,
                    accounttype = accountType,
                    balance = balance,
                    createdat = DateTime.UtcNow,
                    updatedat = DateTime.UtcNow
                };

                _context.accounts.Add(newAccount);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }


        public async Task<IActionResult> OnPostCreateBudgetAsync(string category, decimal amount, DateTime startDate, DateTime endDate)
        {
            int? userId = HttpContext.Session.GetInt32("userid");
            if (userId.HasValue && !string.IsNullOrWhiteSpace(category) && amount > 0)
            {
                var newBudget = new Budget
                {
                    userid = userId.Value,
                    category = category,
                    amount = amount,
                    startdate = DateTime.SpecifyKind(startDate, DateTimeKind.Utc),
                    enddate = DateTime.SpecifyKind(endDate, DateTimeKind.Utc),
                    createdat = DateTime.UtcNow,
                    updatedat = DateTime.UtcNow
                };

                _context.budgets.Add(newBudget);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }



        public async Task<IActionResult> OnPostAddTransactionAsync(int accountId, string category, decimal amount, DateTime date)
        {
            var account = await _context.accounts.FindAsync(accountId);
            var userId = (int)HttpContext.Session.GetInt32("userid");

            if (account == null)
            {
                ModelState.AddModelError(string.Empty, "Account not found.");
                return Page();
            }

            account.balance += amount;

            var newTransaction = new FinancialTracker.Models.Transaction
            {
                accountid = accountId,
                category = category,
                amount = amount,
                date = DateTime.UtcNow,
                transactiontype = amount >= 0 ? "Credit" : "Debit",
                createdat = DateTime.UtcNow,
                updatedat = DateTime.UtcNow,
                userid = userId
            };

            _context.transactions.Add(newTransaction);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }




    }
}
