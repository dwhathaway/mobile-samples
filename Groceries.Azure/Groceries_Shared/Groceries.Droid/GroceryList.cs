using System;
using System.Collections.Generic;
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
	[Activity (Label = "Groceries", MainLauncher = true)]
	public class GroceryList : Activity
	{
		List<GroceryItem> _groceries = new List<GroceryItem> ();
		ListView _groceryList;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			
			this.SetContentView (Resource.Layout.GroceryList);

			_groceryList = FindViewById<ListView> (Resource.Id.listView1);

			_groceryList.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => {

				var t = _groceries[e.Position];

				Intent i = new Intent (this, typeof(GroceryDetailActivity));

				i.PutExtra ("id", t.id);
				i.PutExtra ("title", t.Title);
				i.PutExtra ("description", t.Description);
				i.PutExtra ("quantity", t.Quantity);

				StartActivity (i);
			};

			_groceryList.ItemLongClick += (object sender, AdapterView.ItemLongClickEventArgs e) => {
				var t = _groceries[e.Position];

				Toast.MakeText (this, "You long pressed on " + t.Title, ToastLength.Long).Show ();
			};

			Button addButton = FindViewById<Button> (Resource.Id.button1);

			addButton.Click += (object sender, EventArgs e) => {
				
				Intent i = new Intent (this, typeof(GroceryEditActivity));
				StartActivity (i);
			};

			Toast.MakeText (this, "Retrieving grocery list...", ToastLength.Long).Show ();

			RefreshData ();

			//Use the Task Parallel Library (TPL) to start a new thread to retreive a list of groceries from the server
//			Task.Factory.StartNew(() => {
//				_groceries = GroceryService.GetGroceries ();
//			}).ContinueWith((prevTask) => {
//
//				//Check the response to see if there were any errors on the background task
//				if(prevTask.Exception != null)
//				{
//					Toast.MakeText (this, "Error retrieving results from server...", ToastLength.Long).Show ();
//				}
//				else
//				{
//					groceryList.Adapter = new GroceryListAdapter (this, _groceries);
//				}
//			}, TaskScheduler.FromCurrentSynchronizationContext());

		}

		public async void RefreshData()
		{
			_groceries = await GroceryService.GetGroceriesAsync();
			if (_groceries != null) {
				_groceryList.Adapter = new GroceryListAdapter (this, _groceries);
			}
		}
	}

	public class GroceryListAdapter : BaseAdapter<GroceryItem>
	{
		List<GroceryItem> items;
		Activity context;

		public GroceryListAdapter(Activity context, List<GroceryItem> items) : base() 
		{
			this.context = context;
			this.items = items;
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public override GroceryItem this[int position]
		{  
			get { return items[position]; }
		}

		public override int Count {
			get { return items.Count; }
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View view = convertView; // re-use an existing view, if one is available
			if (view == null) // otherwise create a new one
				view = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);
			view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = items[position].Title;
			return view;
		}
	}
}