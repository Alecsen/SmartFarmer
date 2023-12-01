using Application.DAOInterface;
using Application.LogicInterface;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class FieldLogic : IFieldLogic
{
    private readonly IFieldDao fieldDao;
    private readonly IUserDao userDao;
    private readonly IWeatherStationDao weatherStationDao;

    public FieldLogic(IFieldDao fieldDao, IUserDao userDao, IWeatherStationDao weatherStationDao)
    {
        this.fieldDao = fieldDao;
        this.userDao = userDao;
        this.weatherStationDao = weatherStationDao;
    }

    public Task<IEnumerable<FieldLookupDto>> GetAsync(int ownerId)
    {
        if (ownerId == -1)
        {
            throw new Exception($"The Id {ownerId} is not a valid number");
        }
        
        return fieldDao.GetFieldsByOwnerId(ownerId);
    }

    public async Task<Field> CreateAsync(FieldCreationDto dto)
    {
        //checking if the user exists
        await userDao.GetByUserIdAsync(dto.OwnerId);

        if (dto.LocationData == null)
        {
            throw new Exception("There is not location data so field cannot be created");
        }
        
        Field field = new Field
        {
            Name = dto.FieldName,
            OwnerId = dto.OwnerId,
            LocationData = dto.LocationData,
            CropType = dto.CropType,
            ImportanceLevel = dto.ImportanceLevel,
            SoilType = dto.SoilType,
            FieldCapacity = dto.FieldCapacity,
            Area = CalculatePolygonArea(dto.LocationData)
        };
        var created = await fieldDao.CreateAsync(field);

        await weatherStationDao.CreateWeatherStationAsync(created.Id);
        
        return created;
    }
    
    private static double CalculatePolygonArea(string coordinatesString)
{
    // Parse the input string to extract coordinates
    IList<MapPoint> coordinates = ParseCoordinatesString(coordinatesString);

    double area = 0;

    if (coordinates.Count > 2)
    {
        for (var i = 0; i < coordinates.Count - 1; i++)
        {
            MapPoint p1 = coordinates[i];
            MapPoint p2 = coordinates[i + 1];
            area += ConvertToRadian(p2.Longitude - p1.Longitude) * (2 + Math.Sin(ConvertToRadian(p1.Latitude)) + Math.Sin(ConvertToRadian(p2.Latitude)));
        }

        area = area * 6378137 * 6378137 / 2;
    }

    double absoluteArea = Math.Abs(area);

    return absoluteArea;


}

    private static IList<MapPoint> ParseCoordinatesString(string coordinatesString)
{
        List<MapPoint> coordinates = new List<MapPoint>();

        // Split the input string into individual coordinate pairs
        string[] pairs = coordinatesString.Split(new[] { "), (" }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var pair in pairs)
        {
            // Remove parentheses and split the pair into latitude and longitude
            string[] values = pair.Replace("(", "").Replace(")", "").Split(new[] { ", " }, StringSplitOptions.None);

            // Parse latitude and longitude and add to the coordinates list
            double latitude = double.Parse(values[1]);  // Latitude is the second value
            double longitude = double.Parse(values[0]); // Longitude is the first value

            coordinates.Add(new MapPoint(latitude, longitude));
        }

        return coordinates;
    }

    private static double ConvertToRadian(double input)
    {
        return input * Math.PI / 180;
    }

    // Assuming you have a MapPoint class defined as follows:
    public class MapPoint
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public MapPoint(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
    
}