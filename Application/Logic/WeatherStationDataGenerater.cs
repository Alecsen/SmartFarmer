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
                station.WindSpeed = GenerateWindSpeed(random);
                station.Precipitation = GenerateNextPrecipitation(station, lastPrecipitation, random);
                station.Evaporation = station.Precipitation > 0 ? 0 : -0.1;
            }
            return stations;
        }

        private string GenerateNextWindDirection(string currentDirection, Random random)
        {
            if (random.NextDouble() < 0.6)
            {
                return currentDirection;
            }
            // Definer alle mulige vindretninger
            string[] directions = { "N", "NØ", "Ø", "SØ", "S", "SV", "V", "NV" };

            // Vægtning for hver retning baseret på sandsynlighed for at forekomme
            var weights = new Dictionary<string, int>
            {
                {"N", 5}, {"NØ", 5}, {"Ø", 6}, {"SØ", 5}, 
                {"S", 5}, {"SV", 17}, {"V", 40}, {"NV", 17}
            };

            // Liste til at holde alle retninger baseret på deres vægtning
            var weightedDirections = new List<string>();

            foreach (var direction in directions)
            {
                for (int i = 0; i < weights[direction]; i++)
                {
                    weightedDirections.Add(direction);
                }
            }

            // Vælg en ny tilfældig retning med hensyntagen til vægtning
            string newDirection = weightedDirections[random.Next(weightedDirections.Count)];

            // Logik for gradvis ændring af vindretningen
            int currentIndex = Array.IndexOf(directions, currentDirection);
            int newIndex = Array.IndexOf(directions, newDirection);

            // Kontrollér, om den nye retning er inden for ét skridt fra den nuværende retning
            if (Math.Abs(currentIndex - newIndex) <= 1 || Math.Abs(currentIndex - newIndex) == directions.Length - 1)
            {
                return newDirection; // Tillad ændringen hvis den er gradvis
            }
            else
            {
                // Hvis ændringen ikke er gradvis, vælg den nærmeste retning til nuværende
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
        
        private double GenerateWindSpeed(Random random)
        {
            double mean = 4.5; // Middelværdi for vindhastighed
            double stdDev = 2.0; // Standardafvigelse

            // Brug Box-Muller metoden til at generere en normalfordelt værdi
            double u1 = 1.0 - random.NextDouble(); // Uniform(0,1] tilfældige doubles
            double u2 = 1.0 - random.NextDouble();
            double normalRandom = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);

            // Skaler og skift den normalfordelte værdi
            double windSpeed = mean + stdDev * normalRandom;

            // Sørg for, at vindhastigheden ikke er negativ
            return Math.Max(windSpeed, 0);
        }
       
    }