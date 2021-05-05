using DemoCiCd.Web.Controllers;
using DemoCiCd.Web.DbAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DemoCiCd.Test
{
    
    public class DemoIntegrationTest
    {
        [Fact]
        public async Task SqlConnectionTest()
        {
            // Create DB Context
            var configuration = new ConfigurationBuilder()
                                  .AddJsonFile("appsettings.json")
                                  .AddEnvironmentVariables()
                                  .Build();

            var optionsBuilder = new DbContextOptionsBuilder<DemoDbContext>();
            optionsBuilder.UseSqlServer(configuration["ConnectionStrings:DemoDbConnection"]);

            var context = new DemoDbContext(optionsBuilder.Options);

            // Just to make sure: Delete all existing data in the database
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            // Create Controller
            var empController = new EmployeeController(context);

            // Add Employee
            await empController.Add(new Employee { Name = "Menaka" });

            // Check: Does GetAll returns the added Employee
            var result = (await empController.GetAll()).ToArray();

            Assert.Single(result);
            Assert.Equal("Menaka", result[0].Name );
        }
    }
}
