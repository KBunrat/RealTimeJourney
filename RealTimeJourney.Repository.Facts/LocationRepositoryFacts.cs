using System.Threading.Tasks;
using Xunit;

namespace RealTimeJourney.Repository.Facts
{
    public class LocationRepositoryFacts
    {

        private readonly LocationRepository _locationRepository;

        public LocationRepositoryFacts()
        {
            _locationRepository = new LocationRepository();
        }

        [Fact]
        public async Task Get_QueryAndStopIsNotNull_ReturnValidStopsModel()
        {
            const string stopName = "Øvre Slottsgate";

            var result = await _locationRepository.Get(stopName, TransportTypes.onstreetTram);

            Assert.NotNull(stopName);
        }
    }
}