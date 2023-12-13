using System.Threading.Channels;
using Application.DAOInterface;
using Application.EventHandlers;
using Application.Events;
using Domain.DTOs;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Application.Logic;
 public class WeatherStationDataGenerator : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private WeatherStation _lastGeneratedData = new();
        private readonly IMediator _mediator;

        public WeatherStationDataGenerator(IServiceScopeFactory scopeFactory, IMediator mediator)
        {
            _scopeFactory = scopeFactory;
            _mediator = mediator;
        }
        
        // Alec
        private void OnWeatherChanged()
        {
            Console.WriteLine("Weather changed");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _lastGeneratedData = new WeatherStation(); 
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var weatherStationDao = scope.ServiceProvider.GetRequiredService<IWeatherStationDao>();
                    await GenerateAndUpdateData(weatherStationDao);
                }
                await Task.Delay(TimeSpan.FromSeconds(3600), stoppingToken); // Vent en time
            }
        }

        private async Task GenerateAndUpdateData(IWeatherStationDao weatherStationDao)
        {
            var stations = await weatherStationDao.GetWeatherStations();
            var updatedStations = UpdateWeatherStationsGeneratedData(stations);

            await weatherStationDao.UpdateWeatherStations(updatedStations);
            _lastGeneratedData = updatedStations.FirstOrDefault(); 
            
            Console.WriteLine("Weather changed in Generate");
            await _mediator.Publish(new WeatherUpdateEvent());
           
        }

        private IEnumerable<WeatherStation> UpdateWeatherStationsGeneratedData(IEnumerable<WeatherStation> stations)
        {
            var random = new Random();
            foreach (var station in stations.ToList()) 
            {
                // Gem den nuværende tilstand af stationen
                var lastPrecipitation = station.Precipitation;

                // Opdater stationens data
                station.WindDirection = GenerateNextWindDirection(station.WindDirection, random);
                station.WindSpeed = GenerateWindSpeed(random);
                station.Precipitation = GenerateNextPrecipitation(station, lastPrecipitation, random);
                station.Evaporation = station.Precipitation > 0 ? 0 : -0.15;
            }
            return stations;
        }

        private string GenerateNextWindDirection(string currentDirection, Random random)
        {
            if (random.NextDouble() < 0.4)
            {
                return currentDirection;
            }
            
            string[] directions = { "N", "NØ", "Ø", "SØ", "S", "SV", "V", "NV" };
            
            var weights = new Dictionary<string, int>
            {
                {"N", 5}, {"NØ", 5}, {"Ø", 6}, {"SØ", 5}, 
                {"S", 5}, {"SV", 17}, {"V", 40}, {"NV", 17}
            };

            
            var weightedDirections = new List<string>();

            foreach (var direction in directions)
            {
                for (int i = 0; i < weights[direction]; i++)
                {
                    weightedDirections.Add(direction);
                }
            }

           
            string newDirection = weightedDirections[random.Next(weightedDirections.Count)];

           
            int currentIndex = Array.IndexOf(directions, currentDirection);
            int newIndex = Array.IndexOf(directions, newDirection);

            
            if (Math.Abs(currentIndex - newIndex) <= 1 || Math.Abs(currentIndex - newIndex) == directions.Length - 1)
            {
                return newDirection;
            }
            else
            {
              
                if (currentIndex == 0 && newIndex == directions.Length - 1)
                    return directions[currentIndex - 1];
                else if (currentIndex == directions.Length - 1 && newIndex == 0)
                    return directions[0];
                else
                    return currentIndex > newIndex ? directions[currentIndex - 1] : directions[currentIndex + 1];
            }
        }
        private double GenerateNextPrecipitation(WeatherStation station, double lastPrecipitation, Random random)
        {
            double heavyRainThreshold = 5.0; //tærskel for "meget regn"
            double baseRainProbability = 0.08; //basischance for regn
            double heavyRainProbability = 0.001; //basischance for meget regn

           
            if (lastPrecipitation > 0)
            {
                baseRainProbability += 0.4; 
            }

          
            if (lastPrecipitation > heavyRainThreshold)
            {
                heavyRainProbability += 0.005; 
            }

            // Bestem, om det vil regne
            bool willRain = random.NextDouble() < baseRainProbability;
            bool willBeHeavyRain = willRain && random.NextDouble() < heavyRainProbability;

            if (willRain)
            {
                
                double precipitation = Math.Pow(random.NextDouble(), 2) * 3;

               
                if (willBeHeavyRain)
                {
                    precipitation = heavyRainThreshold + Math.Pow(random.NextDouble(), 3) * (20 - heavyRainThreshold);
                }

                return precipitation;
            }

           
            return 0;
        }
        private double GenerateWindSpeed(Random random)
        {
            double mean = 4.5; // Middelværdi for vindhastighed
            double stdDev = 2.0; // Standardafvigelse

           
            double u1 = 1.0 - random.NextDouble(); 
            double u2 = 1.0 - random.NextDouble();
            double normalRandom = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);

            
            double windSpeed = mean + stdDev * normalRandom;

          
            return Math.Max(windSpeed, 0);
        }
       
    }