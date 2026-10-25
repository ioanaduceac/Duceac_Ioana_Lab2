
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Duceac_Ioana_Lab2.Models;

namespace Duceac_Ioana_Lab2.Pages.AuthorsPages
{
    public class IndexModel : PageModel
    {
        private readonly Duceac_Ioana_Lab2Context _context;

        public IndexModel(Duceac_Ioana_Lab2Context context)
        {
            _context = context;
        }

        public IList<Authors> Authors { get; set; } = new List<Authors>();

        public async Task OnGetAsync()
        {
            Authors = await _context.Authors
                .OrderBy(a => a.LastName)
                .ThenBy(a => a.FirstName)
                .ToListAsync();
        }
    }
}
