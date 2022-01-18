using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using WpfSignalR.Tools.Infrastructures.Security;

namespace WpfSignalR.Tools.Infrastructures.Services
{
    public abstract class RequesterService  
    {
        private SecureString Jwt;
        public enum Method { GET,POST,PUT, PATCH, DELETE};
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
        protected async Task Execute(string EndPoint, string urlParameters, Method method= Method.GET, bool JsonData = true )
        {

            client.BaseAddress = new Uri(URL + EndPoint);
            if (JsonData)
            {
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            }

            switch (method)
            {
                case Method.GET:  await Get(urlParameters);break;
                     
                case Method.POST:
                     await Post(urlParameters);  break;                   
                     
                case Method.PUT: break;
                case Method.PATCH: break;
                case Method.DELETE: break;
                default:
                    break;
            }

        }

        private async Task<bool> Post(string urlParameters)
        {
            try
            {
                HttpContent content = new StringContent(urlParameters, Encoding.UTF8, "application/json");


                client.BaseAddress = new Uri(client.BaseAddress.ToString());

                HttpResponseMessage response = client.PostAsync("", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    Task<string> jsonString = response.Content.ReadAsStringAsync();

                    jsonString.Wait();
                    await Task.CompletedTask;
                    Jwt = SecurityTools.ConvertToSecureString(jsonString.Result);
                    return true ;
                }
                else
                {
                    Debug.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("{0} ({1})", "HttpException", ex.Message);
                return false;
            }
        }

        private async Task<string> Get(string urlParameters)
        {
            try
            {
                client.BaseAddress = new Uri(client.BaseAddress.ToString() + urlParameters);

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
 
