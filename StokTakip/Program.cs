using Microsoft.EntityFrameworkCore;
using StokTakip.Data;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace StokTakip
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

          
            builder.Services.AddRazorPages(options =>
            {
               
                options.Conventions.AuthorizeFolder("/");

                
                options.Conventions.AllowAnonymousToPage("/Login");

               
                options.Conventions.AllowAnonymousToPage("/Error");
            });

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite("Data Source=stoktakip.db"));

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Login";
                    options.LogoutPath = "/Logout";
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
