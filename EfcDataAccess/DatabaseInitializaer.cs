using Domain.Models;

namespace EfcDataAccess;

public class DatabaseInitializaer
{
    public static List<AuthenticationUser> GetAuthenticationUsers()
    {
        return new List<AuthenticationUser>
        {
                new AuthenticationUser { Username = "Rolf", Password = "1234", Email = "user1@example.com", Domain = "Domain1", Name = "User One", Role = "Admin", Age = 30, SecurityLevel = 1 },
                new AuthenticationUser { Username = "Alecsen", Password = "1234", Email = "user2@example.com", Domain = "Domain2", Name = "User Two", Role = "User", Age = 25, SecurityLevel = 2 },
                new AuthenticationUser {  Username = "Maria", Password = "1234", Email = "user3@example.com", Domain = "Domain3", Name = "User Three", Role = "User", Age = 28, SecurityLevel = 2 },
                new AuthenticationUser {  Username = "Røde", Password = "1234", Email = "user4@example.com", Domain = "Domain4", Name = "User Four", Role = "Manager", Age = 32, SecurityLevel = 3 },
                new AuthenticationUser {  Username = "user5", Password = "1234", Email = "user5@example.com", Domain = "Domain5", Name = "User Five", Role = "Manager", Age = 35, SecurityLevel = 3 }
        };
    }

    public static List<Field> GetFields()
    {
        return new List<Field>
        {
            new Field { OwnerId = 1, Name = "Field1", LocationData = "(-100.123, 50.456), (-100.789, 50.456), (-100.789, 50.123), (-100.123, 50.123)" },
            new Field { OwnerId = 1, Name = "Field2", LocationData = "(-101.123, 51.456), (-101.789, 51.456), (-101.789, 51.123), (-101.123, 51.123)" },
            new Field { OwnerId = 2, Name = "Field3", LocationData = "(-102.123, 52.456), (-102.789, 52.456), (-102.789, 52.123), (-102.123, 52.123)" },
            new Field { OwnerId = 2, Name = "Field4", LocationData = "(-103.123, 53.456), (-103.789, 53.456), (-103.789, 53.123), (-103.123, 53.123)" },
            new Field { OwnerId = 3, Name = "Field5", LocationData = "(-104.123, 54.456), (-104.789, 54.456), (-104.789, 54.123), (-104.123, 54.123)" },
        };
    }

    public static List<Sensor> GetSensors()
    {
        return new List<Sensor>
        {
            new Sensor
            {
                Id = 1, 
                // ... other properties
            },
            // ... more sensors
        };
    }
}