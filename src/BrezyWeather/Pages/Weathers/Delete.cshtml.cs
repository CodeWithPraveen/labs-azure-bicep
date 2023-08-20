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
    public class DeleteModel : PageModel
    {
        private readonly BrezyWeather.Data.WeatherContext _context;

        public DeleteModel(BrezyWeather.Data.WeatherContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id, int locationID)
        {
            if (id == null || _context.Weather == null)
            {
                return NotFound();
            }
            var weather = await _context.Weather.FindAsync(id);

            if (weather != null)
            {
                Weather = weather;
                _context.Weather.Remove(Weather);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index", new { locationID = locationID });
        }
    }
}
