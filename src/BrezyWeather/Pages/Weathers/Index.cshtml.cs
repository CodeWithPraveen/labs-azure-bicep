using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BrezyWeather.Data;
using BrezyWeather.Models;

namespace BrezyWeather.Pages.Weathers
{
    public class IndexModel : PageModel
    {
        private readonly BrezyWeather.Data.WeatherContext _context;

        public IndexModel(BrezyWeather.Data.WeatherContext context)
        {
            _context = context;
        }

        public IList<Weather> Weather { get;set; } = default!;
        public int LocationID { get; set; }

        public async Task<IActionResult> OnGetAsync(int locationID)
        {
            LocationID = locationID;

            if (_context.Weather != null)
            {
                var weather = await _context.Weather.Where(m => m.LocationID == locationID).ToListAsync();
                if (weather == null)
                {
                    return NotFound();
                }
                Weather = weather;
            }
            return Page();
        }

    }
}
