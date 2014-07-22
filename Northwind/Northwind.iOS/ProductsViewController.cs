using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;
using System.Threading.Tasks;

using AIG_Common;
using System.Collections.Generic;

namespace Northwind.iOS
{
	partial class ProductsViewController : UITableViewController
    {
        List<Product> _products = new List<Product>();
        ProductsDataSource _productsDataSource;
        
		public ProductsViewController (IntPtr handle) : base (handle)
		{
            this.Title = "Products";
        }

        void AddNewItem(object sender, EventArgs args)
        {
            this.PerformSegue("showDetail", this);
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            refreshData();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.
            NavigationItem.LeftBarButtonItem = EditButtonItem;

            var addButton = new UIBarButtonItem(UIBarButtonSystemItem.Add, AddNewItem);
            NavigationItem.RightBarButtonItem = addButton;

            refreshData();
        }

        private async void refreshData()
        {

            UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
            
            _products = await ServiceClient.GetProductsAsync();
                        
            TableView.Source = _productsDataSource = new ProductsDataSource(this, _products);
            TableView.ReloadData();            

            UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;

        }

        class ProductsDataSource : UITableViewSource
        {
            static readonly NSString CellIdentifier = new NSString("DataSourceCell");

            List<Product> objects = new List<Product>();

            ProductsViewController controller;

            public ProductsDataSource(ProductsViewController controller, List<Product> groceries)
            {
                this.controller = controller;
                this.objects = groceries;
            }

            public IList<Product> Objects
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

                cell.TextLabel.Text = objects[indexPath.Row].ProductName.ToString() + " (Qty: " + objects[indexPath.Row].UnitsInStock.ToString() + ")";

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
                    controller.TableView.BeginUpdates();

                    var groceryItem = objects[indexPath.Row];
                    objects.RemoveAt(indexPath.Row);
                    //GroceryService.DeleteGroceryItem(groceryItem);
                    controller.TableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);

                    controller.TableView.EndUpdates();
                }
                else if (editingStyle == UITableViewCellEditingStyle.Insert)
                {
                    // Create a new instance of the appropriate class, insert it into the array, and add a new row to the table view.
                }
            }
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "showDetail")
            {
                var indexPath = TableView.IndexPathForSelectedRow;
                var item = indexPath == null ? new Product() : _productsDataSource.Objects[indexPath.Row];

                ///((DetailViewController)segue.DestinationViewController).SetDetailItem(item);
            }
        }
	}
}
