using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Vidly.Models;

namespace Vidly
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add Connection String in ASP.NET 6 WITH ENTITYFRAMEWORKCORE
            // Connection string has to be added in appsettings.json file.
            string connString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<VidlyContext>(options => options.UseSqlServer(connString));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Add Auto Mapper
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddControllersWithViews();

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

            app.UseAuthorization();

            // This method maps the attribute routes in ASP.NET 6
            app.MapControllers();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}