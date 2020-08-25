using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using CourierUser.Activities;

namespace CourierUser
{
    [Activity(Label = "ItemsActivity")]
    public class ItemsActivity : Activity
    {
        ItemsList itemsList;
        LinearLayout myCart;
        RecyclerView mRecycleView;
        RecyclerView.LayoutManager mLayoutManager;
        ItemsListAdapter mAdapter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (Intent.HasExtra("Storeid"))
            {
                int storeID = Intent.GetIntExtra("Storeid",0);        //To Get Items Of a Specific Store
                itemsList = new ItemsList(storeID);
            }
            else
            {
                itemsList = new ItemsList(0);                         //To Get All Items
            }
            
            // Set our view from the "main" layout resource  
            SetContentView(Resource.Layout.activity_items);
            myCart = (LinearLayout)FindViewById(Resource.Id.cartInItems);

            if (MainActivity.CartItems.numitems!=0 || MainActivity.CartOffers.numoffers!=0)
            {
                myCart.Visibility = ViewStates.Visible;
                myCart.Click += MyCart_Click;
            }


            mRecycleView = FindViewById<RecyclerView>(Resource.Id.itemsRecyclerView);
            mLayoutManager = new LinearLayoutManager(this);
            mRecycleView.SetLayoutManager(mLayoutManager);
            mAdapter = new ItemsListAdapter(itemsList);
            mAdapter.ItemClick += MAdapter_ItemClick;
            mRecycleView.SetAdapter(mAdapter);
        }

        private void MyCart_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this,typeof(MyCartActivity));
            StartActivity(intent);
        }

        private void MAdapter_ItemClick(object sender, int e)
        {
            Intent itemIntent = new Intent(this, typeof(ItemActivity));
            itemIntent.PutExtra("Itemid", itemsList[e].itemID);
            StartActivity(itemIntent);
        }
        public override void OnBackPressed()
        {
            Intent home = new Intent(this, typeof(MainActivity));
            StartActivity(home);
        }
    }

}