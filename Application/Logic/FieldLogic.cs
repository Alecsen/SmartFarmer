using System.Globalization;
using Application.DAOInterface;
using Application.LogicInterface;
using Domain.DTOs;
using Domain.Models;
using NetTopologySuite;
using NetTopologySuite.Geometries;

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

    public Task<Field> GetByIdAsync(int fieldId)
    {
        if (fieldId == -1)
        {
            throw new Exception($"The field id {fieldId} is not a valid number");
        }

        return fieldDao.GetFieldById(fieldId);
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
            SoilType = dto.SoilType,
            FieldCapacity = dto.FieldCapacity,
            Area = CalculateAreaFromString(dto.LocationData)
        };
        
        var created = await fieldDao.CreateAsync(field);

        await weatherStationDao.CreateWeatherStationByFieldIdAsync(created.Id);

        return created;
    }

    public async Task<Task> PerformCalculation()
    {
        
        var fieldsToUpdate = await fieldDao.GetAllFields();

        foreach (var field in fieldsToUpdate)
        {
           // Console.WriteLine($"for field {field.Name} id: {field.Id} inital moisture level: {field.MoistureLevel}");
            WeatherStation? station = await weatherStationDao.GetByFieldId(field.Id);

            if (station != null)
            {
                CalculateMoistureLevel(station, field);
             //   Console.WriteLine($"for field {field.Name} id: {field.Id} After calculation moisture level: {field.MoistureLevel}");
                await fieldDao.UpdateAsyncField(field);
            }
            
        }
        return Task.CompletedTask;
        
    }

 

    private void CalculateMoistureLevel(WeatherStation weatherStationForField, Field field)
    {
        double precipitation = weatherStationForField.Precipitation;
        double evaporation = weatherStationForField.Evaporation;


        field.MoistureLevel += precipitation + evaporation;
        

    }
    
   
    
    public double CalculateAreaFromString(string coordinateString)
    {
        var koordinater = ParseCoordinatesString(coordinateString);
        if (koordinater.Count < 3)
        {
            throw new ArgumentException("Der skal være mindst 3 koordinater for at danne et polygon.");
        }

        // Tilføj det første punkt til slutningen af listen for at lukke polygonen
        koordinater.Add(koordinater[0]);

        var polygon = new Polygon(new LinearRing(koordinater.ToArray()));
        return polygon.Area * 1000000; // Arealet returneres i kvadratmeter
    }

    private List<Coordinate> ParseCoordinatesString(string coordinateString)
    {
        var koordinater = new List<Coordinate>();
        var parSplit = coordinateString.Split(new[] { "), (" }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var par in parSplit)
        {
            var rensetPar = par.Trim('(', ')');
            var punkter = rensetPar.Split(',');

            if (punkter.Length == 2)
            {
                if (double.TryParse(punkter[0].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out double lon) &&
                    double.TryParse(punkter[1].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out double lat))
                {
                    koordinater.Add(new Coordinate(lon, lat));
                }
                else
                {
                    throw new ArgumentException("Ugyldigt koordinatformat.");
                }
            }
        }

        return koordinater;
    }
    
}