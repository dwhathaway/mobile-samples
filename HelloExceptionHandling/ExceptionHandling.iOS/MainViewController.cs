
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace ExceptionHandling.iOS
{
	public partial class MainViewController : UIViewController
	{
		public MainViewController () : base ("MainViewController", null)
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

			//This TapGestureRecognizer will 
			var tapRecognizer = new UITapGestureRecognizer (this, new MonoTouch.ObjCRuntime.Selector ("NonExistantSelector"));
			View.AddGestureRecognizer(tapRecognizer);

			// Perform any additional setup after loading the view, typically from a nib.
			btnClickMe.TouchUpInside += (sender, e) => {
				throw new Exception("Broken");
			};
		}
	}
}

