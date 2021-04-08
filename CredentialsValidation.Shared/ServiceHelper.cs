using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CredentialsValidation.Shared
{
    public class ServiceHelper 
    {
        readonly string _webAPIBaseURL;

        public ServiceHelper(string WebAPIBaseURL)
        {
            _webAPIBaseURL = WebAPIBaseURL;
        }

        /// <summary>
        /// Method to call WebAPI and validate the provided <see cref="Credentials"/>
        /// </summary>
        /// <param name="CredentialsToBeValidated"></param>
        /// <returns>A <see cref="boolean"/> flag that signifies success or failure</returns>
        public async Task<ResponseEnvelope> ValidateCredentialsAsync(Credentials CredentialsToBeValidated)
        {
            ResponseEnvelope responseEnvelope = new ResponseEnvelope
            {
                Success = false
            };

            try
            {
                using var client = new HttpClient();

                var json = JsonConvert.SerializeObject(CredentialsToBeValidated);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                // Passing service base url  
                client.BaseAddress = new Uri(_webAPIBaseURL);
                client.DefaultRequestHeaders.Clear();

                // Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Sending request to find web api REST service resource using HttpClient
                HttpResponseMessage Res = await client.PostAsync("Credentials", data);

                // Checking if the response is successful or not
                if (Res.IsSuccessStatusCode)
                {
                    // Storing the response details recieved from web api   
                    var response = Res.Content.ReadAsStringAsync().Result;

                    // Deserializing the response recieved from web api
                    responseEnvelope = JsonConvert.DeserializeObject<ResponseEnvelope>(response);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError($"{nameof(ServiceHelper)} : {nameof(ValidateCredentialsAsync)}", ex);
            }

            return responseEnvelope;
        }
    }
}
