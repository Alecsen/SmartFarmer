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
    private readonly IIrrigationMachineDao irrigationMachineDao;
    private readonly Dictionary<int, double> FieldCapcityForSoilType;
    public FieldLogic(IFieldDao fieldDao, IUserDao userDao, IWeatherStationDao weatherStationDao, IIrrigationMachineDao irrigationMachineDao)
    {
        this.fieldDao = fieldDao;
        this.userDao = userDao;
        this.weatherStationDao = weatherStationDao;
        this.irrigationMachineDao = irrigationMachineDao;
        
        FieldCapcityForSoilType = new Dictionary<int, double>
        {
            {1, 25},
            {2, 35},
            {3, 45},
            {4, 55},
            {5, 65},
            {6, 75},
            {7, 85},
            {8, 95},
            {9, 105},
            {10, 115},
            {11, 125},
            {12, 135}
        };
        
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

        // Use the SoilType to get the FieldCapacity from the dictionary
        if (!FieldCapcityForSoilType.TryGetValue(dto.SoilType, out double fieldCapacity))
        {
            throw new Exception($"No field capacity found for soil type {dto.SoilType}");
        }
        
        Field field = new Field
        {
            Name = dto.FieldName,
            OwnerId = dto.OwnerId,
            LocationData = dto.LocationData,
            CropType = dto.CropType,
            SoilType = dto.SoilType,
            FieldCapacity = fieldCapacity,
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
            IEnumerable<IrrigationMachine> machine = await irrigationMachineDao.GetIrrigationMachineByFieldId(field.Id);
            
            if (station != null)
            {
                CalculateMoistureLevel(station, field,machine);
             //   Console.WriteLine($"for field {field.Name} id: {field.Id} After calculation moisture level: {field.MoistureLevel}");
                await fieldDao.UpdateAsyncField(field);
            }
            
        }
        return Task.CompletedTask;
        
    }

 

    private void CalculateMoistureLevel(WeatherStation weatherStationForField, Field field,
        IEnumerable<IrrigationMachine> irrigationMachines)
    {
        double precipitation = weatherStationForField.Precipitation;
        double evaporation = weatherStationForField.Evaporation;

        // Calculate the total amount of water spread by the irrigation machines (in cubic meters)
        double totalWaterSpread = irrigationMachines.Where(machine => machine.IsRunning).Sum(machine => machine.WaterAmount);

        // Convert field area from hectares to square meters
        double? fieldAreaInSquareMeters = field.Area * 10000;

        // Calculate how many mm of water have been spread over the field
        double? waterSpreadPerSquareMeter = (totalWaterSpread / fieldAreaInSquareMeters)*1000;

        Console.WriteLine($"Precipitation: {precipitation}, Evaporation: {evaporation}, Water spread: {waterSpreadPerSquareMeter}");
        
        field.MoistureLevel += precipitation + evaporation + waterSpreadPerSquareMeter ?? 0;


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
    
    
    //method for legacy fields, not used anymore but might be useful in the future
    public async Task CalculateAreaForAllFields()
    {
        // Retrieve all fields from the database
        var fields = await fieldDao.GetAllFields();

        foreach (var field in fields)
        {
            // Calculate the area for each field
            double area = CalculateAreaFromString(field.LocationData);

            // Update the field's area in the database
            field.Area = area;
            await fieldDao.UpdateAsyncField(field);
        }
    }
    
}