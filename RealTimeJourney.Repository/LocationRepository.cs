using System;
using System.Threading.Tasks;
using RealTimeJourney.Repository.Models;

namespace RealTimeJourney.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private readonly SimpleGraphQlClient _simpleGraphQlClient;

        public LocationRepository()
        {
            _simpleGraphQlClient = new SimpleGraphQlClient("https://api.entur.org/stop_places/1.0/graphql");
        }

        public async Task<StopsModel> Get(string stopName, TransportTypes transportType)
        {
            var queryString = @"mutation stopPlace ($transportType: String!
                                                    $query: String!)
                                                        { stopPlace(size: 10, stopPlaceType: $transportType, ,query: $query) 
                                                            { 
                                                             name 
                                                                { 
                                                                value 
                                                                } 
                                                             }
                                                         }";
            try
            {
                var result = await _simpleGraphQlClient.Execute(queryString, new {transport = transportType.ToString(), query = stopName});
                var stopsModels = new StopsModel
                {
                    Name = result.data["stopPlace"].name.value,
                    StopId = result.data["stopPlace"].id
                };

                return stopsModels;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}