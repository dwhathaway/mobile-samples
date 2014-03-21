using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Groceries.Core.Models;
using Groceries.Core.Services;

namespace Groceries.Droid
{
	[Activity (Label = "GroceryDetailActivity")]			
	public class GroceryDetailActivity : Activity
	{
		private GroceryItem _groceryItem;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			//Set the layout for this activity
			this.SetContentView (Resource.Layout.GroceryDetail);

			//Pull out all of the "extras" that were passed in the intent
			_groceryItem = new GroceryItem () {
				ObjectId = Intent.GetStringExtra ("objectId"),
				Title = Intent.GetStringExtra ("title"),
				Description = Intent.GetStringExtra ("description"),
				Quantity = Intent.GetIntExtra ("quantity", 0)
			};

			//Grab each of the text views, which display the details about the grocery item, and assign them values
			TextView txtTitle = FindViewById<TextView> (Resource.Id.txtTitle);
			txtTitle.Text = _groceryItem.Title;

			TextView txtDescription = FindViewById<TextView> (Resource.Id.txtDescription);
			txtDescription.Text = _groceryItem.Description;

			TextView txtQuantity = FindViewById<TextView> (Resource.Id.txtQuantity);
			txtQuantity.Text = _groceryItem.Quantity.ToString ();

			Button btnEditGrocery = FindViewById<Button> (Resource.Id.btnEditGrocery);

			btnEditGrocery.Click += (object sender, EventArgs e) => {

				Intent i = new Intent(this, typeof(GroceryEditActivity));
				
				i.PutExtra("objectId", _groceryItem.ObjectId);
				i.PutExtra("title", _groceryItem.Title);
				i.PutExtra("description", _groceryItem.Description);
				i.PutExtra("quantity", _groceryItem.Quantity);

				StartActivity(i);
			};
		}
	}
}