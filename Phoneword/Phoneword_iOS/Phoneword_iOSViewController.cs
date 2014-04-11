using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

using Phoneword.Common;

namespace Phoneword_iOS
{
	public partial class Phoneword_iOSViewController : UIViewController
	{
		string _translatedNumber;

		public Phoneword_iOSViewController (IntPtr handle) : base (handle)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		#region View lifecycle

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.


			btnTranslate.TouchUpInside += (object sender, EventArgs e) => {

				txtInput.ResignFirstResponder ();

				//Translate the number
				_translatedNumber = Phoneword.Common.PhonewordTranslator.ToNumber(txtInput.Text);

				//Change the "Call" text
				if(!string.IsNullOrEmpty(_translatedNumber)) {
					btnCall.SetTitle("Call " + _translatedNumber, UIControlState.Normal);
					btnCall.Enabled = true;
				} else {
					btnCall.SetTitle("Call", UIControlState.Normal);
					btnCall.Enabled = false;
				}
			};

			btnCall.TouchUpInside += (object sender, EventArgs e) => {

				string folder = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);

				Database db = new Database(System.IO.Path.Combine (folder, "Phoneword.db"));

				db.Insert(new CallHistoryItem() { Number = _translatedNumber, Date = DateTime.Now });

				var url = new NSUrl("tel:" + _translatedNumber);

				if(!UIApplication.SharedApplication.OpenUrl(url)) {
					var alertView = new UIAlertView("Not supported",
						"Scheme 'tel:' is not supported on this device",
						null,
						"OK",
						null);
					alertView.Show();
				}
			};
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
		}

		#endregion
	}
}

