using System;
using System.Threading.Tasks;
using CraftBeer.Api;
using CraftBeer.Api.Domain;
using CraftBeer.Api.Models;
using Xunit;
using Moq;

namespace CraftBeer.Test
{
    public class BeerServiceUnitTests
    {
        [Fact]
        public async Task GetAllBeers_NoBeerRegistered_ReturnEmptyCollectionAsync()
        {
            var repository = new Mock<IRepository<Beer>>(MockBehavior.Strict);
            repository.Setup(m => m.GetAllAsync())
                .ReturnsAsync(new Beer[0]);
            
            var result = await new BeerService(repository.Object).GetAllBeersAsync();

            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllBeers_SingleBeerRegistered_ReturnSingleElementInCollectionAsync()
        {
            var repository = new Mock<IRepository<Beer>>(MockBehavior.Strict);
            repository.Setup(m => m.GetAllAsync())
                .ReturnsAsync(new Beer[]
                {
                    new Beer(1)
                });
            
            var result = await new BeerService(repository.Object).GetAllBeersAsync();

            Assert.Collection(result, (beer) => Assert.Equal(1, beer.Id));
        }

        [Fact]
        public async Task GetAllBeers_MultipleBeersRegistered_ReturnCollectionOrderedByIdAscendingAsync()
        {
            var repository = new Mock<IRepository<Beer>>(MockBehavior.Strict);
            repository.Setup(m => m.GetAllAsync())
                .ReturnsAsync(new Beer[]
                {
                    new Beer(2),
                    new Beer(1)
                });
            
            var result = await new BeerService(repository.Object).GetAllBeersAsync();

            Assert.Collection(result,
                (beer) => Assert.Equal(1, beer.Id),
                (beer) => Assert.Equal(2, beer.Id));
        }

        [Fact]
        public async Task GetAllBeers_ErrorRetrievingData_ThrowsServiceExceptionAsync()
        {
            var repository = new Mock<IRepository<Beer>>(MockBehavior.Strict);
            repository.Setup(m => m.GetAllAsync())
                .ThrowsAsync(new Exception());

            await Assert.ThrowsAsync(typeof(ServiceException),
                async () => await new BeerService(repository.Object).GetAllBeersAsync());
        }

        [Fact]
        public async Task GetBeerById_BeerExists_BeerReturnedAsync()
        {
            var repository = new Mock<IRepository<Beer>>(MockBehavior.Strict);
            repository.Setup(m => m.GetByIdAsync(1))
                .ReturnsAsync(new Beer(1));
            
            var result = await new BeerService(repository.Object).GetBeerByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task GetBeerById_BeerDoesntExist_NullReturnedAsync()
        {
            var repository = new Mock<IRepository<Beer>>(MockBehavior.Strict);
            repository.Setup(m => m.GetByIdAsync(1))
                .ReturnsAsync(new Beer(1));
            repository.Setup(m => m.GetByIdAsync(2))
                .ReturnsAsync((Beer) null);
            
            var result = await new BeerService(repository.Object).GetBeerByIdAsync(2);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetBeerById_ErrorRetrievingData_ThrowsServiceExceptionAsync()
        {
            var repository = new Mock<IRepository<Beer>>(MockBehavior.Strict);
            repository.Setup(m => m.GetByIdAsync(It.IsAny<int>()))
                .ThrowsAsync(new Exception());
            
            await Assert.ThrowsAsync(typeof(ServiceException),
                async () => await new BeerService(repository.Object).GetBeerByIdAsync(1));
        }

        [Fact]
        public async Task AddNewBeer_BeerCreatedSucessfully_ReturnsBeerAsync()
        {
            var repository = new Mock<IRepository<Beer>>(MockBehavior.Strict);
            repository.Setup(m => m.CreateAsync(It.IsAny<Beer>()))
                .Returns(Task.CompletedTask);
            
            var beer = new Beer(3);
            var result = await new BeerService(repository.Object).AddNewBeerAsync(beer);

            Assert.NotNull(result);
            Assert.Equal(beer.Id, result.Id);
        }

        [Fact]
        public async Task AddNewBeer_ErrorInsertingData_ThrowsServiceExceptionAsync()
        {
            var repository = new Mock<IRepository<Beer>>(MockBehavior.Strict);
            repository.Setup(m => m.CreateAsync(It.IsAny<Beer>()))
                .ThrowsAsync(new Exception());

            await Assert.ThrowsAsync(typeof(ServiceException),
                async () => await new BeerService(repository.Object).AddNewBeerAsync(new Beer(3)));
        }

        [Fact]
        public async Task UpdateBeer_BeerUpdatedSucessfully_ReturnsTrueAsync()
        {
            var beer = new Beer(12);
            var repository = new Mock<IRepository<Beer>>(MockBehavior.Strict);
            repository.Setup(m => m.GetByIdAsync(beer.Id))
                .ReturnsAsync(beer);
            repository.Setup(m => m.UpdateAsync(beer.Id, It.IsAny<Beer>()))
                .Returns(Task.CompletedTask);

            var result = await new BeerService(repository.Object).UpdateBeerAsync(beer.Id, new Beer(52));

            Assert.True(result);
        }

        [Fact]
        public async Task UpdateBeer_BeerNotFound_ReturnsFalseAsync()
        {
            var repository = new Mock<IRepository<Beer>>(MockBehavior.Strict);
            repository.Setup(m => m.GetByIdAsync(22))
                .ReturnsAsync((Beer) null);

            var result = await new BeerService(repository.Object).UpdateBeerAsync(22, new Beer(52));

            Assert.False(result);
        }

        [Fact]
        public async Task UpdateBeer_ErrorRetrievingData_ThrowsServiceExceptionAsync()
        {
            var beer = new Beer(12);
            var repository = new Mock<IRepository<Beer>>(MockBehavior.Strict);
            repository.Setup(m => m.GetByIdAsync(beer.Id))
                .Throws(new Exception());

            await Assert.ThrowsAsync(typeof(ServiceException),
                async () => await new BeerService(repository.Object).UpdateBeerAsync(beer.Id, new Beer(52)));
        }

        [Fact]
        public async Task UpdateBeer_ErrorUpdatingData_ThrowsServiceExceptionAsync()
        {
            var beer = new Beer(12);
            var repository = new Mock<IRepository<Beer>>(MockBehavior.Strict);
            repository.Setup(m => m.GetByIdAsync(beer.Id))
                .ReturnsAsync(beer);
            repository.Setup(m => m.UpdateAsync(beer.Id, It.IsAny<Beer>()))
                .Throws(new Exception());

            await Assert.ThrowsAsync(typeof(ServiceException),
                async () => await new BeerService(repository.Object).UpdateBeerAsync(beer.Id, new Beer(52)));
        }

        [Fact]
        public async Task DeleteBeer_BeerDeletedSucessfully_ReturnsTrueAsync()
        {
            var beer = new Beer(12);
            var repository = new Mock<IRepository<Beer>>(MockBehavior.Strict);
            repository.Setup(m => m.GetByIdAsync(beer.Id))
                .ReturnsAsync(beer);
            repository.Setup(m => m.DeleteAsync(beer.Id))
                .Returns(Task.CompletedTask);

            var result = await new BeerService(repository.Object).DeleteBeerAsync(beer.Id);

            Assert.True(result);
        }

        [Fact]
        public async Task DeleteBeer_BeerNotFound_ReturnsFalseAsync()
        {
            var repository = new Mock<IRepository<Beer>>(MockBehavior.Strict);
            repository.Setup(m => m.GetByIdAsync(13))
                .ReturnsAsync((Beer) null);

            var result = await new BeerService(repository.Object).UpdateBeerAsync(13, new Beer(52));

            Assert.False(result);
        }

        [Fact]
        public async Task DeleteBeer_ErrorRetrievingData_ThrowsServiceExceptionAsync()
        {
            var beer = new Beer(12);
            var repository = new Mock<IRepository<Beer>>(MockBehavior.Strict);
            repository.Setup(m => m.GetByIdAsync(beer.Id))
                .Throws(new Exception());

            await Assert.ThrowsAsync(typeof(ServiceException),
                async () => await new BeerService(repository.Object).DeleteBeerAsync(beer.Id));
        }

        [Fact]
        public async Task DeleteBeer_ErrorUpdatingData_ThrowsServiceExceptionAsync()
        {
            var beer = new Beer(12);
            var repository = new Mock<IRepository<Beer>>(MockBehavior.Strict);
            repository.Setup(m => m.GetByIdAsync(beer.Id))
                .ReturnsAsync(beer);
            repository.Setup(m => m.DeleteAsync(beer.Id))
                .Throws(new Exception());

            await Assert.ThrowsAsync(typeof(ServiceException),
                async () => await new BeerService(repository.Object).DeleteBeerAsync(beer.Id));
        }
    }
}