using Application.DAOInterface;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
namespace EfcDataAccess.DAOs;
public class WeatherEfcStationDao : IWeatherStationDao
{
    
    private readonly SmartFarmerAppContext context;

    public WeatherEfcStationDao(SmartFarmerAppContext context)
    {
        this.context = context;
    }


    public async Task<WeatherStation> CreateWeatherStationByFieldIdAsync(int fieldId)
    {
        WeatherStation toCreate = new WeatherStation
        {
            FieldId = fieldId,
            WindDirection = " ",
            Evaporation = 0,
            WindSpeed = 0,
            Precipitation = 0,
        };
        EntityEntry<WeatherStation> newWeatherStation = await context.WeatherStations.AddAsync(toCreate);
        await context.SaveChangesAsync();
        return newWeatherStation.Entity;
    }

    public Task<WeatherStationLookupDto> GetById(int id)
    {
        throw new NotImplementedException();
    }

    //This method is currently a bit "unfishned, it only have the abbilty to return 1 weather station per field, which makes sense for our program now, but might be liable for a change in the future
    public async Task<WeatherStation?> GetByFieldId(int fieldId)
    {
        IEnumerable<WeatherStation> weatherStation = await context.WeatherStations.Where(station => station.FieldId == fieldId).ToListAsync();

        return weatherStation.FirstOrDefault();
        
    }

    public async Task<IEnumerable<WeatherStation>> GetWeatherStations()
    {
        List<WeatherStation> stations = await context.WeatherStations.ToListAsync();
        return stations;
    }

    public async Task UpdateWeatherStations(IEnumerable<WeatherStation> updatedStations)
    {
        foreach (var station in updatedStations)
        {
            context.Entry(station).State = EntityState.Modified;
        }

        await context.SaveChangesAsync();
    }
    
}

