using Duceac_Ioana_Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Duceac_Ioana_Lab2.Pages.BookPages;

public class EditModel : BookCategoriesPageModel
{
    private readonly Duceac_Ioana_Lab2Context _context;

    public EditModel(Duceac_Ioana_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Book Book { get; set; } = default!;

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
            "LastName",
            Book.AuthorsID
        );
    }

    private async Task LoadSelectListsAsync()
    {
        ViewData["PublisherID"] = new SelectList(
            await _context.Publisher
                .AsNoTracking()
                .OrderBy(p => p.PublisherName)
                .ToListAsync(),
            "ID",
            "PublisherName",
            Book.PublisherID
        );

        AuthorsOptions = new SelectList(
            await _context.Authors
                .AsNoTracking()
                .OrderBy(a => a.LastName)
                .ThenBy(a => a.FirstName)
                .ToListAsync(),
            "ID",
            "LastName",
            Book.AuthorsID
        );
    }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        Book = await _context.Book
             .Include(b => b.Publisher)
             .Include(b => b.BookCategories).ThenInclude(b => b.Category)
             .AsNoTracking()
             .FirstOrDefaultAsync(m => m.ID == id);

        if (Book is null)
        {
            return NotFound();
        }

        PopulateAssignedCategoryData(_context, Book);

        await LoadSelectListsAsync();
        await LoadAuthorsAsync();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCategories)
    {
        if (id == null) return NotFound();

        var bookToUpdate = await _context.Book
         .Include(i => i.Publisher)
         .Include(i => i.BookCategories)
         .ThenInclude(i => i.Category)
         .FirstOrDefaultAsync(s => s.ID == id);

        if (!ModelState.IsValid)
        {
            await LoadSelectListsAsync();
            await LoadAuthorsAsync();
            return Page();
        }

        bookToUpdate = await _context.Book
            .FirstOrDefaultAsync(b => b.ID == Book.ID);

        if (bookToUpdate is null)
        {
            return NotFound();
        }

        bookToUpdate.title = Book.title;
        bookToUpdate.Price = Book.Price;
        bookToUpdate.PublishingDate = Book.PublishingDate;
        bookToUpdate.AuthorsID = Book.AuthorsID;
        bookToUpdate.PublisherID = Book.PublisherID;

        if (await TryUpdateModelAsync<Book>(bookToUpdate, "Book", i => i.title, i => i.AuthorsID, i => i.Price, i => i.PublishingDate, i => i.PublisherID))
        {
            UpdateBookCategories(_context, selectedCategories, bookToUpdate);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
        //Apelam UpdateBookCategories pentru a aplica informatiile din checkboxuri la entitatea Books care
        //este editata
        UpdateBookCategories(_context, selectedCategories, bookToUpdate);
        PopulateAssignedCategoryData(_context, bookToUpdate);
        return Page();
    }
}