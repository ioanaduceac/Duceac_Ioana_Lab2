
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Duceac_Ioana_Lab2.Models;

namespace Duceac_Ioana_Lab2.Pages.BookPages;

public class IndexModel : PageModel
{
    private readonly Duceac_Ioana_Lab2Context _context;

    public IndexModel(Duceac_Ioana_Lab2Context  context)
    {
        _context = context;
    }

    public IList<Book> Book { get; set; } = new List<Book>();

    public SelectList AuthorsOptions { get; set; } = default!;

    [BindProperty(SupportsGet = true)]
    public int? SelectedAuthorID { get; set; }

    public async Task OnGetAsync()
    {
        AuthorsOptions = new SelectList(
            await _context.Authors
                .AsNoTracking()
                .OrderBy(a => a.LastName)
                .ThenBy(a => a.FirstName)
                .ToListAsync(),
            "ID",
            "LastName",
            SelectedAuthorID
        );

        var query = _context.Book
            .Include(b => b.Authors)
            .Include(b => b.Publisher)
            .AsQueryable();

        if (SelectedAuthorID.HasValue)
        {
            query = query.Where(b =>
                b.AuthorsID == SelectedAuthorID.Value);
        }

        Book = await query.ToListAsync();
    }
}
