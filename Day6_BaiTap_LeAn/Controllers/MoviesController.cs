using Day6_BaiTap_LeAn.Data;
using Day6_BaiTap_LeAn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Day6_BaiTap_LeAn.Controllers;

public class MoviesController(MovieDbContext db) : Controller
{
    public async Task<IActionResult> Index()
    {
        var movies = db.Movies.Include(movie => movie.Genre).OrderBy(movie => movie.Id);
        return View(await movies.ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var movie = await db.Movies.Include(item => item.Genre)
            .FirstOrDefaultAsync(item => item.Id == id);
        return movie is null ? NotFound() : View(movie);
    }

    public async Task<IActionResult> Create()
    {
        await PopulateGenresAsync();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Title,ReleaseDate,Duration,GenreId")] Movie movie)
    {
        if (ModelState.IsValid)
        {
            db.Movies.Add(movie);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        await PopulateGenresAsync(movie.GenreId);
        return View(movie);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var movie = await db.Movies.FindAsync(id);
        if (movie is null)
        {
            return NotFound();
        }

        await PopulateGenresAsync(movie.GenreId);
        return View(movie);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Duration,GenreId")] Movie movie)
    {
        if (id != movie.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            db.Update(movie);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        await PopulateGenresAsync(movie.GenreId);
        return View(movie);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var movie = await db.Movies.Include(item => item.Genre)
            .FirstOrDefaultAsync(item => item.Id == id);
        return movie is null ? NotFound() : View(movie);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var movie = await db.Movies.FindAsync(id);
        if (movie is not null)
        {
            db.Movies.Remove(movie);
            await db.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task PopulateGenresAsync(object? selectedGenre = null)
    {
        ViewBag.GenreId = new SelectList(await db.Genres.OrderBy(genre => genre.Name).ToListAsync(), "Id", "Name", selectedGenre);
    }
}
