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
    
    static double CalculatePolygonArea(string coordinatesString)
    {
        // Parse the input string to extract coordinates
        List<(double, double)> coordinates = ParseCoordinatesString(coordinatesString);

        // Apply the shoelace formula to calculate the area
        double area = ShoelaceFormula(coordinates);

        // Convert the area to acres (1 square meter is approximately 0.000247105 acres)
        double areaInAcres = area * 0.000247105;

        return areaInAcres;
    }

    static List<(double, double)> ParseCoordinatesString(string coordinatesString)
    {
        List<(double, double)> coordinates = new List<(double, double)>();

        // Split the input string into individual coordinate pairs
        string[] pairs = coordinatesString.Split(new[] { "), (" }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var pair in pairs)
        {
            // Remove parentheses and split the pair into latitude and longitude
            string[] values = pair.Replace("(", "").Replace(")", "").Split(new[] { ", " }, StringSplitOptions.None);

            // Parse latitude and longitude and add to the coordinates list
            double latitude = double.Parse(values[0]);
            double longitude = double.Parse(values[1]);

            coordinates.Add((latitude, longitude));
        }

        return coordinates;
    }

    static double ShoelaceFormula(List<(double, double)> coordinates)
    {
        double sum = 0;

        for (int i = 0; i < coordinates.Count - 1; i++)
        {
            sum += coordinates[i].Item1 * coordinates[i + 1].Item2 - coordinates[i + 1].Item1 * coordinates[i].Item2;
        }

        // Add the last term
        sum += coordinates[coordinates.Count - 1].Item1 * coordinates[0].Item2 - coordinates[0].Item1 * coordinates[coordinates.Count - 1].Item2;

        // Take the absolute value and divide by 2
        double area = Math.Abs(sum) / 2;

        return area;
    }
    
}