using LiveChat.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Data
{
    public static class ApplicationDbSeeder
    {
        public async static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<StoredUser>>();
            var result=context.Database.EnsureCreated();
            if (result)
            {
                if (!context.Users.Any())
                {
                    var adminUser = new StoredUser()
                    {
                        UserName="admin",
                        Email="admin@admin",
                        EmailConfirmed=true,
                    };
                    var resultCreate= await userManager.CreateAsync(adminUser,"Admin123!");
                    if (resultCreate.Succeeded)
                    {

                    }
                }
            }
        }
    }
}
