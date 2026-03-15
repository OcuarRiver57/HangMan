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
            var query = this.context.Words.Include(w => w.Category).AsQueryable();

            if (!string.IsNullOrWhiteSpace(fw.Search))
            {
                query = query.Where(w => w.Spelling.ToUpper().Contains(fw.Search.ToUpper()));
            }

            if (!string.IsNullOrWhiteSpace(fw.CategorySearch))
            {
                query = query.Where(w => w.Category != null && w.Category.Name.ToUpper().Contains(fw.CategorySearch.ToUpper()));
            }

            fw.Words = query.ToList();

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
                
            // Ensure strings are not null for the view
            pref.CustomWord = pref.CustomWord ?? string.Empty;
            pref.Category = pref.Category ?? string.Empty;

            PlayerPreferenceViewModel p = new(pref, context.Categories.ToList());
            return View(p);
        }

        [HttpPost]
        [Authorize]
        public IActionResult PlayerSettings(PlayerPreferenceViewModel p, string action)
        {
            var userId = userManager.GetUserId(User);
            if (!string.IsNullOrWhiteSpace(userId))
                p.Preferences.PlayerId = userId;

            // Ensure string values are not null to prevent database insertion errors
            p.Preferences.CustomWord = p.Preferences.CustomWord ?? string.Empty;
            p.Preferences.Category = p.Preferences.Category ?? string.Empty;

            if (context.PlayerPreferences.Any(m => m.PlayerId == p.Preferences.PlayerId))
            {
                var dm = context.PlayerPreferences.Find(p.Preferences.PlayerId);
                if (dm != null)
                    context.PlayerPreferences.Remove(dm);
            }

            context.PlayerPreferences.Add(p.Preferences);
            context.SaveChanges();

            if (action == "start")
            {
                return RedirectToAction("Index", "Game", new
                {
                    useCustomSettings = true,
                    customWord = p.Preferences.CustomWord,
                    maxWordLength = p.Preferences.MaxWordLength,
                    minWordLength = p.Preferences.MinWordLength,
                    health = p.Preferences.Health,
                    category = p.Preferences.Category,
                    drugIsGeneric = p.Preferences.DrugIsGeneric
                });
            }

            p.Categories = context.Categories.ToList();
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

            var isDrugCategory = awm.Word.Category?.Name?.ToLower() == "drug" || awm.Word.Category?.Name?.ToLower() == "drugs";
            if (!isDrugCategory)
            {
                awm.Word.DrugClassification = "Not a drug";
                awm.Word.DrugIsGeneric = false;
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
