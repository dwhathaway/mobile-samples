// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace Phoneword_iOS
{
	[Register ("Phoneword_iOSViewController")]
	partial class Phoneword_iOSViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnCall { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnTranslate { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtInput { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnTranslate != null) {
				btnTranslate.Dispose ();
				btnTranslate = null;
			}

			if (btnCall != null) {
				btnCall.Dispose ();
				btnCall = null;
			}

			if (txtInput != null) {
				txtInput.Dispose ();
				txtInput = null;
			}
		}
	}
}
