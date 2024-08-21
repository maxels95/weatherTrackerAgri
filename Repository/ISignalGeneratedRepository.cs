using System.Collections.Generic;
using System.Threading.Tasks;
using AgriWeatherTracker.Models;

namespace AgriWeatherTracker.Repository
{
    public interface ISignalGeneratedRepository
    {
        Task<IEnumerable<SignalGenerated>> GetAllSignalsAsync();
        Task<SignalGenerated?> GetSignalByIdAsync(int id);
        Task AddSignalAsync(SignalGenerated signalGenerated);
        Task UpdateSignalAsync(SignalGenerated signalGenerated);
        Task DeleteSignalAsync(int id);
    }
}
