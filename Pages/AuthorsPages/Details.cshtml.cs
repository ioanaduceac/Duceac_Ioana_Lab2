
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Duceac_Ioana_Lab2.Models;

namespace Duceac_Ioana_Lab2.Pages.AuthorsPages
{
    public class DetailsModel : PageModel
    {
        private readonly Duceac_Ioana_Lab2Context _context;

        public DetailsModel(Duceac_Ioana_Lab2Context context)
        {
            _context = context;
        }

        public Authors Authors { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authors = await _context.Authors
                .FirstOrDefaultAsync(a => a.ID == id);

            if (authors == null)
            {
                return NotFound();
            }

            Authors = authors;
            return Page();
        }
    }
}
