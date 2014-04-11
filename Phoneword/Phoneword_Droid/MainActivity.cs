using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Phoneword.Common;

namespace Phoneword_Droid
{
	[Activity (Label = "Phoneword", MainLauncher = true)]
	public class MainActivity : Activity
	{
		string _translatedNumber = string.Empty;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button btnTranslate = FindViewById<Button> (Resource.Id.btnTranslate);
			Button btnCall = FindViewById<Button> (Resource.Id.btnCall);
			Button btnCallHistory = FindViewById<Button> (Resource.Id.btnCallHistory);

			EditText txtInput = FindViewById<EditText> (Resource.Id.txtInput);

			btnTranslate.Click += (object sender, EventArgs e) => {

				_translatedNumber = Phoneword.Common.PhonewordTranslator.ToNumber(txtInput.Text);

				btnCall.Text = "Call " + _translatedNumber;

			};

			btnCall.Click += (object sender, EventArgs e) =>  {

				string folder = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);

				Database db = new Database(System.IO.Path.Combine (folder, "Phoneword.db"));

				db.Insert(new CallHistoryItem() { Number = _translatedNumber, Date = DateTime.Now });

				Intent i = new Intent(Intent.ActionCall);

				i.SetData(Android.Net.Uri.Parse("tel:" + _translatedNumber));
				StartActivity(i);

			};

			btnCallHistory.Click += (object sender, EventArgs e) => {
				Intent historyIntent = new Intent(this, typeof(CallHistoryActivity));
				StartActivity(historyIntent);
			};
		}
	}


}


