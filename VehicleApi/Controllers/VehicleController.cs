using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleApi.Data;
using VehicleApi.Models;

namespace VehicleApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly AppDbContext _ctx;
        public VehicleController(AppDbContext ctx) => _ctx = ctx;

        [HttpGet("by-plate/{plate}")]
        public async Task<IActionResult> GetByPlate(string plate)
        {
            var normalized = plate?.Trim().ToUpperInvariant() ?? string.Empty;
            var vehicle = await _ctx.Vehicles.FirstOrDefaultAsync(v => v.Plate == normalized);
            if (vehicle == null) return NotFound();
            return Ok(vehicle);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVehicleRequest req)
        {
            try
            {
                var exists = await _ctx.Vehicles.AnyAsync(v => v.Plate == req.Plate.Trim().ToUpperInvariant());
                if (exists) return BadRequest(new { error = "Veículo já cadastrado com essa placa" });

                var vehicle = new Vehicle(req.Plate, req.Make, req.Model, req.Year);
                await _ctx.Vehicles.AddAsync(vehicle);
                await _ctx.SaveChangesAsync();

                return CreatedAtAction(nameof(GetByPlate), new { plate = vehicle.Plate }, vehicle);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        public record CreateVehicleRequest(string Plate, string Make, string Model, int Year);
    }
}
