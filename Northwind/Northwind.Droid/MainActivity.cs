using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using AIG_Common;

namespace Northwind.Droid
{
    [Activity(Label = "Northwind.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        ListView _productsListView;
        List<Product> _products;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            _productsListView = FindViewById<ListView>(Resource.Id.productsListView);

            fillProducts();
        }

        private async void fillProducts()
        {
            _products = await ServiceClient.GetProductsAsync();
            _productsListView.Adapter = new ProductsAdapter(this, _products);

        }
    }

    
    public class ProductsAdapter : BaseAdapter<Product>
    {
        List<Product> items;
        Activity context;

        public ProductsAdapter(Activity context, List<Product> items)
            : base()
        {
            this.context = context;
            this.items = items;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Product this[int position]
        {
            get { return items[position]; }
        }

        public override int Count
        {
            get { return items.Count; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView; // re-use an existing view, if one is available
            if (view == null) // otherwise create a new one
                view = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);
            view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = items[position].ProductName;
            return view;
        }
    }	
		
}

