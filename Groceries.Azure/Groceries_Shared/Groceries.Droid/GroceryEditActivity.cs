using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
	[Activity (Label = "GroceryEditActivity")]			
	public class GroceryEditActivity : Activity
	{
		private GroceryItem _groceryItem;

		EditText txtTitle;
		EditText txtDescription;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			
			this.SetContentView (Resource.Layout.GroceryEdit);
			
			//Pull out all of the "extras" that were passed in the intent
			_groceryItem = new GroceryItem () {
				id = Intent.GetStringExtra ("id"),
				Title = Intent.GetStringExtra ("title"),
				Description = Intent.GetStringExtra ("description"),
				Quantity = Intent.GetIntExtra ("quantity", 0)
			};
			
			//Grab each of the text views, which display the details about the grocery item, and assign them values
			txtTitle = FindViewById<EditText> (Resource.Id.editText1);
			txtTitle.Text = _groceryItem.Title;
			
			txtDescription = FindViewById<EditText> (Resource.Id.editText2);
			txtDescription.Text = _groceryItem.Description;

			//Grab the spinner control, and bind it to the resource array
			Spinner spinner = FindViewById<Spinner> (Resource.Id.spinner1);
			
			spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs> (spinner_ItemSelected);

			var adapter = ArrayAdapter.CreateFromResource (this, Resource.Array.quantity_array, Android.Resource.Layout.SimpleSpinnerItem);
			
			adapter.SetDropDownViewResource (Android.Resource.Layout.SimpleSpinnerDropDownItem);
			spinner.Adapter = adapter;

			//Set the selected item - this is easy because we have a 0-based array, which matchs the quantity
			spinner.SetSelection (_groceryItem.Quantity);

			//Finally, wire up the "Save" button
			Button btnEdit = FindViewById<Button> (Resource.Id.btnEdit);

			btnEdit.Click += (object sender, EventArgs e) => {

				//Persist these changes somewhere, and then navigate back to the "Grocery Detail" view				
				Toast.MakeText (this, "Saving...", ToastLength.Long).Show ();

				_groceryItem.Title = txtTitle.Text;
				_groceryItem.Description = txtDescription.Text;

				Task.Factory.StartNew(() => {

					if(!string.IsNullOrEmpty(_groceryItem.id)) {
						GroceryService.UpdateGroceryItemAsync(_groceryItem);
					}
					else {
						GroceryService.CreateGroceryItemAsync (_groceryItem);
					}
				}).ContinueWith((prevTask) => {					
						Intent i = new Intent(this, typeof(GroceryList));					
						StartActivity(i);
				}, TaskScheduler.FromCurrentSynchronizationContext ());
			};
		}

		private void spinner_ItemSelected (object sender, AdapterView.ItemSelectedEventArgs e)
		{
			_groceryItem.Quantity = int.Parse(((Spinner)sender).GetItemAtPosition (e.Position).ToString());
		}
	}
}