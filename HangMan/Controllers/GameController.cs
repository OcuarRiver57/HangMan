using Microsoft.AspNetCore.Mvc;

namespace HangMan.Controllers
{
    public class GameController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
