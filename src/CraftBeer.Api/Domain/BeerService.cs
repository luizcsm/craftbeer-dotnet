using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CraftBeer.Api.Models;

namespace CraftBeer.Api.Domain
{
    public class BeerService
    {
        private readonly IRepository<Beer> _beerRepository;

        public BeerService(IRepository<Beer> beerRepository)
        {
            _beerRepository = beerRepository;
        }

        public Task<IEnumerable<Beer>> GetAllBeersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Beer> GetBeerByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Beer> AddNewBeerAsync(Beer beer)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateBeerAsync(int id, Beer beer)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteBeerAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}