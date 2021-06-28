using CraftBeer.Api.Models;
using Microsoft.EntityFrameworkCore;

public class BeerDbContext : DbContext
{
    public BeerDbContext(DbContextOptions<BeerDbContext> options)
        : base(options)
    { }
    
    public DbSet<Beer> Beers { get; set; }
}