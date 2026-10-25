using Duceac_Ioana_Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Duceac_Ioana_Lab2.Pages.BookPages;

public class CreateModel : BookCategoriesPageModel
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
        new Book();
        Book.BookCategories = new List<BookCategory>();
        PopulateAssignedCategoryData(_context, Book);
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

    public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
    {
        var newBook = new Book();
        if (selectedCategories != null)
        {
            newBook.BookCategories = new List<BookCategory>();
            foreach (var cat in selectedCategories)
            {
                var catToAdd = new BookCategory
                {
                    CategoryID = int.Parse(cat)
                };
                newBook.BookCategories.Add(catToAdd);
            }
        }
        Book.BookCategories = newBook.BookCategories;
        _context.Book.Add(Book);
        await _context.SaveChangesAsync();
        return RedirectToPage("./Index");
    }
}