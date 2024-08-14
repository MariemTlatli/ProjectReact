using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
public class CompanyController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CompanyController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Company
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompanyDTO>>> GetCompanies()
    {
        return await _context.Companies
            .Select(company => company.ToDTO())
            .ToListAsync();
    }

    // GET: api/Company/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<CompanyDTO>> GetCompany(int id)
    {
        // Récupérer l'entité `Company` avec l'Id spécifié
        var company = await _context.Companies.FirstOrDefaultAsync(c => c.Id == id);

        // Vérifier si l'entité existe
        if (company == null)
        {
            return NotFound();
        }

        // Convertir l'entité récupérée en DTO
        var companyDTO = company.ToDTO();

        // Retourner le DTO
        return companyDTO;
    }
    // POST: api/Company
    // POST: api/Company
    [HttpPost]
    public async Task<ActionResult<CompanyDTO>> PostCompany(CompanyDTO companyDTO)
    {
        // Validation
        if (companyDTO == null)
        {
            return BadRequest("Company data is required.");
        }

        var company = companyDTO.ToEntity();

        // Validation des propriétés
        if (string.IsNullOrEmpty(company.Name) || string.IsNullOrEmpty(company.Address) || string.IsNullOrEmpty(company.Phone))
        {
            return BadRequest("Name, Address, and Phone are required fields.");
        }

        _context.Companies.Add(company);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            // Gestion des erreurs lors de la sauvegarde
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }

        return CreatedAtAction(nameof(GetCompany), new { id = company.Id }, company.ToDTO());
    }
    [Authorize(Roles = "Admin", Policy = "AdminPolicy")]
    // PUT: api/Company/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCompany(int id, CompanyDTO companyDTO)
    {
        if (id != companyDTO.Id)
        {
            return BadRequest();
        }

        var company = companyDTO.ToEntity();

        _context.Entry(company).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CompanyExists(id))
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

    // DELETE: api/Company/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCompany(int id)
    {
        var company = await _context.Companies.FindAsync(id);
        if (company == null)
        {
            return NotFound();
        }

        _context.Companies.Remove(company);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CompanyExists(int id)
    {
        return _context.Companies.Any(e => e.Id == id);
    }
}
