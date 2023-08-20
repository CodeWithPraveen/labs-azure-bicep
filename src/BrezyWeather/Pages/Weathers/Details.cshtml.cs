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
    public class DetailsModel : PageModel
    {
        private readonly BrezyWeather.Data.WeatherContext _context;

        public DetailsModel(BrezyWeather.Data.WeatherContext context)
        {
            _context = context;
        }

        public Weather Weather { get; set; } = default!;
        public int LocationID { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, int locationID)
        {
            LocationID = locationID;

            if (id == null || _context.Weather == null)
            {
                return NotFound();
            }

            var weather = await _context.Weather.FirstOrDefaultAsync(m => m.ID == id);
            if (weather == null)
            {
                return NotFound();
            }
            else
            {
                Weather = weather;
            }
            return Page();
        }
    }
}
