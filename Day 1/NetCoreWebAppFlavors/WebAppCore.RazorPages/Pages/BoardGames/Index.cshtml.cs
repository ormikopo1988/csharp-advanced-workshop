using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppCore.RazorPages.DataContext;
using WebAppCore.RazorPages.Models;

namespace WebAppCore.RazorPages.Pages.BoardGames
{
    public class IndexModel : PageModel
    {
        private BoardGamesDBContext _context;

        public IndexModel(BoardGamesDBContext context)
        {
            _context = context;
        }

        public List<BoardGame> BoardGames { get; set; }

        public void OnGet()
        {
            BoardGames = _context.BoardGames.ToList();
        }
    }
}