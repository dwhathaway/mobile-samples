using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

using Phoneword.Common;

namespace Phoneword_iOS
{
	[Register ("RecentCallsViewControllerController")]
	public partial class RecentCallsViewControllerController : UITableViewController
	{
		DataSource dataSource;

		public RecentCallsViewControllerController (IntPtr handle) : base (handle)
		{
			// Custom initialization
		}

		void AddNewItem (object sender, EventArgs args)
		{
			//dataSource.Objects.Insert (0, DateTime.Now.ToString());

			using (var indexPath = NSIndexPath.FromRowSection (0, 0))
				TableView.InsertRows (new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Automatic);
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


			string folder = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);

			Database db = new Database(System.IO.Path.Combine (folder, "Phoneword.db"));

			List<CallHistoryItem> numbers = db.QueryCallHistory ().Cast<CallHistoryItem>().ToList(); 

			TableView.Source = dataSource = new DataSource (this, numbers);
		}

		class DataSource : UITableViewSource
		{
			static readonly NSString CellIdentifier = new NSString ("Cell");
			readonly List<CallHistoryItem> objects = new List<CallHistoryItem> ();
			readonly RecentCallsViewControllerController controller;

			public DataSource (RecentCallsViewControllerController controller, List<CallHistoryItem> callhistory)
			{
				this.controller = controller;
				this.objects = callhistory;
			}

			public IList<CallHistoryItem> Objects {
				get { return objects; }
			}
			// Customize the number of sections in the table view.
			public override int NumberOfSections (UITableView tableView)
			{
				return 1;
			}

			public override int RowsInSection (UITableView tableview, int section)
			{
				return objects.Count;
			}

			// Customize the appearance of table view cells.
			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				var cell = (UITableViewCell)tableView.DequeueReusableCell (CellIdentifier, indexPath);

				cell.TextLabel.Text = objects [indexPath.Row].Number.ToString ();

				return cell;
			}

			public override bool CanEditRow (UITableView tableView, NSIndexPath indexPath)
			{
				// Return false if you do not want the specified item to be editable.
				return true;
			}

			public override void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
			{
				if (editingStyle == UITableViewCellEditingStyle.Delete) {
					// Delete the row from the data source.
					objects.RemoveAt (indexPath.Row);
					controller.TableView.DeleteRows (new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
				} else if (editingStyle == UITableViewCellEditingStyle.Insert) {
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
	}
}

