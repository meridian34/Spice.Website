using System.Collections.Generic;
using System.Threading.Tasks;

namespace Spice.Website.Services.Interfaces
{
    public interface ISpiceService
    {
        Task<IReadOnlyCollection<Models.Spice>> GetSpicesAsync();

        Task AddSpiceAsync(Models.Spice spice);

        Task DeleteSpiceAsync(int spiceId);
    }
}
