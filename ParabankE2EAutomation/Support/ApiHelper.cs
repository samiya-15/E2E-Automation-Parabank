using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ParabankE2EAutomation.Support
{
    public class ApiHelper
    {
        private RestClient _client;

        public ApiHelper(string baseUrl)
        {
            _client = new RestClient(baseUrl);
        }

        public async Task<RestResponse> PostRequest(string resource, object body)
        {
            var request = new RestRequest(resource, Method.Post);
            request.AddJsonBody(body);
            return await _client.ExecuteAsync(request);
        }

        public async Task<RestResponse> GetRequest(string resource)
        {
            var request = new RestRequest(resource, Method.Get);
            return await _client.ExecuteAsync(request);
        }

        public async Task<RestResponse> GetRequestWithQueryParams(string resource, Dictionary<string, string> queryParams)
        {
            var request = new RestRequest(resource, Method.Get);

            // Add query parameters to the request
            foreach (var param in queryParams)
            {
                request.AddParameter(param.Key, param.Value);
            }

            return await _client.ExecuteAsync(request);
        }

        public async Task<RestResponse> PostRequestWithQueryParams(string resource, Dictionary<string, string> queryParams)
        {
            var request = new RestRequest(resource, Method.Post);

            // Add query parameters to the request
            foreach (var param in queryParams)
            {
                request.AddParameter(param.Key, param.Value);
            }

            return await _client.ExecuteAsync(request);
        }
    }

}
