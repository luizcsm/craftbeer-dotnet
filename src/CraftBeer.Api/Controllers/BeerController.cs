using CraftBeer.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace CraftBeer.Api.Controllers
{
    [ApiController]
    [Route("beers")]
    public class BeerController : ControllerBase
    {
        private readonly ILogger<BeerController> _logger;

        public BeerController(ILogger<BeerController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Beer> List()
        {
            _logger.LogInformation("List");
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public Beer Get([FromRoute] int id)
        {
            _logger.LogInformation("Get {Id}", id);
            throw new NotImplementedException();
        }

        [HttpPost]
        public void Post([FromBody] Beer beer)
        {
            _logger.LogInformation("Post {@Beer}", beer);
            throw new NotImplementedException();
        }

        [HttpPut("{id}")]
        public void Put([FromRoute] int id, [FromBody] Beer beer)
        {
            _logger.LogInformation("Put {@Beer} {Id}", beer, id);
            throw new NotImplementedException();
        }

        [HttpPatch("{id}")]
        public void Patch([FromRoute] int id, [FromBody] Beer beer)
        {
            _logger.LogInformation("Patch {@Beer} {Id}", beer, id);
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public void Delete([FromRoute] int id)
        {
            _logger.LogInformation("Delete {Id}", id);
            throw new NotImplementedException();
        }
    }
}