using Microsoft.EntityFrameworkCore;
using WebAppCore.MVC.Models;

namespace WebAppCore.MVC.DataContext
{
    public class BoardGamesDBContext : DbContext
    {
        public BoardGamesDBContext(DbContextOptions<BoardGamesDBContext> options)
            : base(options) { }

        public DbSet<BoardGame> BoardGames { get; set; }
    }
}