using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Webkit;
using Android.Content.PM;
using System.Collections.Generic;

namespace Intercept.Droid
{
	[Activity (Label = "Intercept.Android", MainLauncher = true)]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			WebView webView1 = FindViewById<WebView> (Resource.Id.webView1);

			MyWebViewClient client = new MyWebViewClient ();

			webView1.SetWebViewClient (client);

			webView1.LoadUrl ("https://www.google.com");
		}
	}

	class MyWebViewClient : WebViewClient
	{
		public override bool ShouldOverrideUrlLoading (WebView view, string url)
		{
			if (!CanOpenUrl(view.Context, url)) {
				return base.ShouldOverrideUrlLoading (view, url);
			} else {
				// Not allowed, so return true to overide loading
				Toast.MakeText(view.Context, "Access Denied", ToastLength.Long).Show(); 
				return true;
			}
		}

		public override void OnLoadResource (WebView view, string url)
		{
			if (CanOpenUrl (view.Context, url)) {
				base.OnLoadResource (view, url);
			}
		}

		public bool CanOpenUrl(Context context, string requestString)
		{
			// Implemente logic here to test if this app is allowed for this data
			if(GetIntentsForAction (context, requestString))

			Console.WriteLine (requestString == "Is allowed...");

			return false;
		}

		public bool GetIntentsForAction(Context context, String action) {
			PackageManager packageManager = context.PackageManager;
			Intent intent = new Intent(Intent.ActionSend);
			List<ResolveInfo> resolveInfo = new List<ResolveInfo>(packageManager.QueryIntentActivities (intent, PackageInfoFlags.MatchDefaultOnly));

			if (resolveInfo.Count > 0) {
				return true;
			}
			return false;
		}
	}
}


