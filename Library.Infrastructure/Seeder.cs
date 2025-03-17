using Library.Domain.Main;
using Library.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure
{
    public class Seeder
    {
        public static async Task SeedAdmin(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<DataContext>()!;
            var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
            {
                await context.Database.MigrateAsync();
            }
            if(!await context.Admins.AnyAsync())
            {
                var admin = new AdminEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Admin",
                    Password = PasswordHelper.HashPassword("admin"),
                };
                context.Add(admin);
                await context.SaveChangesAsync();
            }
        }
    }
}
