using Skeleton.Data.Context;
using Skeleton.Models.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace Skeleton.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();

            var identityDbContext = scope.ServiceProvider.GetRequiredService<DrawDbContext>();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

            identityDbContext.Database.Migrate();

            if (!userManager.Users.Any())
            {
                userManager.CreateAsync(new User() { UserName = "user1", Email = "user1@outlook.com" }, "Password12*").Wait();

                userManager.CreateAsync(new User() { UserName = "user2", Email = "user2@outlook.com" }, "Password12*").Wait();
                userManager.CreateAsync(new User() { UserName = "user3", Email = "user3@outlook.com" }, "Password12*").Wait();
                userManager.CreateAsync(new User() { UserName = "user4", Email = "user4@outlook.com" }, "Password12*").Wait();
                userManager.CreateAsync(new User() { UserName = "user5", Email = "user5@outlook.com" }, "Password12*").Wait();
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
