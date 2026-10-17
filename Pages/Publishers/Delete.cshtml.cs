
using Duceac_Ioana_Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Duceac_Ioana_Lab2.Pages.Publishers
{
    public class DeleteModel : PageModel
    {
        private readonly Duceac_Ioana_Lab2Context _context;

        public DeleteModel(Duceac_Ioana_Lab2Context context)
        {
            _context = context;
        }

        public Publisher Publisher { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            var publisher = await _context.Publisher
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ID == id);

            if (publisher == null)
                return NotFound();

            Publisher = publisher;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var publisher = await _context.Publisher
                .FirstOrDefaultAsync(p => p.ID == id);

            if (publisher == null)
                return RedirectToPage("./Index");

            _context.Publisher.Remove(publisher);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
