using System;
using Xunit;
using Moq;

namespace CraftBeer.Test
{
    public class BeerServiceUnitTests
    {
        [Fact]
        public async Task GetAllBeers_NoBeerRegistered_ReturnEmptyCollectionAsync()
        {            
        }

        [Fact]
        public async Task GetAllBeers_SingleBeerRegistered_ReturnSingleElementInCollectionAsync()
        {
        }

        [Fact]
        public async Task GetAllBeers_MultipleBeersRegistered_ReturnCollectionOrderedByIdAscendingAsync()
        {
        }

        [Fact]
        public async Task GetAllBeers_ErrorRetrievingData_ThrowsServiceExceptionAsync()
        {
        }
    }
}