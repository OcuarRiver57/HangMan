using HangMan.Data;
using HangMan.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using System.Linq;
using System.Security.Claims;

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
            PlayerModel p = new()
            {
                UserName = "xx_Username_xx",
                GamesPlayed = 100,
                GamesWon = 25,
                GamesLost = 75,
                LongestWord = "FiberOptics",
                MistakesWithWin = 75,
                MistakesTotal = 420
            };

            return View(p);

            //return View(context.CurrentPlayer)
        }
        [HttpPost]
        public IActionResult Index(PlayerModel p)
        {
            return View(p);
        }

        public IActionResult Dictionary()
        {
            FilteredWordsModel fw = new FilteredWordsModel();
            fw.Words = this.context.Words.Include(w => w.Category).ToList();
            return View(fw);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FilterDictionary(FilteredWordsModel fw)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteWord(int id)
        {
            var entity = context.Words.Find(id);
            if (entity != null)
            {
                context.Words.Remove(entity);
                context.SaveChanges();
            }
                
            return RedirectToAction("Dictionary");
        }
        [Authorize]
        public IActionResult PlayerSettings()
        {
            PlayerPreferenceViewModel p = new(new PlayerPreferenceModel(), context.Categories.ToList());
            return View(p);
        }

        [HttpPost]
        public IActionResult PlayerSettings(PlayerPreferenceViewModel p, string action)
        {
            p.Preferences.PlayerId = User.Identity.Name;
            if (context.PlayerPreferences.Any(m => m.PlayerId == p.Preferences.PlayerId))
            {
                var dm = context.PlayerPreferences.Find(p.Preferences.PlayerId);
                context.PlayerPreferences.Remove(dm);
            }
            context.PlayerPreferences.Add(p.Preferences);
            if(action == "start")
                return RedirectToAction("Index", "Game", p.Preferences);

            else
                return View(p);
        }

        public IActionResult AddWord()
        {
            AddWordModel awm = new()
            {
                Categories = context.Categories.ToList()
            };
            return View(awm);
        }

        [HttpPost] 
        public IActionResult AddWord(AddWordModel awm)
        {
            if (awm.SelectedCategoryName == "NewCategory1234567898765432345678765432134567876543"
                && context.Categories.Any(c => EF.Functions.Like(c.Name, awm.Word.Category.Name))) 
                context.Categories.Add(awm.Word.Category);
            else
            {
                awm.Word.Category = context.Categories
                    .Where(c => c.Name == awm.Word.Category.Name)
                    .First();
            }

            context.Words.Add(awm.Word);
            context.SaveChanges();

            FilteredWordsModel fw = new FilteredWordsModel();
            fw.Search = awm.Word.Spelling;
            fw.Words = this.context.Words
                .Include(w => w.Category)
                .Where(w => w.Spelling.ToUpper().Contains(fw.Search.ToUpper()))
                .ToList();

            return View("dictionary", fw);
        }
    }
}
