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

        [HttpGet]
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

        [HttpGet("{id}")]
        public async Task<ActionResult<Drink>> GetDrinkById(int id)
        {
            Drink? queriedDrink = await _context.Drinks.FindAsync(id);

            return queriedDrink != null ? Ok(queriedDrink) : NotFound();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Drink>> CreateDrink([FromBody] Drink aDrink)
        {
            _context.Add(aDrink);
            await _context.SaveChangesAsync();

            return aDrink;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Drink>> UpdateDrink([FromBody] Drink aDrink, int id)
        {
            if (aDrink == null)
            {
                return BadRequest("Updated item cannot be null.");
            }

            var existingDrink = await _context.Drinks.FindAsync(id);

            if (existingDrink == null)
            {
                return NotFound($"Drink with ID {id} not found.");
            }

            await _context.SaveChangesAsync();

            return Ok(id);
        }
    }
}
