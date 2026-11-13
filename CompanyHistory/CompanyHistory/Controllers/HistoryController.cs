using Microsoft.AspNetCore.Mvc;

namespace CompanyHistory.Controllers
{
    public class HistoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Year(int year)
        {
            ViewBag.year = year;
            return View();
        }
        public IActionResult Milestones()
        {
            object mot = "2021, uong bia";
            object hai = "2022, an tao";
            object ba = "2023, uong tra su";

            ViewData["Miles"] = new object[] { mot, hai, ba };
            return View();
        }
    }
}
