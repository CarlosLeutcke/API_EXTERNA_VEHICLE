using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VehicleApi.Models;

namespace VehicleApi.Data
{
    public static class SeedData
    {
        public static async Task EnsureSeededAsync(AppDbContext ctx)
        {
            if (await ctx.Vehicles.AnyAsync()) return;

            ctx.Vehicles.Add(new Vehicle("ABC1234", "Fiat", "Uno", 2010));
            ctx.Vehicles.Add(new Vehicle("XYZ1A23", "Volkswagen", "Gol", 2019));
            await ctx.SaveChangesAsync();
        }
    }
}
