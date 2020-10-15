using Microsoft.EntityFrameworkCore;
using WebAppCore.RazorPages.Models;

namespace WebAppCore.RazorPages.DataContext
{
    public class BoardGamesDBContext : DbContext
    {
        public BoardGamesDBContext(DbContextOptions<BoardGamesDBContext> options)
            : base(options) { }

        public DbSet<BoardGame> BoardGames { get; set; }
    }
}