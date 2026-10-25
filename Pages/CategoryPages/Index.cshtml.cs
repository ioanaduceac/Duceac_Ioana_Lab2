using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Duceac_Ioana_Lab2.Models;

namespace Duceac_Ioana_Lab2.Pages.CategoryPages;

public class IndexModel : PageModel
{
    private readonly Duceac_Ioana_Lab2Context _context;

    public IndexModel(Duceac_Ioana_Lab2Context context)
    {
        _context = context;
    }

    public IList<Category> Category { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Category = await _context.Category.ToListAsync();
    }
}
