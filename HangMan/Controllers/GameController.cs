using HangMan.Data;
using HangMan.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;
using Mysqlx.Expr;
using System.Threading.Tasks;

namespace HangMan.Controllers
{
    public class GameController : Controller
    {
        private AppDbContext context;
        private UserManager<PlayerModel> userManager;

        public GameController(AppDbContext c, UserManager<PlayerModel> u)
        {
            this.context = c;
            this.userManager = u;
        }


        public IActionResult Index(
            bool useCustomSettings = false,
            string customWord = "",
            int maxWordLength = 0,
            int minWordLength = 0,
            int health = 0,
            string category = "",
            bool drugIsGeneric = false)
        {
            if (useCustomSettings)
            {
                var prefs = new PlayerPreferenceModel
                {
                    CustomWord = customWord,
                    MaxWordLength = maxWordLength,
                    MinWordLength = minWordLength,
                    Health = health,
                    Category = category,
                    DrugIsGeneric = drugIsGeneric
                };

                return View(BuildGameFromPreferences(prefs));
            }

            return View(BuildDefaultGame());
        }

        [HttpPost]
        public IActionResult Index(PlayerPreferenceModel p)
        {
            return View(BuildGameFromPreferences(p));
        }

        [HttpPost]
        public async Task<IActionResult> GameOverAsync(GameModel gm)
        {
            var user = await userManager.GetUserAsync(User);
            if (user != null)
            {
                user.GamesPlayed += 1;
                user.MistakesTotal += gm.IncorrectGuesses.Count;

                if (gm.IsWin)
                {
                    user.GamesWon += 1;
                    user.MistakesWithWin += gm.IncorrectGuesses.Count;
                }
                else
                    user.GamesLost += 1;

                if (user.LongestWord.Length < gm.Word.Length)
                    user.LongestWord = gm.Word;
            }

            if (gm.IsCustom)
                return View("PlayerSettings", "GameInfomation");
            else
                return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> NewGame(string word, int mistakes, bool won, bool isCustom)
        {
            if (!isCustom)
            {
                await UpdatePlayerStats(word, mistakes, won);
            }

            return RedirectToAction(nameof(Index));
        }

        private GameModel BuildDefaultGame()
        {
            GameModel gm = new();
            var wordInfo = GetRandomWord();
            if (wordInfo != null)
            {
                gm.Word = wordInfo.Spelling;
                gm.Category = wordInfo.Category.Name;
                gm.DrugClassification = wordInfo.DrugClassification;
                gm.DrugIsGeneric = wordInfo.DrugIsGeneric;
            }

            gm.Health = gm.Word.Length > 5 ? gm.Word.Length - 2 : 3;
            return gm;
        }

        private GameModel BuildGameFromPreferences(PlayerPreferenceModel p)
        {
            GameModel gm = new();
            gm.IsCustom = true;

            if (!string.IsNullOrWhiteSpace(p.CustomWord))
            {
                gm.Word = p.CustomWord;
                gm.Category = p.Category;
                gm.DrugIsGeneric = p.DrugIsGeneric;
            }
            else
            {
                var wordInfo = GetRandomWord(p.MaxWordLength, p.MinWordLength, p.Category, p.DrugIsGeneric);
                if (wordInfo != null)
                {
                    gm.Word = wordInfo.Spelling;
                    gm.Category = wordInfo.Category.Name;
                    gm.DrugClassification = wordInfo.DrugClassification;
                    gm.DrugIsGeneric = wordInfo.DrugIsGeneric;
                }
            }

            if (p.Health > 0)
                gm.Health = p.Health;
            else
                gm.Health = gm.Word.Length > 5 ? gm.Word.Length - 2 : 3;

            return gm;
        }

        private WordModel? GetRandomWord(int min = 0, int max = 0, string cat = "", bool drugIsGeneric = false)
        { 
            var baseQuery = context.Words.Include(w => w.Category).AsQueryable();

            if (min > 0)
                baseQuery = baseQuery.Where(w => w.Length > min);

            if (max > 0)
                baseQuery = baseQuery.Where(w => w.Length < max);

            IQueryable<WordModel> filteredQuery = baseQuery;

            if (!string.IsNullOrEmpty(cat))
            {
                filteredQuery = filteredQuery.Where(w => w.Category.Name == cat);

                if (cat.ToLower() == "drug" || cat.ToLower() == "drugs")
                {
                    filteredQuery = filteredQuery.Where(w => w.DrugIsGeneric == drugIsGeneric);
                }
            }

            int count = filteredQuery.Count();
            if (count > 0)
                return filteredQuery.Skip(Random.Shared.Next(count)).FirstOrDefault();

            int baseCount = baseQuery.Count();
            return baseCount > 0 ? baseQuery.Skip(Random.Shared.Next(baseCount)).FirstOrDefault() : null;
        }

        private async Task UpdatePlayerStats(string word, int mistakes, bool won)
        {
            var user = await userManager.GetUserAsync(User);
            if (user != null)
            {

                if (won)
                {
                    user.GamesWon++;
                    user.MistakesWithWin += mistakes;
                    if (!string.IsNullOrEmpty(word) && word.Length > user.LongestWord.Length)
                        user.LongestWord = word;
                }
                else
                {
                    user.GamesLost++;
                }

                user.GamesPlayed++;
                user.MistakesTotal += mistakes;

                await context.SaveChangesAsync();
            }
        }
    }
}
