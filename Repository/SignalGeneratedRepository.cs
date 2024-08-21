using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AgriWeatherTracker.Data;
using AgriWeatherTracker.Models;

namespace AgriWeatherTracker.Repository
{
    public class SignalGeneratedRepository : ISignalGeneratedRepository
    {
        private readonly AppDbContext _context;

        public SignalGeneratedRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SignalGenerated>> GetAllSignalsAsync()
        {
            return await _context.SignalsGenerated.ToListAsync();
        }

        public async Task<SignalGenerated?> GetSignalByIdAsync(int id)
        {
            return await _context.SignalsGenerated.FindAsync(id);
        }

        public async Task AddSignalAsync(SignalGenerated signalGenerated)
        {
            await _context.SignalsGenerated.AddAsync(signalGenerated);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSignalAsync(SignalGenerated signalGenerated)
        {
            _context.SignalsGenerated.Update(signalGenerated);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSignalAsync(int id)
        {
            var signalGenerated = await _context.SignalsGenerated.FindAsync(id);
            if (signalGenerated != null)
            {
                _context.SignalsGenerated.Remove(signalGenerated);
                await _context.SaveChangesAsync();
            }
        }
    }
}
