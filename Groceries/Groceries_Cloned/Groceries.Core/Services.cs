using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

			System.Net.WebRequest request = WebRequest.Create("https://api.parse.com/1/classes/Item");

			request.ContentType = "application/json";
			request.Method = "GET";
			request.Headers ["X-Parse-Application-Id"] = _appId;
			request.Headers ["X-Parse-REST-API-Key"] = _apiKey;

			WebResponse response = request.GetResponse();

			Stream dataStream = response.GetResponseStream ();

			StreamReader reader = new StreamReader (dataStream);

			// Read the content.
			string responseFromServer = reader.ReadToEnd ();

			//Close everything up...
			reader.Close ();
			dataStream.Close ();
			response.Close ();

			groceries = DeserializeResponse<List<GroceryItem>> (responseFromServer, "results");

			return groceries;
		}

		public static bool UpdateGroceryItem(GroceryItem groceryItem)
		{
			System.Net.WebRequest request = WebRequest.Create("https://api.parse.com/1/classes/Item/" + groceryItem.ObjectId);

			request.ContentType = "application/json";
			request.Method = "PUT";
			request.Headers ["X-Parse-Application-Id"] = _appId;
			request.Headers ["X-Parse-REST-API-Key"] = _apiKey;

			byte[] buffer = Encoding.GetEncoding("UTF-8").GetBytes(JsonConvert.SerializeObject(groceryItem));

			string result = System.Convert.ToBase64String(buffer);

			Stream reqstr = request.GetRequestStream();
			reqstr.Write(buffer, 0, buffer.Length);
			reqstr.Close();

			bool saveSuccess = false;

			try
			{
				WebResponse response = request.GetResponse();
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
			System.Net.WebRequest request = WebRequest.Create("https://api.parse.com/1/classes/Item");

		
			request.ContentType = "application/json";
			request.Method = "POST";
			request.Headers ["X-Parse-Application-Id"] = _appId;
			request.Headers ["X-Parse-REST-API-Key"] = _apiKey;

			byte[] buffer = Encoding.GetEncoding("UTF-8").GetBytes(JsonConvert.SerializeObject(groceryItem));

			string result = System.Convert.ToBase64String(buffer);

			Stream reqstr = request.GetRequestStream();
			reqstr.Write(buffer, 0, buffer.Length);
			reqstr.Close();

			bool saveSuccess = false;

			try
			{
				WebResponse response = request.GetResponse();
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