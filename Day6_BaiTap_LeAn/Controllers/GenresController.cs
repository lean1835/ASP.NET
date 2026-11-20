using Day6_BaiTap_LeAn.Data;
using Day6_BaiTap_LeAn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Day6_BaiTap_LeAn.Controllers;

public class GenresController(MovieDbContext db) : Controller
{
    public async Task<IActionResult> Index()
    {
        return View(await db.Genres.OrderBy(genre => genre.Name).ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var genre = await db.Genres.Include(item => item.Movies)
            .FirstOrDefaultAsync(item => item.Id == id);
        return genre is null ? NotFound() : View(genre);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name")] Genre genre)
    {
        if (ModelState.IsValid)
        {
            db.Genres.Add(genre);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(genre);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var genre = await db.Genres.FindAsync(id);
        return genre is null ? NotFound() : View(genre);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Genre genre)
    {
        if (id != genre.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            db.Update(genre);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(genre);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var genre = await db.Genres.FirstOrDefaultAsync(item => item.Id == id);
        return genre is null ? NotFound() : View(genre);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var genre = await db.Genres.FindAsync(id);
        if (genre is null)
        {
            return NotFound();
        }

        if (await db.Movies.AnyAsync(movie => movie.GenreId == id))
        {
            TempData["Error"] = "Không thể xóa thể loại đang được sử dụng bởi phim.";
            return RedirectToAction(nameof(Index));
        }

        db.Genres.Remove(genre);
        await db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
