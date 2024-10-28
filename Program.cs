using Microsoft.EntityFrameworkCore;
using FinancialTracker.Data;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace FinancialTracker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure Entity Framework with PostgreSQL
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
               options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add services to the container
            builder.Services.AddRazorPages();
            // Configure authentication
            builder.Services.AddAuthentication("MyAuth")
                .AddCookie("MyAuth", options =>
                {
                    options.LoginPath = "/Login"; // Redirect to the login page if not authenticated
                    options.LogoutPath = "/Logout"; // Redirect to the logout page
                });

            // Add session support
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Session middleware should be before authentication and authorization
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
