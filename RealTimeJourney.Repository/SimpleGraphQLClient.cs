using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace RealTimeJourney.Repository
{
    public class SimpleGraphQlClient
    {
        private readonly RestClient _client;

        public SimpleGraphQlClient(string graphQlApiUrl)
        {
            _client = new RestClient(graphQlApiUrl);

            ServicePointManager.SecurityProtocol =
                SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
        }

        public dynamic Execute(string query, object variables = null,
            Dictionary<string, string> additionalHeaders = null, int timeout = 0)
        {
            var request = new RestRequest("/", Method.POST);
            request.Timeout = timeout;

            if (additionalHeaders != null && additionalHeaders.Count > 0)
                foreach (var additionalHeader in additionalHeaders)
                    request.AddHeader(additionalHeader.Key, additionalHeader.Value);

            request.AddJsonBody(new
            {
                query,
                variables
            });

            return JObject.Parse(_client.Execute(request).Content);
        }
    }
}