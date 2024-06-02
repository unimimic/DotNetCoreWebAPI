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
    private readonly IWebHostEnvironment _env;

    public SteelPlatePatternController(DataContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    // GET: api/SteelPlatePattern
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SteelPlatePattern>>> GetSteelPlatePatterns()
    {
        var steelPlatePatterns = await _context.SteelPlatePatterns.ToListAsync();
        return Ok(steelPlatePatterns);
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

        return Ok(steelPlatePattern);
    }

    // PUT: api/SteelPlatePattern/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSteelPlatePattern(Guid id, [FromForm] SteelPlatePattern steelPlatePattern, IFormFile imageFile)
    {
        if (id != steelPlatePattern.Id)
        {
            return BadRequest();
        }

        if (imageFile != null)
        {
            var imagePath = Path.Combine(_env.WebRootPath, "images", imageFile.FileName);
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }
            steelPlatePattern.ImagePath = $"/images/{imageFile.FileName}";
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
    public async Task<ActionResult<SteelPlatePattern>> PostSteelPlatePattern([FromForm] SteelPlatePattern steelPlatePattern, IFormFile imageFile)
    {
        if (imageFile != null)
        {
            var imagePath = Path.Combine("db", "images", imageFile.FileName);
            var uploadsFolder = Path.Combine("db", "images");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }
            steelPlatePattern.ImagePath = $"/images/{imageFile.FileName}";
        }

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

        if (!string.IsNullOrEmpty(steelPlatePattern.ImagePath))
        {
            var imagePath = Path.Combine(_env.WebRootPath, steelPlatePattern.ImagePath.TrimStart('/'));
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
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