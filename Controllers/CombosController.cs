using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TacoFastFoodAPI.Models;

namespace TacoFastFoodAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CombosController : ControllerBase
    {
        FastFoodTacoDbContext _context;

        public CombosController(FastFoodTacoDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Combo>> GetCombos()
        {
            try
            {
                var combo = await _context.Combos.Include(c => c.Taco).Include(c => c.Drink).ToListAsync();
                return Ok(combo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Could not retrieve combos");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Combo>> GetComboById(int id)
        {
            Combo? queriedCombo = await _context.Combos.Include(c => c.Taco)
                                                       .Include(c => c.Drink)
                                                       .FirstOrDefaultAsync(c => c.Id == id);

            return queriedCombo != null ? Ok(queriedCombo) : NotFound(); 
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Combo>> CreateCombo([FromBody]Combo aCombo)
        {
            _context.Add(aCombo);
            await _context.SaveChangesAsync();

            return aCombo;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteCombo(int id)
        {
            var aCombo = await _context.Combos.FindAsync(id);

            if (aCombo == null)
            {
                return NotFound("Combo not found");
            }

            _context.Combos.Remove(aCombo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
