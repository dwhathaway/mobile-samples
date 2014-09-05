using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Xamarin.Forms.Platform.Android;

namespace InternationalizationSample.Android
{
	public class AndroidStrings : IStrings
	{
		private static Resource _resource { get; set; }
		private static Context _context { get; set; }

		public AndroidStrings(Context context)
		{
			_context = context;
		}

		public string GetString(string stringName)
		{
			List<string> messages = new List<string> ();

			string resId = typeof(Resource.String).GetFields ().Where (f => f.Name.StartsWith (stringName)).First ().GetRawConstantValue ().ToString ();

			return _context.GetString (int.Parse (resId));
		}
	}

	[Activity (Label = "InternationalizationSample.Android.Android", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : AndroidActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Xamarin.Forms.Forms.Init (this, bundle);

			AndroidStrings strings = new AndroidStrings (this);

			App.Strings = strings;

			SetPage (App.GetMainPage ());
		}
	}
}