using Microsoft.EntityFrameworkCore;
using Pacientes.DataBase.Utils;

namespace Pacientes.DataBase
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<DbContextAPI>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add services to the container.
            builder.Services.AddAuthorization();


            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();

            app.Run();
        }
    }
}
