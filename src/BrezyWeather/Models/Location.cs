using System.ComponentModel.DataAnnotations;

namespace BrezyWeather.Models
{
    public class Location
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public int Zipcode { get; set; }
    }
}
