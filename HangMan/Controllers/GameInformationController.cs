using HangMan.Data;
using HangMan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using System.Linq;

namespace HangMan.Controllers
{
    public class GameInformationController : Controller
    {
        private readonly AppDbContext context;

        public GameInformationController(AppDbContext c)
        {
            this.context = c;
        }
        public IActionResult Index() //player stats page
        {
            Player p = new Player("xx_Username_xx", "password123");
            p.GamesPlayed = 100;
            p.GamesWon = 25;
            p.GamesLost = 75;
            p.LongestWord = "FiberOptics";
            p.MistakesWithWin = 75;
            p.MistakesTotal = 420;

            return View(p);

            //return View(context.CurrentPlayer)
        }
        [HttpPost]
        public IActionResult Index(Player p)
        {
            return View(p);
        }
        public IActionResult Dictionary()
        {
            FilteredWords fw = new FilteredWords();
            fw.Words = this.context.Words.Include(w => w.Category).ToList();
            return View(fw);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FilterDictionary(FilteredWords fw)
        {

            if (!string.IsNullOrWhiteSpace(fw.Search))  
                fw.Words = this.context.Words
                    .Include(w => w.Category)
                    .Where(w => w.Spelling.ToUpper().Contains(fw.Search.ToUpper()))
                    .ToList();

            else
                fw.Words = this.context.Words
                    .Include(w => w.Category)
                    .ToList();

            return View("Dictionary", fw);
        }
        public IActionResult PlayerSettings()
        {
            PlayerPreference p = new();
            p.Categories = context.Categories.ToList();
            return View(p);
        }
        [HttpPost]
        public IActionResult PlayerSettings(PlayerPreference p)
        {
            return View(p);
        }
        public IActionResult AddWord()
        {
            return View();
        }
    }
}
