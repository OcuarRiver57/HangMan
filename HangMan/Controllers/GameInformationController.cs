using Microsoft.AspNetCore.Mvc;

namespace HangMan.Controllers
{
    public class GameInformationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        
    }
}
