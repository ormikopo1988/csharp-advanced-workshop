using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebAppCore.MVC.DataContext;
using WebAppCore.MVC.Models;

namespace WebAppCore.MVC.Controllers
{
    public class BoardGameController : Controller
    {
        private readonly BoardGamesDBContext context;

        public BoardGameController(BoardGamesDBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var games = context.BoardGames.ToList();

            return View(games);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(BoardGame game)
        {
            //Determine the next ID
            var newID = context.BoardGames.Select(x => x.ID).Max() + 1;

            game.ID = newID;

            context.BoardGames.Add(game);
            context.SaveChanges();
            
            return RedirectToAction("Index");
        }
    }
}