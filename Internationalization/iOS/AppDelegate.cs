using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using Xamarin.Forms;

namespace InternationalizationSample.iOS
{
	public class AppleStrings : IStrings
	{
		static NSBundle _bundle;

		public AppleStrings(NSBundle bundle)
		{
			_bundle = bundle;
		}

		public string GetString(string stringName)
		{

			return _bundle.LocalizedString(stringName, null);
		}
	}

	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		UIWindow window;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			Forms.Init ();

			window = new UIWindow (UIScreen.MainScreen.Bounds);

			App.Strings = new AppleStrings (NSBundle.MainBundle);
			
			window.RootViewController = App.GetMainPage ().CreateViewController ();
			window.MakeKeyAndVisible ();
			
			return true;
		}
	}
}

