using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Pacientes.DataBase.Models.Usuarios;
using System;

namespace Pacientes.DataBase.Utils.DataSeed
{
    public class DataSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DbContextAPI>();

            await context.Database.MigrateAsync();

            if (!await context.Usuarios.AnyAsync())
            {
                var admin = new Usuarios
                {
                    Usuario = "admin",
                    Contrasenia = BCrypt.Net.BCrypt.HashPassword("Admin123*"),
                    Rol = "Admin"
                };
                context.Usuarios.Add(admin);
                await context.SaveChangesAsync();
            }
        }
    }
}
