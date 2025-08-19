using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Pacientes.DataBase.Utils
{
    public class DbContextAPIFactory : IDesignTimeDbContextFactory<DbContextAPI>
    {
        public DbContextAPI CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<DbContextAPI>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new DbContextAPI(optionsBuilder.Options);
        }
    }
}
