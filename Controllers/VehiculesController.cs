using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BimaTech.Parking.Data;
using BimaTech.Parking.Models;

namespace BimaTech.Parking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiculesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public VehiculesController(ApplicationDbContext context) => _context = context;

        [HttpPost]
        public async Task<IActionResult> Create(Vehicule vehicule)
        {
            bool existe = await _context.Vehicules
                .AnyAsync(v => v.Immatriculation == vehicule.Immatriculation);
            if (existe)
                return Conflict("Cette immatriculation existe déjà.");

            _context.Vehicules.Add(vehicule);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Create), new { id = vehicule.Id }, vehicule);
        }

        [HttpPut("{vehiculeId}/employe/{employeId}")]
        public async Task<IActionResult> Associer(int vehiculeId, int employeId)
        {
            var vehicule = await _context.Vehicules.FindAsync(vehiculeId);
            var employe = await _context.Employes.FindAsync(employeId);
            if (vehicule == null || employe == null) return NotFound();

            vehicule.EmployeId = employeId;
            await _context.SaveChangesAsync();
            return Ok(vehicule);
        }

        [HttpGet("employe/{employeId}")]
        public async Task<IActionResult> ParEmploye(int employeId)
        {
            var vehicules = await _context.Vehicules
                .Where(v => v.EmployeId == employeId)
                .ToListAsync();
            return Ok(vehicules);
        }
    }
}