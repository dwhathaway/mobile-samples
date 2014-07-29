using System;
using Newtonsoft.Json;

namespace Groceries.Core.Models
{
	public class GroceryItem
	{
		public string id { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public int Quantity { get; set; }
	}
}