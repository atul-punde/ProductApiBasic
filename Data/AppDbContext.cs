using Microsoft.EntityFrameworkCore;
using ProductApi.Models;

namespace ProductApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Product> Products => Set<Product>();
}