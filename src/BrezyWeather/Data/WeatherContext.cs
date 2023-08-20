using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BrezyWeather.Models;

namespace BrezyWeather.Data
{
    public class WeatherContext : DbContext
    {
        public WeatherContext (DbContextOptions<WeatherContext> options)
            : base(options)
        {
        }

        public DbSet<BrezyWeather.Models.Location> Location { get; set; } = default!;
        public DbSet<BrezyWeather.Models.Weather>? Weather { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>().HasData(
                new Location { ID = 1, Name = "Chennai", Country = "India", Zipcode = 600003 },
                new Location { ID = 2, Name = "Kuala Lumpur", Country = "Malaysia", Zipcode = 50088 },
                new Location { ID = 3, Name = "Shanghai", Country = "China", Zipcode = 200120 },
                new Location { ID = 4, Name = "Tokyo", Country = "Japan", Zipcode = 049319 }
                );

            modelBuilder.Entity<Weather>().HasData(
                new Weather { ID = 1, Temperature = 28, Humidity = 78, AirQuality = "Fair", LocationID = 1 },
                new Weather { ID = 2, Temperature = 27, Humidity = 84, AirQuality = "Healthy", LocationID = 1 }
                );
        }
    }
}
