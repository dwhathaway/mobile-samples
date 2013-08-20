using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Java.Lang;

namespace ExceptionHandling.Droid
{
	[Activity (Label = "ExceptionHandling.Droid", MainLauncher = true)]
	public class Activity1 : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			AppDomain.CurrentDomain.UnhandledException += delegate(object sender, UnhandledExceptionEventArgs e) {

				//This statement determines the type (i.e. source) of the exception, and then passes it to a different method
				//This is only for display purposes, and can be replaced with Crittercism or Bugsnag code
				switch (e.ExceptionObject.GetType().ToString())
				{
					case "System.Exception": 
						doSomething(((System.Exception)e.ExceptionObject));
						break;
					case "Java.Lang.Exception":
						doSomething(((Java.Lang.Exception)e.ExceptionObject));
						break;
				}
			};

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.myButton);
			
			button.Click += delegate {

				throw new System.Exception("You clicked a broken button!");
			};

			Button button1 = FindViewById<Button> (Resource.Id.button1);

			button1.Click += delegate {
				
				throw new Java.Lang.Exception("new java exception");
			};
		}

		/// <summary>
		/// Function for handling managed exceptions
		/// </summary>
		/// <param name="ex">Ex.</param>
		private void doSomething(System.Exception ex)
		{
			Console.WriteLine (ex.ToString ());
		}

		/// <summary>
		/// Function for handling native exceptions
		/// </summary>
		/// <param name="ex">Ex.</param>
		private void doSomething(Java.Lang.Exception ex)
		{
			Console.WriteLine (ex.ToString ());
		}
	}
}