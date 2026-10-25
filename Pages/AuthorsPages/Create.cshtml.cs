
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Duceac_Ioana_Lab2.Models;

namespace Duceac_Ioana_Lab2.Pages.AuthorsPages
{
    public class CreateModel : PageModel
    {
        private readonly Duceac_Ioana_Lab2Context _context;

        public CreateModel(Duceac_Ioana_Lab2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Authors Authors { get; set; } = new Authors();

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Authors.Add(Authors);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
