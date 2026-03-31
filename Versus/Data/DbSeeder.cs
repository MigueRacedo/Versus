using Microsoft.AspNetCore.Identity;
using Versus.Models;

namespace Versus.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

        context.Database.EnsureCreated();

        if (!userManager.Users.Any())
        {
            var admin = new IdentityUser
            {
                UserName = "admin@versus.com",
                Email = "admin@versus.com",
                EmailConfirmed = true
            };
            await userManager.CreateAsync(admin, "Admin@123");
        }

        if (!context.Categories.Any())
        {
            var categories = new List<Category>
            {
                new() { Name = "Lightweight", Description = "Up to 60 kg", MaxWeight = 60 },
                new() { Name = "Welterweight", Description = "61-70 kg", MinWeight = 61, MaxWeight = 70 },
                new() { Name = "Middleweight", Description = "71-80 kg", MinWeight = 71, MaxWeight = 80 },
                new() { Name = "Heavyweight", Description = "Over 80 kg", MinWeight = 81 },
                new() { Name = "Junior", Description = "Under 18 years", MaxAge = 17 },
            };
            context.Categories.AddRange(categories);
            await context.SaveChangesAsync();
        }

        if (!context.Competitors.Any())
        {
            var lightweightId = context.Categories.First(c => c.Name == "Lightweight").Id;
            var welterweightId = context.Categories.First(c => c.Name == "Welterweight").Id;
            var middleweightId = context.Categories.First(c => c.Name == "Middleweight").Id;
            var heavyweightId = context.Categories.First(c => c.Name == "Heavyweight").Id;

            var competitors = new List<Competitor>
            {
                new() { Name = "Carlos Mendoza", Age = 25, Weight = 58.5, CategoryId = lightweightId },
                new() { Name = "Juan García", Age = 22, Weight = 59.0, CategoryId = lightweightId },
                new() { Name = "Pedro López", Age = 28, Weight = 57.5, CategoryId = lightweightId },
                new() { Name = "Miguel Torres", Age = 24, Weight = 60.0, CategoryId = lightweightId },
                new() { Name = "Andrés Ríos", Age = 26, Weight = 65.0, CategoryId = welterweightId },
                new() { Name = "Luis Herrera", Age = 23, Weight = 68.5, CategoryId = welterweightId },
                new() { Name = "Diego Castro", Age = 27, Weight = 70.0, CategoryId = welterweightId },
                new() { Name = "Roberto Vargas", Age = 29, Weight = 75.0, CategoryId = middleweightId },
                new() { Name = "Felipe Morales", Age = 25, Weight = 78.0, CategoryId = middleweightId },
                new() { Name = "Sebastián Jiménez", Age = 30, Weight = 85.0, CategoryId = heavyweightId },
                new() { Name = "Nicolás Romero", Age = 32, Weight = 90.0, CategoryId = heavyweightId },
                new() { Name = "Mateo Flores", Age = 28, Weight = 88.0, CategoryId = heavyweightId },
            };
            context.Competitors.AddRange(competitors);
            await context.SaveChangesAsync();
        }

        if (!context.Competitions.Any())
        {
            var lightweightId = context.Categories.First(c => c.Name == "Lightweight").Id;
            var competitions = new List<Competition>
            {
                new() { Name = "Copa Versus 2024 - Ligero", Date = new DateTime(2024, 6, 15), Location = "Ciudad de México", CategoryId = lightweightId },
            };
            context.Competitions.AddRange(competitions);
            await context.SaveChangesAsync();
        }
    }
}
