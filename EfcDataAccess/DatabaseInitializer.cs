using Domain.Models;

namespace EfcDataAccess;

public class DatabaseInitializer
{
    public static List<User> GetAuthenticationUsers()
    {
        return new List<User>
        {
                new User { Id = 1, Username = "Rolf", Password = "1234", Email = "user1@example.com", Name = "User One", Role = "Admin", Address = "Hallssti 29", Birthday = new DateTime(1998, 4, 19), Phone = "53299870", Sex = "male"},
                new User { Id = 2, Username = "Alecsen", Password = "1234", Email = "user2@example.com", Name = "User Two", Role = "User", Address = "Hallssti 29", Birthday = new DateTime(1998, 4, 19), Phone = "53299870", Sex = "male" },
                new User { Id = 3,  Username = "Maria", Password = "1234", Email = "user3@example.com", Name = "User Three", Role = "User", Address = "Hallssti 29", Birthday = new DateTime(1998, 4, 19), Phone = "53299870", Sex = "male" },
                new User { Id = 4,  Username = "Røde", Password = "1234", Email = "user4@example.com", Name = "User Four", Role = "Manager", Address = "Hallssti 29", Birthday = new DateTime(1998, 4, 19), Phone = "53299870", Sex = "male" },
                new User { Id = 5, Username = "user5", Password = "1234", Email = "user5@example.com", Name = "User Five", Role = "Manager", Address = "Hallssti 29", Birthday = new DateTime(1998, 4, 19), Phone = "53299870", Sex = "male" }
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
    
     public static List<User> GetAuthenticationUsersNoId()
    {
        return new List<User>
        {
                new User {  Username = "Rolf", Password = "1234", Email = "user1@example.com", Name = "User One", Role = "Admin", Birthday = new DateTime(1998, 4, 19), Phone = "53299870", Sex = "male" ,Address = "Hallssti 29"},
                new User {  Username = "Alecsen", Password = "1234", Email = "user2@example.com", Name = "User Two", Role = "User", Birthday = new DateTime(1998, 4, 19), Phone = "53299870", Sex = "male",Address = "Hallssti 29" },
                new User {   Username = "Maria", Password = "1234", Email = "user3@example.com", Name = "User Three", Role = "User", Birthday = new DateTime(1998, 4, 19), Phone = "53299870", Sex = "male" ,Address = "Hallssti 29"},
                new User {   Username = "Røde", Password = "1234", Email = "user4@example.com", Name = "User Four", Role = "Manager", Birthday = new DateTime(1998, 4, 19), Phone = "53299870", Sex = "male",Address = "Hallssti 29" },
                new User {  Username = "user5", Password = "1234", Email = "user5@example.com", Name = "User Five", Role = "Manager", Birthday = new DateTime(1998, 4, 19), Phone = "53299870", Sex = "male",Address = "Hallssti 29" }
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
    
}