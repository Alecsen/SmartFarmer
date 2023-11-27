using EfcDataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace SmartFarmer.IntegrationTest;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<SmartFarmerAppContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<SmartFarmerAppContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
            });
        });
    }

    public void SeedDatabase()
    {
        using var scope = Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<SmartFarmerAppContext>();
        
        // Tjek først, om databasen allerede er seedet
        if (!db.Users.Any())
        {
            DatabaseInitializer.GetAuthenticationUsersNoId().ToList().ForEach(user => db.Users.Add(user));
            DatabaseInitializer.GetFieldsNoId().ToList().ForEach(field => db.Fields.Add(field));
            db.SaveChanges();
        }
    }
}
