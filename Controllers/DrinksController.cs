using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TacoFastFoodAPI.Models;

namespace TacoFastFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrinksController : ControllerBase
    {
        private readonly FastFoodTacoDbContext _context;

        public DrinksController(FastFoodTacoDbContext context)
        {
            _context = context;
        }

        [HttpGet("/Drinks")]
        public async Task<ActionResult<Drink>> GetDrinks(string? sortByCost)
        {
            IQueryable<Drink> query = _context.Drinks;

            if (sortByCost != null)
            {
                query = sortByCost == "ascending"
                    ? query.OrderBy(d => d.Cost)
                    : query.OrderByDescending(d => d.Cost);
            }
            var drinkList = await query.ToListAsync();

            return Ok(drinkList);
        }

        [HttpGet("/Drinks/{id}")]
        public async Task<ActionResult<Drink>> GetDrinkById(int id)
        {
            if (DrinkExists(id))
            {
                Drink? queriedDrink = await _context.Drinks.FindAsync(id);
                return Ok(queriedDrink);
            }
            return NotFound("Taco not found");
        }

        [HttpPost("/Drinks")]
        public async Task<ActionResult<Drink>> CreateDrink([FromBody] Drink aDrink)
        {
            _context.Add(aDrink);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDrinkById), new { id = aDrink.Id }, aDrink);
        }

        [HttpPut("/Drinks/{id}")]
        public async Task<ActionResult<Drink>> UpdateDrink([FromBody] Drink aDrink, int id)
        {
            if (aDrink == null)
            {
                return BadRequest("Updated item cannot be null.");
            }

            var existingItem = await _context.Drinks.FindAsync(id);

            if (existingItem == null)
            {
                return NotFound($"Drink with ID {id} not found.");
            }

            existingItem.Id = id;
            existingItem.Name = aDrink.Name;
            existingItem.Cost = aDrink.Cost;
            existingItem.Slushie = aDrink.Slushie;

            await _context.SaveChangesAsync();

            return Ok(existingItem);
        }

        private bool DrinkExists(int id)
        {
            return (_context.Drinks?.Any(d => d.Id == id)).GetValueOrDefault();
        }
    }
}
