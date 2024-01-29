using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Mobilis; 

[ApiController]
[Route("api/[controller]")]
public class PointOfSalesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PointOfSalesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var pointOfSales = await _context.PointOfSales.ToListAsync();
        return Ok(pointOfSales);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var pointOfSale = await _context.PointOfSales.FindAsync(id);
        if (pointOfSale == null)
        {
            return NotFound();
        }
        return Ok(pointOfSale);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PointOfSale pointOfSale)
    {
        pointOfSale.Id = Guid.NewGuid();
        _context.PointOfSales.Add(pointOfSale);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = pointOfSale.Id }, pointOfSale);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] PointOfSale updatedPointOfSale)
    {
        if (id != updatedPointOfSale.Id)
        {
            return BadRequest();
        }

        _context.Entry(updatedPointOfSale).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.PointOfSales.Any(p => p.Id == id))
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var pointOfSale = await _context.PointOfSales.FindAsync(id);
        if (pointOfSale == null)
        {
            return NotFound();
        }

        _context.PointOfSales.Remove(pointOfSale);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
