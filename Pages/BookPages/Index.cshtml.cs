using Duceac_Ioana_Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Duceac_Ioana_Lab2.Pages.BookPages;

public class IndexModel : PageModel
{
    private readonly Duceac_Ioana_Lab2Context _context;

    public IndexModel(Duceac_Ioana_Lab2Context context)
    {
        _context = context;
    }

    public IList<Book> Book { get; set; } = new List<Book>();
    public BookData BookD { get; set; }
    public int BookID { get; set; }
    public int CategoryID { get; set; }
    public string TitleSort { get; set; }
    public string AuthorSort { get; set; }
    public string CurrentFilter { get; set; }


    public SelectList AuthorsOptions { get; set; } = default!;

    [BindProperty(SupportsGet = true)]
    public int? SelectedAuthorID { get; set; }

    public async Task OnGetAsync(int? id, int? categoryID, string sortOrder, string searchString)
    {
        BookD = new BookData();

        TitleSort = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
        AuthorSort = sortOrder == "author" ? "author_desc" : "author";

        CurrentFilter = searchString;

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
        BookD.Books = await _context.Book
              .Include(b => b.Authors)
             .Include(b => b.Publisher)
             .Include(b => b.BookCategories)
             .ThenInclude(b => b.Category)
             .AsNoTracking()
             .OrderBy(b => b.title)
             .ToListAsync();

        if (!String.IsNullOrEmpty(searchString))
        {
            BookD.Books = BookD.Books.Where(s => (s.Authors != null && (s.Authors.FirstName.Contains(searchString)

           || s.Authors.LastName.Contains(searchString)))
           || (s.title != null && s.title.Contains(searchString)));
        }


        if (id != null)
        {
            BookID = id.Value;
            Book book = BookD.Books
            .Where(i => i.ID == id.Value).Single();
            BookD.Categories = book.BookCategories.Select(s => s.Category);
        }

        switch (sortOrder)
        {
            case "title_desc":
                BookD.Books = BookD.Books.OrderByDescending(s =>
               s.title);
                break;
            case "author_desc":
                BookD.Books = BookD.Books.OrderByDescending(s =>
               s.Authors.FullName);
                break;
            case "author":
                BookD.Books = BookD.Books.OrderBy(s =>
               s.Authors.FullName);
                break;
            default:
                BookD.Books = BookD.Books.OrderBy(s => s.title);
                break;
        }

        Book = await query.ToListAsync();
    }
}