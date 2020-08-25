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

namespace CourierUser.Activities
{
    [Activity(Label = "MyCartActivity")]
    public class MyCartActivity : Activity
    {
        ItemsList itemsList;
        RecyclerView mRecycleView;
        RecyclerView.LayoutManager mLayoutManager;
        CartItemsListAdapter mAdapter;
        OffersList offersList;
        RecyclerView oRecycleView;
        RecyclerView.LayoutManager oLayoutManager;
        CartOffersListAdapter oAdapter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_my_cart);
            itemsList = MainActivity.CartItems;
            mRecycleView = FindViewById<RecyclerView>(Resource.Id.myCartItemsRecyclerView);
            mLayoutManager = new LinearLayoutManager(this);
            mRecycleView.SetLayoutManager(mLayoutManager);
            mAdapter = new CartItemsListAdapter(itemsList);
            mAdapter.ItemClick += MAdapter_ItemClick;
            mRecycleView.SetAdapter(mAdapter);
            offersList = MainActivity.CartOffers;

            oRecycleView = FindViewById<RecyclerView>(Resource.Id.myCartOffersRecyclerView);
            oLayoutManager = new LinearLayoutManager(this);
            oRecycleView.SetLayoutManager(oLayoutManager);
            oAdapter = new CartOffersListAdapter(offersList);
            oAdapter.OfferClick += OAdapter_OfferClick;  
            oRecycleView.SetAdapter(oAdapter);
            TextView proceedCheck = (TextView)FindViewById(Resource.Id.proceedCheckoutTV);
            proceedCheck.Click += ProceedCheck_Click;
            // Create your application here
        }

        private void OAdapter_OfferClick(object sender, int e)
        {
            new Android.Support.V7.App.AlertDialog.Builder(this)
                  .SetIcon(Resource.Drawable.ic_alert)
                  .SetTitle("Removing Offer")
                  .SetMessage("Are you sure you want to remove this offer?")
                  .SetPositiveButton("Yes", (c, ev) =>
                  {
                      MainActivity.CartOffers.Remove(offersList[e]);
                      offersList = MainActivity.CartOffers;
                      oAdapter = new CartOffersListAdapter(offersList);
                      oRecycleView.SetAdapter(oAdapter);
                  })
                   .SetNegativeButton("No", (c, ev) => { })
                   .Show();
        }

        private void ProceedCheck_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this,typeof(CheckoutActivity));
            StartActivity(intent);
        }
        public override void OnBackPressed()
        {
            Intent intent = new Intent(this, typeof(ItemsActivity));
            StartActivity(intent);
        }

        private void MAdapter_ItemClick(object sender, int e)
        {
            //Should add if Client wants to remove an item
            new Android.Support.V7.App.AlertDialog.Builder(this)
                  .SetIcon(Resource.Drawable.ic_alert)
                  .SetTitle("Removing Item")
                  .SetMessage("Are you sure you want to remove this item?")
                  .SetPositiveButton("Yes", (c, ev) =>
                  {
                      MainActivity.CartItems.Remove(itemsList[e]);
                      itemsList = MainActivity.CartItems;
                      mAdapter = new CartItemsListAdapter(itemsList);
                      mRecycleView.SetAdapter(mAdapter);
                  })
                   .SetNegativeButton("No", (c, ev) => { })
                   .Show();
        }
    }
}