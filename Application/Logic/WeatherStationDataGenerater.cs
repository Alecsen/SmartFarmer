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
        private WeatherStation _lastGeneratedData;

        public WeatherStationDataGenerator(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Initialiser _lastGeneratedData her eller i første kald af GenerateAndUpdateData
            _lastGeneratedData = new WeatherStation(); // Start med standardværdier

            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var weatherStationDao = scope.ServiceProvider.GetRequiredService<IWeatherStationDao>();
                    await GenerateAndUpdateData(weatherStationDao);
                }

                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken); // Juster intervallet efter behov
            }
        }

        private async Task GenerateAndUpdateData(IWeatherStationDao weatherStationDao)
        {
            var stations = await weatherStationDao.GetWeatherStations();
            var updatedStations = UpdateWeatherStationsGeneratedData(stations);

            await weatherStationDao.UpdateWeatherStations(updatedStations);
            _lastGeneratedData = updatedStations.FirstOrDefault(); // Opdater med den seneste genererede data
        }

        private IEnumerable<WeatherStation> UpdateWeatherStationsGeneratedData(IEnumerable<WeatherStation> stations)
        {
            var random = new Random();
            foreach (var station in stations.ToList()) // Brug ToList for at undgå problemer med iteration over en samling, der ændres
            {
                // Gem den nuværende tilstand af stationen
                var lastPrecipitation = station.Precipitation;

                // Opdater stationens data
                station.WindDirection = GenerateNextWindDirection(station.WindDirection, random);
                station.WindSpeed = random.Next(0, 100);
                station.Precipitation = GenerateNextPrecipitation(station, lastPrecipitation, random);
                station.Evaporation = station.Precipitation > 0 ? 0 : random.Next(-10, 0);
            }
            return stations;
        }

        private string GenerateNextWindDirection(string currentDirection, Random random)
        {
            // Implementer logik for realistisk ændring af vindretning
            // ...
            return currentDirection; // Returner opdateret vindretning
        }

        private double GenerateNextPrecipitation(WeatherStation station, double lastPrecipitation, Random random)
        {
            double heavyRainThreshold = 5.0; //tærskel for "meget regn"
            double baseRainProbability = 0.12; //basischance for regn
            double heavyRainProbability = 0.001; //basischance for meget regn

            // Mindre stigning i chancen for regn, hvis det regnede i den foregående time
            if (lastPrecipitation > 0)
            {
                baseRainProbability += 0.4; // Mindre øgning end tidligere
            }

            // Mindre stigning i chancen for meget regn, hvis der var meget regn i den foregående time
            if (lastPrecipitation > heavyRainThreshold)
            {
                heavyRainProbability += 0.005; // Mindre øgning end tidligere
            }

            // Bestem, om det vil regne
            bool willRain = random.NextDouble() < baseRainProbability;
            bool willBeHeavyRain = willRain && random.NextDouble() < heavyRainProbability;

            if (willRain)
            {
                // Antag en normal nedbørsmængde
                double precipitation = Math.Pow(random.NextDouble(), 2) * 4;

                // Justering for "meget regn"
                if (willBeHeavyRain)
                {
                    precipitation = heavyRainThreshold + Math.Pow(random.NextDouble(), 3) * (20 - heavyRainThreshold);
                }

                return precipitation;
            }

            // Ingen regn
            return 0;
        }

        private string GenerateWindDirection(Random random)
        {
            string[] directions = { "Nord", "Nordøst", "Øst", "Sydøst", "Syd", "Sydvest", "Vest", "Nordvest" };
            return directions[random.Next(directions.Length)];
        }
    }