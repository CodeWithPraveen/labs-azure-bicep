using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BrezyWeather.Data;
using BrezyWeather.Models;

namespace BrezyWeather.Pages.Weathers
{
    public class EditModel : PageModel
    {
        private readonly BrezyWeather.Data.WeatherContext _context;

        public EditModel(BrezyWeather.Data.WeatherContext context)
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

            var weather =  await _context.Weather.FirstOrDefaultAsync(m => m.ID == id);
            if (weather == null)
            {
                return NotFound();
            }
            Weather = weather;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, int locationID)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Weather.LocationID = locationID;
            _context.Attach(Weather).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeatherExists(Weather.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index", new { locationID = locationID });
        }

        private bool WeatherExists(int id)
        {
          return (_context.Weather?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
