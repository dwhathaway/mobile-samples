using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AIG_Common
{
    public class ServiceClient
    {
        static string _baseUrl = "http://10.0.2.120:9002{0}";
        //static string _baseUrl = "http://192.168.56.1:9002{0}";

        public static async Task<List<Product>> GetProductsAsync()
        {
            List<Product> products = new List<Product>();

            var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var request = new HttpRequestMessage(HttpMethod.Get, string.Format(_baseUrl, "/home/getproducts"));

            try
            {
                HttpResponseMessage requestResult = await client.SendAsync(request);

                var responseText = await requestResult.Content.ReadAsStringAsync();

                products = DeserializeResponse<List<Product>>(responseText);
            }
            catch (Exception ex)
            {
                string exMessage = ex.Message;
                products.Add(new Product() { ProductName = exMessage });
            }

            return products;
        }

        private static T DeserializeResponse<T>(string jsonResponse)
        {
            return DeserializeResponse<T>(jsonResponse, string.Empty);
        }

        /// <summary>
        /// Accepts a JSON string and deserializes it to a given object of type T
        /// </summary>
        /// <typeparam name="T">Type of the parameter to add</typeparam>
        /// <param name="jsonResponse">JSON data to deserialize</param>
        /// <param name="rootNode">Name of the root node (if any) to grab the data to deserialize</param>
        /// <returns></returns>
        private static T DeserializeResponse<T>(string jsonResponse, string rootNode)
        {
            var returnObject = Activator.CreateInstance<T>();

            if (!string.IsNullOrEmpty(rootNode)) jsonResponse = JObject.Parse(jsonResponse)[rootNode].ToString();
            returnObject = JsonConvert.DeserializeObject<T>(jsonResponse);

            return returnObject;
        }
    }
}
