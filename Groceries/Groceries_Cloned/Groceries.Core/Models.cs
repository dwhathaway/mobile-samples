using System;
using Newtonsoft.Json;

namespace Groceries.Core.Models
{
	public class GroceryItem
	{
		[JsonProperty("objectId")]
		public string ObjectId { get; set; }

		[JsonProperty("Name")]
		public string Title { get; set; }

		[JsonProperty("Description")]
		public string Description { get; set; }

		[JsonProperty("Quantity")]
		public int Quantity { get; set; }
	}
}