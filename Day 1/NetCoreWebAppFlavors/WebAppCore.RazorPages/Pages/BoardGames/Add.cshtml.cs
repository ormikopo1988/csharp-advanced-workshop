using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppCore.RazorPages.DataContext;
using WebAppCore.RazorPages.Models;

namespace WebAppCore.RazorPages.Pages.BoardGames
{
    public class AddModel : PageModel
    {
        private readonly BoardGamesDBContext _context;

        public AddModel(BoardGamesDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public BoardGame BoardGame { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.BoardGames.Add(BoardGame);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}