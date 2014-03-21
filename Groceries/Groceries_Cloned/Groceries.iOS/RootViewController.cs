using System;
using System.Drawing;
using System.Collections.Generic;
using System.Threading.Tasks;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

using Groceries.Core.Models;
using Groceries.Core.Services;

namespace Groceries.iOS
{
    public partial class RootViewController : UITableViewController
    {
        DataSource dataSource;

        List<GroceryItem> _groceries = new List<GroceryItem>();

        public RootViewController(IntPtr handle)
            : base(handle)
        {
            Title = NSBundle.MainBundle.LocalizedString("Groceries", "Groceries");

            // Custom initialization
        }

        void AddNewItem(object sender, EventArgs args)
        {
            this.PerformSegue("showDetail", this);

            //this.Storyboard
            //			dataSource.Objects.Add (new GroceryItem() { Title = "New Item" });
            //
            //			using (var indexPath = NSIndexPath.FromRowSection (0, 0))
            //				TableView.InsertRows (new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Automatic);
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			refreshData ();
		}
			
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.
            NavigationItem.LeftBarButtonItem = EditButtonItem;

            var addButton = new UIBarButtonItem(UIBarButtonSystemItem.Add, AddNewItem);
            NavigationItem.RightBarButtonItem = addButton;

			refreshData ();
        }

		private void refreshData() {

			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;

			//Use the Task Parallel Library (TPL) to start a new thread to retreive a list of groceries from the server
			Task.Factory.StartNew(() => {
				_groceries = GroceryService.GetGroceries ();
			}).ContinueWith((prevTask) => {
				InvokeOnMainThread(() => {
					//Check the response to see if there were any errors on the background task
					if(prevTask.Exception != null)
					{
						//Toast.MakeText (this, "Error retrieving results from server...", ToastLength.Long).Show ();
					}
					else
					{
						TableView.Source = dataSource = new DataSource(this, _groceries);
						TableView.ReloadData();
						UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
					}
				});
			}, TaskScheduler.FromCurrentSynchronizationContext());
		}

        class DataSource : UITableViewSource
        {
            static readonly NSString CellIdentifier = new NSString("DataSourceCell");

            List<GroceryItem> objects = new List<GroceryItem>();

            RootViewController controller;

            public DataSource(RootViewController controller, List<GroceryItem> groceries)
            {
                this.controller = controller;
                this.objects = groceries;
            }

            public IList<GroceryItem> Objects
            {
                get { return objects; }
            }

            // Customize the number of sections in the table view.
            public override int NumberOfSections(UITableView tableView)
            {
                return 1;
            }

            public override int RowsInSection(UITableView tableview, int section)
            {
                return objects.Count;
            }

            // Customize the appearance of table view cells.
            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                var cell = (UITableViewCell)tableView.DequeueReusableCell(CellIdentifier, indexPath);

				cell.TextLabel.Text = objects[indexPath.Row].Title.ToString() + " (Qty: " + objects[indexPath.Row].Quantity.ToString() + ")";

                return cell;
            }

            public override bool CanEditRow(UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
            {
                // Return false if you do not want the specified item to be editable.
                return true;
            }

            public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
            {
                if (editingStyle == UITableViewCellEditingStyle.Delete)
                {
                    // Delete the row from the data source.
                    objects.RemoveAt(indexPath.Row);
                    controller.TableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
                }
                else if (editingStyle == UITableViewCellEditingStyle.Insert)
                {
                    // Create a new instance of the appropriate class, insert it into the array, and add a new row to the table view.
                }
            }
            /*
            // Override to support rearranging the table view.
            public override void MoveRow (UITableView tableView, NSIndexPath sourceIndexPath, NSIndexPath destinationIndexPath)
            {
            }
            */

            /*
            // Override to support conditional rearranging of the table view.
            public override bool CanMoveRow (UITableView tableView, NSIndexPath indexPath)
            {
                // Return false if you do not want the item to be re-orderable.
                return true;
            }
            */
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "showDetail")
            {
                var indexPath = TableView.IndexPathForSelectedRow;
                var item = indexPath == null ? new GroceryItem() : dataSource.Objects[indexPath.Row];

                ((DetailViewController)segue.DestinationViewController).SetDetailItem(item);
            }
        }
    }
}