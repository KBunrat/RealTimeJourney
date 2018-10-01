using System.Threading.Tasks;
using RealTimeJourney.Repository.Models;

namespace RealTimeJourney.Repository
{
    public interface ILocationRepository
    {
        Task<StopsModel> Get(string stopName, TransportTypes transportType);
    }
}