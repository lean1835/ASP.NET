using Day6_BaiTap_LeAn.Data;
using Day6_BaiTap_LeAn.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MovieDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MovieDb")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MovieDbContext>();
    db.Database.EnsureCreated();

    if (!db.Genres.Any())
    {
        var genres = new[]
        {
            new Genre { Name = "Hành động" },
            new Genre { Name = "Tâm lý" },
            new Genre { Name = "Hài" }
        };

        db.Genres.AddRange(genres);
        db.SaveChanges();

        db.Movies.AddRange(
            new Movie { Title = "Phim 1", ReleaseDate = new DateTime(2025, 1, 1), Duration = 100, GenreId = genres[0].Id },
            new Movie { Title = "Phim 2", ReleaseDate = new DateTime(2026, 1, 1), Duration = 120, GenreId = genres[1].Id });
        db.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
