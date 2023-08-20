using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BrezyWeather.Data;
using BrezyWeather.Models;

namespace BrezyWeather.Pages.Locations
{
    public class IndexModel : PageModel
    {
        private readonly BrezyWeather.Data.WeatherContext _context;

        public IndexModel(BrezyWeather.Data.WeatherContext context)
        {
            context.Database.EnsureCreated();
            _context = context;
        }

        public IList<Location> Location { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Location != null)
            {
                Location = await _context.Location.ToListAsync();
            }
        }
    }
}
