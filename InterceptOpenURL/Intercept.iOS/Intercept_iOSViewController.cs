using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Intercept.iOS
{
	public partial class Intercept_iOSViewController : UIViewController
	{
		public Intercept_iOSViewController () : base ("Intercept_iOSViewController", null)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.

			webView1.ShouldStartLoad += LoadHook;

			NSUrl url = new NSUrl("http://www.google.com");

			NSUrlRequest request = new NSUrlRequest(url);

			webView1.LoadRequest (request);
		}

		bool LoadHook (UIWebView sender, NSUrlRequest request, UIWebViewNavigationType navType){
			var requestString = request.Url.AbsoluteString;

			// determine here if this is a url you want to open in Safari or not
			if (CanOpenUrl (requestString)){
				UIApplication.SharedApplication.OpenUrl (new NSUrl (requestString));
				return true;
			} else {
				UIAlertView alertView = new UIAlertView ("Access Denied", "This action is prohibited", null, "Back", null);
				alertView.Show ();

				return false;
			}
		}

		public bool CanOpenUrl(string requerstString)
		{
			// Implemente logic here to test if this app is allowed for this data

			Console.WriteLine (requerstString == "Is allowed...");

			return true;
		}
	}
}

