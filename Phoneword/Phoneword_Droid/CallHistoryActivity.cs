using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Phoneword.Common;

namespace Phoneword_Droid
{
	[Activity (Label = "CallHistory")]			
	public class CallHistoryActivity : Activity
	{
		List<CallHistoryItem> _callHistory = new List<CallHistoryItem>();

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			base.SetContentView (Resource.Layout.CallHistory);

			string folder = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);

			Database db = new Database(System.IO.Path.Combine (folder, "Phoneword.db"));

			_callHistory = db.QueryCallHistory ().Cast<CallHistoryItem>().ToList(); 

			var callHistoryAdapter = new CallHistoryAdapter (this, _callHistory);

			ListView recentCallsList = FindViewById<ListView> (Resource.Id.callHistory);
			recentCallsList.Adapter = callHistoryAdapter;

			recentCallsList.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => {
				Intent i = new Intent(Intent.ActionCall);

				i.SetData(Android.Net.Uri.Parse("tel:" + _callHistory[e.Position].Number));
				StartActivity(i);			
			};
		}
	}

	public class CallHistoryAdapter : BaseAdapter<CallHistoryItem>
	{
		List<CallHistoryItem> items;
		Activity context;

		public CallHistoryAdapter(Activity context, List<CallHistoryItem> items) : base() 
		{
			this.context = context;
			this.items = items;
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public override CallHistoryItem this[int position]
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
			view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = items[position].Number.ToString();
			return view;
		}
	}
}

