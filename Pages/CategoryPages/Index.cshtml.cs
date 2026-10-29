using Duceac_Ioana_Lab2.Models;
using Duceac_Ioana_Lab2.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Duceac_Ioana_Lab2.Pages.CategoryPages;

public class IndexModel : PageModel
{
    private readonly Duceac_Ioana_Lab2Context _context;

    public IndexModel(Duceac_Ioana_Lab2Context context)
    {
        _context = context;
    }

    public IList<Category> Category { get; set; } = default!;
    public CategoryIndexData CategoryData { get; set; }
    public int CategoryID { get; set; }

    public async Task OnGetAsync(int? id)
    {
        CategoryData = new CategoryIndexData();

        CategoryData.Categories = await _context.Category
            .Include(i => i.BookCategories)
                .ThenInclude(i => i.Book)
                    .ThenInclude(i => i.Authors)
            .OrderBy(i => i.CategoryName)
            .ToListAsync();

        if (id != null)
        {
            CategoryID = id.Value;
            Category category = CategoryData.Categories
                .Where(i => i.ID == id.Value).Single();
            CategoryData.BookCategories = category.BookCategories;
        }
    }
}