
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Duceac_Ioana_Lab2.Models;

namespace Duceac_Ioana_Lab2.Pages.AuthorsPages
{
    public class DeleteModel : PageModel
    {
        private readonly Duceac_Ioana_Lab2Context _context;

        public DeleteModel(Duceac_Ioana_Lab2Context context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var authors = await _context.Authors.FindAsync(id);

            if (authors != null)
            {
                bool hasBooks = await _context.Book
                    .AnyAsync(b => b.AuthorsID == id);

                if (hasBooks)
                {
                    ModelState.AddModelError(string.Empty,
                        "Autorul nu poate fi șters deoarece este asociat unor cărți.");

                    Authors = authors;
                    return Page();
                }

                _context.Authors.Remove(authors);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
