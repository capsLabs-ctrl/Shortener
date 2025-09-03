using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace site_test_task.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {   
            var basePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "site-test-task");
            var config = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            Console.WriteLine("Connection string: " + config.GetConnectionString("DefaultConnection"));
            Console.WriteLine(basePath);
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseMySql(config.GetConnectionString("DefaultConnection"),
                new MySqlServerVersion(new Version(10, 3)));

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
