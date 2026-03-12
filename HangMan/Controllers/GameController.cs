using HangMan.Data;
using HangMan.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Expr;

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


        public IActionResult Index()
        {
            GameModel gm = new();
            gm.Word = GetRandomWord();
            gm.Health = gm.Word.Length > 5 ? gm.Word.Length - 2 : 3;
            return View(gm);
        }
        [HttpPost]
        public IActionResult Index(PlayerPreferenceModel p)
        {
            GameModel gm = new();
            gm.IsCustom = true;

            if (!string.IsNullOrWhiteSpace(p.CustomWord))
                gm.Word = p.CustomWord;
            else
                gm.Word = GetRandomWord(p.MaxWordLength, p.MinWordLength, p.Category);

            if (p.Health > 0)
                gm.Health = p.Health;
            else
                gm.Health = gm.Word.Length > 5 ? gm.Word.Length - 2 : 3;

            return View(gm);
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

                if(user.LongestWord.Length < gm.Word.Length)
                    user.LongestWord = gm.Word;
            }

            if (gm.IsCustom)
                return View("PlayerSettings", "GameInfomation");
            else
                return View("Index");
        }

        private string GetRandomWord(int min = 0, int max = 0, string cat = "")
        { // used ai to make my code run faster by avoiding processing heavy operations like converting the entire db with .ToList()
            // Base query
            var baseQuery = context.Words.Include(w => w.Category).AsQueryable();

            if (min > 0)
                baseQuery = baseQuery.Where(w => w.Length > min);

            if (max > 0)
                baseQuery = baseQuery.Where(w => w.Length < max);

            // Try category filter first
            IQueryable<WordModel> filteredQuery = baseQuery;

            if (!string.IsNullOrEmpty(cat))
                filteredQuery = filteredQuery.Where(w => w.Category.Name == cat);

            // Try to get a random word WITH the category
            var word = filteredQuery
                .OrderBy(x => Guid.NewGuid())
                .Select(x => x.Spelling)
                .FirstOrDefault();

            if (word != null)
                return word;

            // Category failed → fallback to ALL categories
            return baseQuery
                .OrderBy(x => Guid.NewGuid())
                .Select(x => x.Spelling)
                .FirstOrDefault();
        }

    }
}
