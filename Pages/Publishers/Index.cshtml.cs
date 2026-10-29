using Duceac_Ioana_Lab2.Models.ViewModels;
using Duceac_Ioana_Lab2.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Duceac_Ioana_Lab2.Pages.Publishers
{
    public class IndexModel : PageModel
    {
        private readonly Duceac_Ioana_Lab2Context _context;

        public IndexModel(Duceac_Ioana_Lab2Context context)
        {
            _context = context;
        }

        public IList<Publisher> Publishers { get; set; } = new List<Publisher>();
        public  PublisherIndexData  PublisherData { get; set; } 
        public int PublisherID { get; set; }
        public int BookID { get; set; }
        public async Task OnGetAsync(int? id, int? bookID)
        {
            Publishers = await _context.Publisher
                .AsNoTracking()
                .ToListAsync();

            PublisherData = new PublisherIndexData();
            PublisherData.Publishers = await _context.Publisher
                .Include(i => i.Books)
                    .ThenInclude(c => c.Authors)
                .OrderBy(i => i.PublisherName)
                .ToListAsync();

            if(id != null)
            {
                PublisherID = id.Value;
                Publisher publisher = PublisherData.Publishers
                    .Where(i => i.ID == id.Value).Single();
                PublisherData.Books = publisher.Books;
            }
        }
    }
}
