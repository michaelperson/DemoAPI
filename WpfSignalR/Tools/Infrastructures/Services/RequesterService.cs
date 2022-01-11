using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WpfSignalR.Tools.Infrastructures.Services
{
    public class RequesterService
    {
        private readonly HttpClient  client;
        private readonly string URL;
        public RequesterService(string baseUrl)
        {
            client = new HttpClient();
            URL = baseUrl;
        }

        /// <summary>
        /// Executes the request to the specified end point.
        /// </summary>
        /// <param name="EndPoint">The end point to request.</param>
        /// <returns>A json string with endpoint response</returns>
        protected async Task<string> Execute(string EndPoint, string urlParameters, bool JsonData = true)
        {
          
            client.BaseAddress = new Uri(URL + EndPoint + urlParameters);

            if (JsonData)
            {
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            }
             

            try
            {
                HttpResponseMessage response = await client.GetAsync("");   
                if (response.IsSuccessStatusCode)
                {
                    Task<string> jsonString = response.Content.ReadAsStringAsync();

                    jsonString.Wait();
                    return jsonString.Result;

                }
                else
                {
                    Debug.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                    return "";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("{0} ({1})", "HttpException", ex.Message);
                return "";
            }

        }
    }
}
 
