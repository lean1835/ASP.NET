using Day6_BaiTap_LeAn.Models;
using Microsoft.EntityFrameworkCore;

namespace Day6_BaiTap_LeAn.Data;

public class MovieDbContext(DbContextOptions<MovieDbContext> options) : DbContext(options)
{
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<Movie> Movies => Set<Movie>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Movie>()
            .HasOne(movie => movie.Genre)
            .WithMany(genre => genre.Movies)
            .HasForeignKey(movie => movie.GenreId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
