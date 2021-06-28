using CraftBeer.Api.Domain;
using CraftBeer.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CraftBeer.Api.Controllers
{
    [ApiController]
    [Route("beers")]
    public class BeerController : ControllerBase
    {
        private readonly ILogger<BeerController> _logger;
        private readonly BeerService _beerService;

        public BeerController(ILogger<BeerController> logger, BeerService beerService)
        {
            _logger = logger;
            _beerService = beerService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Beer>))]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _beerService.GetAllBeersAsync());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Beer))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var result = await _beerService.GetBeerByIdAsync(id);
            if (result != null) {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> PostAsync([FromBody] Beer beer)
        {
            var result = await _beerService.AddNewBeerAsync(beer);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Id });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromBody] Beer beer)
        {
            bool found = await _beerService.UpdateBeerAsync(id, beer);
            return found ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            bool found = await _beerService.DeleteBeerAsync(id);
            return found ? Ok() : NotFound();
        }

        // Need to understand differences between PUT and PATCH when request body is not specific and attributes are not optional
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PatchAsync([FromRoute] int id, [FromBody] Beer beer)
        {
            bool found = await _beerService.UpdateBeerAsync(id, beer);
            return found ? Ok() : NotFound();
        }
    }
}