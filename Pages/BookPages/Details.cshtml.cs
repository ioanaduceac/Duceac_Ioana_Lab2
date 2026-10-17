
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Duceac_Ioana_Lab2.Models;

namespace Duceac_Ioana_Lab2.Pages.BookPages;

public class DetailsModel : PageModel
{
    private readonly Duceac_Ioana_Lab2Context _context;

    public DetailsModel(Duceac_Ioana_Lab2Context context)
    {
        _context = context;
    }

    public Book Book { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var book = await _context.Book
            .Include(b => b.Authors)
            .FirstOrDefaultAsync(b => b.ID == id);

        if (book is null)
        {
            return NotFound();
        }

        Book = book;

        return Page();
    }
}
