using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System.Net.Http;
using System.Net.Http.Headers;

using Groceries.Core.Models;

namespace Groceries.Core.Services
{
	public class GroceryService
	{
		static string _appId = "Uh9gagNv8mXILguBtI9xsyd1tpG68DaKfyNQJsQy";
		static string _apiKey = "1uTSJHR5c0n9LEKtP4xuA5fcqV2BCyzCiW6euuhS";

		public static List<GroceryItem> GetGroceries()
		{
			List<GroceryItem> groceries = new List<GroceryItem> ();

			var client = new HttpClient ();

			client.DefaultRequestHeaders.Accept.Add (new MediaTypeWithQualityHeaderValue ("application/json"));

			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://api.parse.com/1/classes/Item");

			request.Headers.Add ("X-Parse-Application-Id", _appId);
			request.Headers.Add ("X-Parse-REST-API-Key", _apiKey);

			var response = client.SendAsync(request).Result;

			var responseText = response.Content.ReadAsStringAsync().Result;

			groceries = DeserializeResponse<List<GroceryItem>> (responseText, "results");

			return groceries;
		}

		public static bool UpdateGroceryItem(GroceryItem groceryItem)
		{
			var client = new HttpClient ();

			client.DefaultRequestHeaders.Accept.Add (new MediaTypeWithQualityHeaderValue ("application/json"));

			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, "https://api.parse.com/1/classes/Item/" + groceryItem.ObjectId);

			request.Headers.Add ("X-Parse-Application-Id", _appId);
			request.Headers.Add ("X-Parse-REST-API-Key", _apiKey);

			request.Content = new StringContent(JsonConvert.SerializeObject(groceryItem));

			bool saveSuccess = false;

			try
			{
				var response = client.SendAsync(request).Result;

				//Grab the response text to make sure that it returned the right data
				var responseText = response.Content.ReadAsStringAsync().Result;

				saveSuccess = true;
			}
			catch(WebException wex)
			{
				//Failed to save
			}
			catch(Exception ex)
			{
				//Something else happened...
			}

			return saveSuccess;
		}


		public static bool CreateGroceryItem(GroceryItem groceryItem)
		{
			var client = new HttpClient ();

			client.DefaultRequestHeaders.Accept.Add (new MediaTypeWithQualityHeaderValue ("application/json"));

			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://api.parse.com/1/classes/Item");

			request.Headers.Add ("X-Parse-Application-Id", _appId);
			request.Headers.Add ("X-Parse-REST-API-Key", _apiKey);

			request.Content = new StringContent(JsonConvert.SerializeObject(groceryItem));

			bool saveSuccess = false;

			try
			{
				var response = client.SendAsync(request).Result;

				//Grab the response text to make sure that it returned the right data
				var responseText = response.Content.ReadAsStringAsync().Result;

				saveSuccess = true;
			}
			catch(WebException wex)
			{
				//Failed to save
			}
			catch(Exception ex)
			{
				//Something else happened...
			}

			return saveSuccess;
		}

		public static bool DeleteGroceryItem(GroceryItem groceryItem)
		{
			var client = new HttpClient ();

			client.DefaultRequestHeaders.Accept.Add (new MediaTypeWithQualityHeaderValue ("application/json"));

			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, "https://api.parse.com/1/classes/Item/" + groceryItem.ObjectId);

			request.Headers.Add ("X-Parse-Application-Id", _appId);
			request.Headers.Add ("X-Parse-REST-API-Key", _apiKey);

			request.Content = new StringContent(JsonConvert.SerializeObject(groceryItem));

			bool saveSuccess = false;

			try
			{
				var response = client.SendAsync(request).Result;

				//Grab the response text to make sure that it returned the right data
				var responseText = response.Content.ReadAsStringAsync().Result;

				saveSuccess = true;
			}
			catch(WebException wex)
			{
				//Failed to save
			}
			catch(Exception ex)
			{
				//Something else happened...
			}

			return saveSuccess;
		}

		private static T DeserializeResponse<T>(string jsonResponse, string rootNode)
		{
			JObject o = JObject.Parse(jsonResponse);

			var returnObject = Activator.CreateInstance<T>();

			try{
				returnObject = JsonConvert.DeserializeObject<T>(o[rootNode].ToString());
			}
			catch(Exception ex) {
			}

			return returnObject;
		}
	}
}