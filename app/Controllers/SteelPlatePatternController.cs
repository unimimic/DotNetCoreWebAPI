using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using app.Data;
using app.Models;

namespace app.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SteelPlatePatternController : ControllerBase
{
    private readonly DataContext _context;

    public SteelPlatePatternController(DataContext context)
    {
        _context = context;
    }

    // GET: api/SteelPlatePattern
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SteelPlatePattern>>> GetSteelPlatePatterns()
    {
        return await _context.SteelPlatePatterns.ToListAsync();
    }

    // GET: api/SteelPlatePattern/5
    [HttpGet("{id}")]
    public async Task<ActionResult<SteelPlatePattern>> GetSteelPlatePattern(Guid id)
    {
        var steelPlatePattern = await _context.SteelPlatePatterns.FindAsync(id);

        if (steelPlatePattern == null)
        {
            return NotFound();
        }

        return steelPlatePattern;
    }

    // PUT: api/SteelPlatePattern/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSteelPlatePattern(Guid id, SteelPlatePattern steelPlatePattern)
    {
        if (id != steelPlatePattern.Id)
        {
            return BadRequest();
        }

        _context.Entry(steelPlatePattern).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SteelPlatePatternExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/SteelPlatePattern
    [HttpPost]
    public async Task<ActionResult<SteelPlatePattern>> PostSteelPlatePattern(SteelPlatePattern steelPlatePattern)
    {
        _context.SteelPlatePatterns.Add(steelPlatePattern);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetSteelPlatePattern), new { id = steelPlatePattern.Id }, steelPlatePattern);
    }

    // DELETE: api/SteelPlatePattern/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSteelPlatePattern(Guid id)
    {
        var steelPlatePattern = await _context.SteelPlatePatterns.FindAsync(id);
        if (steelPlatePattern == null)
        {
            return NotFound();
        }

        _context.SteelPlatePatterns.Remove(steelPlatePattern);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool SteelPlatePatternExists(Guid id)
    {
        return _context.SteelPlatePatterns.Any(e => e.Id == id);
    }
}