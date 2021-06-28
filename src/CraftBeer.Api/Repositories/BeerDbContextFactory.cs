using Microsoft.EntityFrameworkCore;

namespace CraftBeer.Api.Repositories
{
    public class BeerDbContextFactory
    {
        private DbContextOptions<BeerDbContext> GenerateDbContextOptions()
        {
            return new DbContextOptionsBuilder<BeerDbContext>()
                .UseInMemoryDatabase(databaseName: "Beer")
                .Options;
        }
        
        public BeerDbContext GetBeerDbContext()
        {
            return new BeerDbContext(GenerateDbContextOptions());
        }
    }
}