using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<Beer>> GetAllBeersAsync()
        {
            try
            {
                return (await _beerRepository.GetAllAsync()).OrderBy((b) => b.Id);
            }
            catch (Exception ex)
            {
                throw new ServiceException("Error retrieving beer list", ex);
            }
        }

        public async Task<Beer> GetBeerByIdAsync(int id)
        {
            try
            {
                return await _beerRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new ServiceException($"Error retrieving beer with Id = {id}", ex);
            }
        }

        public async Task<Beer> AddNewBeerAsync(Beer beer)
        {
            try
            {
                await _beerRepository.CreateAsync(beer);
                return beer;
            }
            catch (Exception ex)
            {
                throw new ServiceException($"Error inserting new beer with Id = {beer?.Id}", ex);
            }
        }

        public async Task<bool> UpdateBeerAsync(int id, Beer beer)
        {
            if (await GetBeerByIdAsync(id) == null)
            {
                return false;
            }
            try
            {
                await _beerRepository.UpdateAsync(id, beer);
            }
            catch (Exception ex)
            {
                throw new ServiceException($"Error updating beer Id = {id}", ex);
            }
            return true;
        }

        public async Task<bool> DeleteBeerAsync(int id)
        {
            if (await GetBeerByIdAsync(id) == null)
            {
                return false;
            }
            try
            {
                await _beerRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new ServiceException($"Error removing beer Id = {id}", ex);
            }
            return true;
        }
    }
}