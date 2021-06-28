using CraftBeer.Api.Models;
using System.Collections.Generic;

namespace CraftBeer.Api.Domain
{
    public interface IBeerRepository
    {
        IEnumerable<Beer> List();
        Beer Get(int id);
        void Add(Beer beer);
        void Update(int id, Beer beer);
        void Remove(int id);
    }
}