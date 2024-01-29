// AgenciesController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mobilis; 


[ApiController]
[Route("api/[controller]")]
public class AgenciesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AgenciesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAgencies()
    {
        var agencies = await _context.Agencies.ToListAsync();
        return Ok(agencies);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAgencyById(int id)
    {
        var agency = await _context.Agencies.FindAsync(id);
        if (agency == null)
        {
            return NotFound();
        }
        return Ok(agency);
    }

    [HttpPost]
    public async Task<IActionResult> AddAgency([FromBody] Agency newAgency)
    {
        _context.Agencies.Add(newAgency);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAgencyById), new { id = newAgency.Id }, newAgency);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAgency(int id, [FromBody] Agency updatedAgency)
    {
        if (id != updatedAgency.Id)
        {
            return BadRequest();
        }

        _context.Entry(updatedAgency).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Agencies.Any(a => a.Id == id))
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
    public async Task<IActionResult> DeleteAgency(int id)
    {
        var agency = await _context.Agencies.FindAsync(id);
        if (agency == null)
        {
            return NotFound();
        }

        _context.Agencies.Remove(agency);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
