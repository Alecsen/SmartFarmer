using System.Threading.Channels;
using Application.DAOInterface;
using Domain.DTOs;
using Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Application.Logic;

    public class WeatherStationDataGenerator : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public WeatherStationDataGenerator(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var weatherStationDao = scope.ServiceProvider.GetRequiredService<IWeatherStationDao>();
                    await GenerateAndUpdateData(weatherStationDao);
                }

                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken); // Juster intervallet efter behov
            }
        }

        private async Task GenerateAndUpdateData(IWeatherStationDao weatherStationDao)
        {
            var stations = await weatherStationDao.GetWeatherStations();
            var updatedStations = UpdateWeatherStations(stations);

            await weatherStationDao.UpdateWeatherStations(updatedStations);
        }

        private IEnumerable<WeatherStation> UpdateWeatherStations(IEnumerable<WeatherStation> stations)
        {
            var random = new Random();
            foreach (var station in stations)
            {
                station.WindDirection = GenerateWindDirection(random);
                station.WindSpeed = random.Next(0, 100);
                station.Precipitation = random.NextDouble() * 20;
                station.Evaporation = random.Next(-10, 10);
            }
            return stations;
        }

        private string GenerateWindDirection(Random random)
        {
            string[] directions = { "Nord", "Nordøst", "Øst", "Sydøst", "Syd", "Sydvest", "Vest", "Nordvest" };
            return directions[random.Next(directions.Length)];
        }
    }
    