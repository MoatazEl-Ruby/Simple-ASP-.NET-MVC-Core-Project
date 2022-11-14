using ASP.NET_Lab_4.Data;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Lab_4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<Lab_4_DB>(
                a =>
                {
                    a.UseSqlServer(builder.Configuration.GetConnectionString("con1"));
                }
                );

            builder.Services.AddAuthentication("cookie").AddCookie("cookie", a =>
            {
                a.LoginPath = "/account/login";
                a.LoginPath = "/account/accessdenied";
                a.LoginPath = "/account/logout";

            }
            );
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}