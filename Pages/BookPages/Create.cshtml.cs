
using Duceac_Ioana_Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Duceac_Ioana_Lab2.Pages.BookPages;

public class CreateModel : PageModel
{
    private readonly Duceac_Ioana_Lab2Context _context;

    public CreateModel(Duceac_Ioana_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Book Book { get; set; } = new();

    public SelectList AuthorsOptions { get; set; } = default!;

    private async Task LoadAuthorsAsync()
    {
        AuthorsOptions = new SelectList(
            await _context.Authors
                .AsNoTracking()
                .OrderBy(a => a.LastName)
                .ThenBy(a => a.FirstName)
                .ToListAsync(),
            "ID",
            "LastName"
        );
    }

    public async Task<IActionResult> OnGetAsync()
    {
        await LoadAuthorsAsync();
        await LoadSelectListsAsync();
        return Page();
    }

    private async Task LoadSelectListsAsync()
    {
        ViewData["PublisherID"] = new SelectList(
            await _context.Publisher
                .AsNoTracking()
                .OrderBy(p => p.PublisherName)
                .ToListAsync(),
            "ID",
            "PublisherName"
        );

        AuthorsOptions = new SelectList(
            await _context.Authors
                .AsNoTracking()
                .OrderBy(a => a.LastName)
                .ThenBy(a => a.FirstName)
                .ToListAsync(),
            "ID",
            "LastName"
        );
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadAuthorsAsync();
            await LoadSelectListsAsync();
            return Page();
        }

        _context.Book.Add(Book);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
