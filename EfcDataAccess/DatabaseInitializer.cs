using Domain.Models;

namespace EfcDataAccess;

public class DatabaseInitializer
{
    public static List<User> GetAuthenticationUsers()
    {
        return new List<User>
        {
                new User { Id = 1, Username = "Rolf", Password = "1234", Email = "user1@example.com", Name = "User One", Role = "Admin", Address = "Hallssti 29", Phone = "53299870", Sex = "male"},
                new User { Id = 2, Username = "Alecsen", Password = "1234", Email = "user2@example.com", Name = "User Two", Role = "User", Address = "Hallssti 29", Phone = "53299870", Sex = "male" },
                new User { Id = 3,  Username = "Maria", Password = "1234", Email = "user3@example.com", Name = "User Three", Role = "User", Address = "Hallssti 29", Phone = "53299870", Sex = "male" },
                new User { Id = 4,  Username = "Røde", Password = "1234", Email = "user4@example.com", Name = "User Four", Role = "Manager", Address = "Hallssti 29", Phone = "53299870", Sex = "male" },
                new User { Id = 5, Username = "user5", Password = "1234", Email = "user5@example.com", Name = "User Five", Role = "Manager", Address = "Hallssti 29", Phone = "53299870", Sex = "male" }
        };
    }

    public static List<Field> GetFields()
    {
        return new List<Field>
        {
            new Field {Id = 1, OwnerId = 1, Name = "RolfMark1", LocationData = "(-100.123, 50.456), (-100.789, 50.456), (-100.789, 50.123), (-100.123, 50.123)" },
            new Field {Id = 2, OwnerId = 1, Name = "RolfMark2", LocationData = "(-101.123, 51.456), (-101.789, 51.456), (-101.789, 51.123), (-101.123, 51.123)" },
            new Field {Id = 3, OwnerId = 2, Name = "AlecsenMark1", LocationData = "(-102.123, 52.456), (-102.789, 52.456), (-102.789, 52.123), (-102.123, 52.123)" },
            new Field {Id = 4, OwnerId = 2, Name = "AlecsenMark2", LocationData = "(-103.123, 53.456), (-103.789, 53.456), (-103.789, 53.123), (-103.123, 53.123)" },
            new Field {Id = 5, OwnerId = 3, Name = "MariasMark1", LocationData = "(-104.123, 54.456), (-104.789, 54.456), (-104.789, 54.123), (-104.123, 54.123)" },
            
        };
    }

    public static List<Sensor> GetSensors()
    {
        return new List<Sensor>
        {
            new Sensor {Id = 1,FieldId = 1, Longitude = -100.3, Latitude = 50.3, MoistureLevel = 50 },
            new Sensor {Id = 2,FieldId = 1, Longitude = -100.3, Latitude = 50.4, MoistureLevel = 55 },
            new Sensor {Id = 3,FieldId = 2, Longitude = -100.5, Latitude = 51.3, MoistureLevel = 60 },
            new Sensor {Id = 4,FieldId = 2, Longitude = -100.3, Latitude = 51.3, MoistureLevel = 65 },
            new Sensor {Id = 5,FieldId = 3, Longitude = -102.2, Latitude = 52.4, MoistureLevel = 50 },
            new Sensor {Id = 6, FieldId = 3, Longitude = -102.4, Latitude = 52.4, MoistureLevel = 55 },
            new Sensor {Id = 7,FieldId = 4, Longitude = -103.2, Latitude = 53.4, MoistureLevel = 60 },
            new Sensor {Id = 8,FieldId = 4, Longitude = -103.4, Latitude = 53.4, MoistureLevel = 65 },
            new Sensor {Id = 9,FieldId = 5, Longitude = -104.2, Latitude = 54.4, MoistureLevel = 50 },
            new Sensor {Id = 10,FieldId = 5, Longitude = -104.4, Latitude = 54.4, MoistureLevel = 55 },
        };
    }
    
     public static List<User> GetAuthenticationUsersNoId()
    {
        return new List<User>
        {
                new User {  Username = "Rolf", Password = "1234", Email = "user1@example.com", Name = "User One", Role = "Admin" },
                new User {  Username = "Alecsen", Password = "1234", Email = "user2@example.com", Name = "User Two", Role = "User" },
                new User {   Username = "Maria", Password = "1234", Email = "user3@example.com", Name = "User Three", Role = "User" },
                new User {   Username = "Røde", Password = "1234", Email = "user4@example.com", Name = "User Four", Role = "Manager" },
                new User {  Username = "user5", Password = "1234", Email = "user5@example.com", Name = "User Five", Role = "Manager" }
        };
    }

    public static List<Field> GetFieldsNoId()
    {
        return new List<Field>
        {
            new Field { OwnerId = 1, Name = "RolfMark1", LocationData = "(-100.123, 50.456), (-100.789, 50.456), (-100.789, 50.123), (-100.123, 50.123)" },
            new Field { OwnerId = 1, Name = "RolfMark2", LocationData = "(-101.123, 51.456), (-101.789, 51.456), (-101.789, 51.123), (-101.123, 51.123)" },
            new Field {OwnerId = 2, Name = "AlecsenMark1", LocationData = "(-102.123, 52.456), (-102.789, 52.456), (-102.789, 52.123), (-102.123, 52.123)" },
            new Field { OwnerId = 2, Name = "AlecsenMark2", LocationData = "(-103.123, 53.456), (-103.789, 53.456), (-103.789, 53.123), (-103.123, 53.123)" },
            new Field { OwnerId = 3, Name = "MariasMark1", LocationData = "(-104.123, 54.456), (-104.789, 54.456), (-104.789, 54.123), (-104.123, 54.123)" },
            
        };
    }

    public static List<Sensor> GetSensorsNoId()
    {
        return new List<Sensor>
        {
            new Sensor {FieldId = 1, Longitude = -100.3, Latitude = 50.3, MoistureLevel = 50 },
            new Sensor {FieldId = 1, Longitude = -100.3, Latitude = 50.4, MoistureLevel = 55 },
            new Sensor {FieldId = 2, Longitude = -100.5, Latitude = 51.3, MoistureLevel = 60 },
            new Sensor {FieldId = 2, Longitude = -100.3, Latitude = 51.3, MoistureLevel = 65 },
            new Sensor {FieldId = 3, Longitude = -102.2, Latitude = 52.4, MoistureLevel = 50 },
            new Sensor { FieldId = 3, Longitude = -102.4, Latitude = 52.4, MoistureLevel = 55 },
            new Sensor {FieldId = 4, Longitude = -103.2, Latitude = 53.4, MoistureLevel = 60 },
            new Sensor {FieldId = 4, Longitude = -103.4, Latitude = 53.4, MoistureLevel = 65 },
            new Sensor {FieldId = 5, Longitude = -104.2, Latitude = 54.4, MoistureLevel = 50 },
            new Sensor {FieldId = 5, Longitude = -104.4, Latitude = 54.4, MoistureLevel = 55 },
        };
    }
}