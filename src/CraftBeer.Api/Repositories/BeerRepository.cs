using System;
using System.Linq;
using System.Collections.Generic;
using CraftBeer.Api.Models;
using CraftBeer.Api.Domain;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CraftBeer.Api.Repositories
{
    public class BeerRepository : IRepository<Beer>
    {
        private readonly BeerDbContextFactory _dbContextFactory;

        public BeerRepository(BeerDbContextFactory beerDbContextFactory)
        {
            this._dbContextFactory = beerDbContextFactory;
        }

        public async Task CreateAsync(Beer entity)
        {
            using (var context = _dbContextFactory.GetBeerDbContext())
            {
                context.Beers.Add(entity);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var context = _dbContextFactory.GetBeerDbContext())
            {
                var beer = context.Beers.First((b) => b.Id == id);
                context.Beers.Remove(beer);
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Beer>> GetAllAsync()
        {
            using (var context = _dbContextFactory.GetBeerDbContext())
            {
                return await context.Beers.ToListAsync();
            }
        }

        public async Task<Beer> GetByIdAsync(int id)
        {
            using (var context = _dbContextFactory.GetBeerDbContext())
            {
                return await context.Beers.FirstOrDefaultAsync(b => b.Id == id);
            }
        }

        public async Task UpdateAsync(int id, Beer entity)
        {
            using (var context = _dbContextFactory.GetBeerDbContext())
            {
                var beer = context.Beers.First((b) => b.Id == id);
                context.Beers.Remove(beer);
                context.Beers.Add(entity);
                await context.SaveChangesAsync();
            }
        }
    }
}