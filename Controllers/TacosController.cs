using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TacoFastFoodAPI.Models;

namespace TacoFastFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TacosController : ControllerBase
    {
        private readonly FastFoodTacoDbContext _context;

        public TacosController(FastFoodTacoDbContext context)
        {
            _context = context;
        }

        [HttpGet("/Tacos")]
        public async Task<ActionResult<Taco>> GetTacos(bool? isSoftShell)
        {
            List<Taco> tacos = new List<Taco>();

            try
            {
                if (isSoftShell == true)
                {
                    tacos = await _context.Tacos.Where(t => t.SoftShell == isSoftShell).ToListAsync();

                    return Ok(tacos);
                }

                tacos = await _context.Tacos.ToListAsync();
                return Ok(tacos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Could not retrieve Tacos");
            }
        }

        [HttpGet("/Tacos/{id}")]
        public async Task<ActionResult<Taco>> GetTacoById(int id)
        {
            if (TacoExists(id))
            {
                Taco? queriedTaco = await _context.Tacos.FindAsync(id);
                return Ok(queriedTaco);
            }
            return NotFound("Taco not found");
        }

        [HttpPost("/Tacos")]
        public async Task<ActionResult<Taco>> CreateTaco([FromBody] Taco aTaco)
        {
            _context.Add(aTaco);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTacoById), new { id = aTaco.Id }, aTaco);
        }

        [HttpDelete("/Tacos/{id}")]
        public async Task<ActionResult<int>> DeleteTaco(int id)
        {
            var aTaco = await _context.Tacos.FindAsync(id);

            if (aTaco == null)
            {
                return NotFound("Taco not found");
            }

            _context.Tacos.Remove(aTaco);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TacoExists(int id)
        {
            return (_context.Tacos?.Any(t => t.Id == id)).GetValueOrDefault();
        }

    }
}
