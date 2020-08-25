using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using CourierUser.Activities;

namespace CourierUser
{
    public class HomeFragment : Android.Support.V4.App.Fragment
    {
        ImageView store, items, offers;
        LinearLayout myCart;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            View v = inflater.Inflate(Resource.Layout.fragment_home, container, false);
            store = (ImageView) v.FindViewById(Resource.Id.shop_iv);
            items = (ImageView)v.FindViewById(Resource.Id.items_iv);
            offers = (ImageView)v.FindViewById(Resource.Id.offers_iv);
            myCart = (LinearLayout)v.FindViewById(Resource.Id.cartInItems);

            if (!MainActivity.CartItems.isEmpty())
            {
                myCart.Visibility = ViewStates.Visible;
                myCart.Click += MyCart_Click; ;
            }
            store.Click += Store_Click;
            items.Click += Items_Click;
            offers.Click += Offers_Click;

            return v;
        }

        private void MyCart_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(Activity, typeof(MyCartActivity));
            StartActivity(intent);
        }

        private void Offers_Click(object sender, EventArgs e)
        {
            Intent itemsIntent = new Intent(Activity, typeof(OffersActivity));
            StartActivity(itemsIntent);
        }

        private void Items_Click(object sender, EventArgs e)
        {
            Intent itemsIntent = new Intent(Activity, typeof(ItemsActivity));
            StartActivity(itemsIntent);
        }

        private void Store_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(Activity,typeof(StoresActivity));
            StartActivity(intent);
        }
    }
}