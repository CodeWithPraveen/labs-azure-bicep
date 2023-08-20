using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BrezyWeather.Data;
using BrezyWeather.Models;

namespace BrezyWeather.Pages.Weathers
{
    public class CreateModel : PageModel
    {
        private readonly BrezyWeather.Data.WeatherContext _context;

        public CreateModel(BrezyWeather.Data.WeatherContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int locationID)
        {
            LocationID = locationID;

            return Page();
        }

        [BindProperty]
        public Weather Weather { get; set; } = default!;
        public int LocationID { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(int locationID)
        {
            LocationID = locationID;

            if (!ModelState.IsValid || _context.Weather == null || Weather == null)
            {
                return Page();
            }

            //var location = _context.Location.FirstOrDefault(x => x.ID == locationID);
            Weather.LocationID = locationID;

            _context.Weather.Add(Weather);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new { locationID = locationID } );
        }
    }
}
