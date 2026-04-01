using Microsoft.AspNetCore.Identity;
using Versus.Models;

namespace Versus.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var contexto = serviceProvider.GetRequiredService<ApplicationDbContext>();
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

        contexto.Database.EnsureCreated();

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

        if (!contexto.Categorias.Any())
        {
            var categorias = new List<Categoria>
            {
                new() { Nombre = "Peso Ligero", Descripcion = "Hasta 60 kg", PesoMaximo = 60 },
                new() { Nombre = "Peso Wélter", Descripcion = "61-70 kg", PesoMinimo = 61, PesoMaximo = 70 },
                new() { Nombre = "Peso Medio", Descripcion = "71-80 kg", PesoMinimo = 71, PesoMaximo = 80 },
                new() { Nombre = "Peso Pesado", Descripcion = "Más de 80 kg", PesoMinimo = 81 },
                new() { Nombre = "Junior", Descripcion = "Menores de 18 años", EdadMaxima = 17 },
            };
            contexto.Categorias.AddRange(categorias);
            await contexto.SaveChangesAsync();
        }

        if (!contexto.Competidores.Any())
        {
            var ligeroId = contexto.Categorias.First(c => c.Nombre == "Peso Ligero").Id;
            var welterId = contexto.Categorias.First(c => c.Nombre == "Peso Wélter").Id;
            var medioId = contexto.Categorias.First(c => c.Nombre == "Peso Medio").Id;
            var pesadoId = contexto.Categorias.First(c => c.Nombre == "Peso Pesado").Id;

            var competidores = new List<Competidor>
            {
                new() { Nombre = "Carlos Mendoza", Edad = 25, Peso = 58.5, CategoriaId = ligeroId },
                new() { Nombre = "Juan García", Edad = 22, Peso = 59.0, CategoriaId = ligeroId },
                new() { Nombre = "Pedro López", Edad = 28, Peso = 57.5, CategoriaId = ligeroId },
                new() { Nombre = "Miguel Torres", Edad = 24, Peso = 60.0, CategoriaId = ligeroId },
                new() { Nombre = "Andrés Ríos", Edad = 26, Peso = 65.0, CategoriaId = welterId },
                new() { Nombre = "Luis Herrera", Edad = 23, Peso = 68.5, CategoriaId = welterId },
                new() { Nombre = "Diego Castro", Edad = 27, Peso = 70.0, CategoriaId = welterId },
                new() { Nombre = "Roberto Vargas", Edad = 29, Peso = 75.0, CategoriaId = medioId },
                new() { Nombre = "Felipe Morales", Edad = 25, Peso = 78.0, CategoriaId = medioId },
                new() { Nombre = "Sebastián Jiménez", Edad = 30, Peso = 85.0, CategoriaId = pesadoId },
                new() { Nombre = "Nicolás Romero", Edad = 32, Peso = 90.0, CategoriaId = pesadoId },
                new() { Nombre = "Mateo Flores", Edad = 28, Peso = 88.0, CategoriaId = pesadoId },
            };
            contexto.Competidores.AddRange(competidores);
            await contexto.SaveChangesAsync();
        }

        if (!contexto.Competencias.Any())
        {
            var ligeroId = contexto.Categorias.First(c => c.Nombre == "Peso Ligero").Id;
            var competencias = new List<Competencia>
            {
                new() { Nombre = "Copa Versus 2024 - Ligero", Fecha = new DateTime(2024, 6, 15), Lugar = "Ciudad de México", CategoriaId = ligeroId },
            };
            contexto.Competencias.AddRange(competencias);
            await contexto.SaveChangesAsync();
        }
    }
}
