using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Groceries.Core.Models;

using Microsoft.WindowsAzure.MobileServices;

namespace Groceries.Core.Services
{
	public class GroceryService
	{
		private static MobileServiceClient _serviceClient = new MobileServiceClient ("https://{your_application_name}.azure-mobile.net/", "{your_application_key}");

		public GroceryService()
		{
			CurrentPlatform.Init ();
		}

		public static async Task<List<GroceryItem>> GetGroceriesAsync()
		{
			List<GroceryItem> groceries = new List<GroceryItem> ();

			groceries = await _serviceClient.GetTable<GroceryItem>().ToListAsync();

			return groceries;
		}

		public static async Task<bool> UpdateGroceryItemAsync(GroceryItem groceryItem)
		{
			bool saveSuccess = false;

			try
			{
				await _serviceClient.GetTable<GroceryItem>().UpdateAsync(groceryItem);
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


		public static async Task<bool> CreateGroceryItemAsync(GroceryItem groceryItem)
		{
			bool saveSuccess = false;

			try
			{
				await _serviceClient.GetTable<GroceryItem>().InsertAsync(groceryItem);
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

		public static async Task<bool> DeleteGroceryItemAsync(GroceryItem groceryItem)
		{

			bool saveSuccess = false;

			try
			{
				await _serviceClient.GetTable<GroceryItem>().DeleteAsync(groceryItem);

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
	}
}