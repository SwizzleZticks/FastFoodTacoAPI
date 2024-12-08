﻿
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
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

        [HttpGet("/Combos")]
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

        [HttpGet("/Combos/{id}")]
        public async Task<ActionResult<Combo>> GetComboById(int id)
        {
            Combo? queriedCombo = await _context.Combos.Include(c => c.Taco)
                                                        .Include(c => c.Drink)
                                                        .FirstOrDefaultAsync(c => c.Id == id);

            return queriedCombo != null ? Ok(queriedCombo) : NotFound(); 
        }

        private bool ComboExists(int id)
        {
            return (_context.Combos?.Any(c => c.Id == id)).GetValueOrDefault();
        }
    }
}
