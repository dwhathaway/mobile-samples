// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

namespace Groceries_iOS
{
	[Register ("DetailViewController")]
	partial class DetailViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnSave { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtDescription { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtName { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtQuantity { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnSave != null) {
				btnSave.Dispose ();
				btnSave = null;
			}
			if (txtDescription != null) {
				txtDescription.Dispose ();
				txtDescription = null;
			}
			if (txtName != null) {
				txtName.Dispose ();
				txtName = null;
			}
			if (txtQuantity != null) {
				txtQuantity.Dispose ();
				txtQuantity = null;
			}
		}
	}
}
