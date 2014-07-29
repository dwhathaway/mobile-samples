using System;
using System.Drawing;
using System.Collections.Generic;
using System.Threading.Tasks;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using Groceries.Core.Models;
using Groceries.Core.Services;

using MBProgressHUD;

namespace Groceries_iOS
{
	public partial class DetailViewController : UIViewController
	{
		GroceryItem detailItem;
		MTMBProgressHUD _hud;

		public DetailViewController (IntPtr handle) : base (handle)
		{

		}

		public void SetDetailItem (GroceryItem newDetailItem)
		{
			if (detailItem != newDetailItem) {
				detailItem = newDetailItem;

				// Update the view
				ConfigureView ();
			}
		}

		void ConfigureView ()
		{
			// Update the user interface for the detail item
			if (IsViewLoaded && detailItem != null) {

				txtName.Text = detailItem.Title;
				txtDescription.Text = detailItem.Description;
				txtQuantity.Text = detailItem.Quantity.ToString();
			}
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
			ConfigureView ();

			txtDescription.EditingDidEnd += HandleEditingDidEnd;
			txtDescription.Delegate = new CatchEnterDelegate();

			txtQuantity.EditingDidEnd += HandleEditingDidEnd;
			txtQuantity.Delegate = new CatchEnterDelegate();

			txtName.EditingDidEnd += HandleEditingDidEnd;
			txtName.Delegate = new CatchEnterDelegate();

			_hud = new MTMBProgressHUD(View)
			{
				LabelText = "Saving...",
				RemoveFromSuperViewOnHide = true
			};

			View.AddSubview (_hud);

			btnSave.TouchUpInside += (object sender, EventArgs e) => {
				saveRecord();
			};
		}

		private async void saveRecord()
		{

			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;

			//Assign user set values back to object, and persist back to cloud
			detailItem.Title = txtName.Text;
			detailItem.Description = txtDescription.Text;
			detailItem.Quantity = int.Parse(txtQuantity.Text);

			_hud.Show (animated: true);

			if(!string.IsNullOrEmpty(detailItem.id)) {
				await GroceryService.UpdateGroceryItemAsync(detailItem);
			}
			else {
				await GroceryService.CreateGroceryItemAsync (detailItem);
			}

			_hud.Hide (animated: true, delay: 5);

			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;

			NavigationController.PopViewControllerAnimated(true);
		}

		void HandleEditingDidEnd(object sender, EventArgs e)
		{
			//do what you need to do with the value of the textfield here
		}
	}

	public class CatchEnterDelegate : UITextFieldDelegate
	{
		public override bool ShouldReturn(UITextField textField)
		{
			textField.ResignFirstResponder();
			return true;
		}
	}
}

