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
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });


            builder.Services.AddAuthentication("cookie").AddCookie("cookie", a =>
            {
                a.LoginPath = "/account/login";
                a.LoginPath = "/account/accessdenied";
                a.LoginPath = "/account/logout";

            }
            );
            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<Lab_4_DB>();

                var retryCount = 0;
                var maxRetries = 20; // Maximum number of retries
                var delayBetweenRetries = TimeSpan.FromSeconds(20); // Time to wait between retries

                while (true)
                {
                    try
                    {
                        Console.WriteLine("Attempting to apply database migrations...");
                        dbContext.Database.Migrate();
                        Console.WriteLine("Database migrations applied successfully.");
                        break; // Exit the loop if migration is successful
                    }
                    catch (Exception ex)
                    {
                        retryCount++;
                        Console.WriteLine($"An error occurred while applying database migrations: {ex.Message}");

                        if (retryCount >= maxRetries)
                        {
                            throw; // Rethrow the exception if the maximum number of retries is exceeded
                        }

                        Console.WriteLine($"Waiting {delayBetweenRetries.TotalSeconds} seconds before retrying...");
                        Thread.Sleep(delayBetweenRetries); // Wait before retrying
                    }
                }
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}