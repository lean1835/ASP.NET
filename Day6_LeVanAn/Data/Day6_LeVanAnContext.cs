using Microsoft.EntityFrameworkCore;

public class Day6_LeVanAnContext(DbContextOptions<Day6_LeVanAnContext> options) : DbContext(options)
{
    public DbSet<Day6_LeVanAn.Models.Product> Product { get; set; } = default!;
}
