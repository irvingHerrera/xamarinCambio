

namespace App3
{

    using App3.Models;
    using Newtonsoft.Json;
    using Plugin.Connectivity;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class ApiService
    {
        public async Task<Response> CheckConnection()
        {
            if (!CrossConnectivity.IsSupported)
                return new Response
                {
                    IsSuccess = false,
                    Message = "Check your internet settings"
                }; ;

            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Check your internet settings"
                };
            }

            var response = await CrossConnectivity.Current.IsRemoteReachable("google.com");

            if (!response)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Check your internet connection"
                };
            }

            return new Response
            {
                IsSuccess = true
            };
        }

        public async Task<Response> GetList<T>(string urlBase, string controller)
        {
            try
            {

                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var response = await client.GetAsync(controller);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result
                    };
                }

                var list = JsonConvert.DeserializeObject<List<T>>(result);

                return new Response
                {
                    IsSuccess = true,
                    Result = list
                };
            }
            catch (System.Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}
