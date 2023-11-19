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

            // Brug en service provider for at oprette og seede databasen
            using (var serviceProvider = services.BuildServiceProvider())
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<SmartFarmerAppContext>();

                    // Sørg for at databasen er oprettet
                    db.Database.EnsureCreated();

                    // Seed databasen med nogle testdata
                    SeedDatabase(db);
                }
            }
        });
    }

    private void SeedDatabase(SmartFarmerAppContext context)
    {
        DatabaseInitializer.GetAuthenticationUsersNoId().ToList().ForEach(user => context.Users.Add(user));
        DatabaseInitializer.GetFieldsNoId().ToList().ForEach(field => context.Fields.Add(field));
        DatabaseInitializer.GetSensorsNoId().ToList().ForEach(sensor => context.Sensors.Add(sensor));

        context.SaveChanges();
    }
}
