using HangMan.Data;
using HangMan.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using System.Linq;
using System.Security.Claims;

namespace HangMan.Controllers
{
    public class GameInformationController : Controller
    {
        private AppDbContext context;
        
        private UserManager<PlayerModel> userManager;

        public GameInformationController(AppDbContext c, UserManager<PlayerModel> u)
        {
            this.context = c;
            this.userManager = u;
        }
        public async Task<IActionResult> Index() //player stats page
        {
            var user = await userManager.GetUserAsync(User);
            return View(user);
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
            var userId = userManager.GetUserId(User);
            var pref = context.PlayerPreferences
                    .FirstOrDefault(p => p.PlayerId == userId);
            if (pref == null)
            {
                pref = new PlayerPreferenceModel
                {
                    PlayerId = userId
                };
                context.PlayerPreferences.Add(pref);
                context.SaveChanges();
            }
                

            PlayerPreferenceViewModel p = new(pref, context.Categories.ToList());
            return View(p);
        }

        [HttpPost]
        [Authorize]
        public IActionResult PlayerSettings(PlayerPreferenceViewModel p, string action)
        {
            if(string.IsNullOrWhiteSpace(p.Preferences.PlayerId))
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
            if (awm.SelectedCategoryName == "NewCategory1234567898765432345678765432134567876543")
            {
                if (!string.IsNullOrWhiteSpace(awm.Word.Category?.Name)
                    && !context.Categories.Any(c => c.Name == awm.Word.Category.Name))
                {
                    context.Categories.Add(awm.Word.Category);
                }
            }
            else
            {
                awm.Word.Category = context.Categories
                    .FirstOrDefault(c => c.Name == awm.SelectedCategoryName);
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
