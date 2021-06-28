using System;
using System.Collections.Generic;
using CraftBeer.Api.Models;
using CraftBeer.Api.Domain;
using System.Threading.Tasks;

namespace CraftBeer.Api.Repositories
{
    public class BeerRepository : IRepository<Beer>
    {
        public Task CreateAsync(Beer entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Beer>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Beer> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, Beer entity)
        {
            throw new NotImplementedException();
        }
    }
}