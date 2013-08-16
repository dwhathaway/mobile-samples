// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace Groceries.iOS
{
	[Register ("DetailViewController")]
	partial class DetailViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnCallService { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnNewButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnSave { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel detailDescriptionLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIToolbar toolbar { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtDescription { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtName { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtNewField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField txtQuantity { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel txtServiceResult { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnCallService != null) {
				btnCallService.Dispose ();
				btnCallService = null;
			}

			if (btnSave != null) {
				btnSave.Dispose ();
				btnSave = null;
			}

			if (detailDescriptionLabel != null) {
				detailDescriptionLabel.Dispose ();
				detailDescriptionLabel = null;
			}

			if (toolbar != null) {
				toolbar.Dispose ();
				toolbar = null;
			}

			if (txtDescription != null) {
				txtDescription.Dispose ();
				txtDescription = null;
			}

			if (txtName != null) {
				txtName.Dispose ();
				txtName = null;
			}

			if (txtNewField != null) {
				txtNewField.Dispose ();
				txtNewField = null;
			}

			if (txtQuantity != null) {
				txtQuantity.Dispose ();
				txtQuantity = null;
			}

			if (txtServiceResult != null) {
				txtServiceResult.Dispose ();
				txtServiceResult = null;
			}

			if (btnNewButton != null) {
				btnNewButton.Dispose ();
				btnNewButton = null;
			}
		}
	}
}
