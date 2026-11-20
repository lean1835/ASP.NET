using Day6_BaiTap_LeAn.Data;
using Day6_BaiTap_LeAn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Day6_BaiTap_LeAn.Controllers
{
    public class HomeController(MovieDbContext db) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var movies = await db.Movies.Include(movie => movie.Genre)
                .OrderBy(movie => movie.Id)
                .ToListAsync();
            return View(movies);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
