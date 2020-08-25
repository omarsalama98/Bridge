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
using CourierUser.Fragments;

namespace CourierUser.Activities
{
    [Activity(Label = "OffersActivity")]
    public class OffersActivity : Activity
    {
        OffersList offersList;
        RecyclerView mRecycleView;
        RecyclerView.LayoutManager mLayoutManager;
        OffersListAdapter mAdapter;
        LinearLayout myCart;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            offersList = new OffersList(0);
            // Set our view from the "main" layout resource  
            SetContentView(Resource.Layout.activity_offers);
            myCart = (LinearLayout)FindViewById(Resource.Id.cartInOffers);

            if (!MainActivity.CartItems.isEmpty() || !MainActivity.CartOffers.isEmpty())
            {
                myCart.Visibility = ViewStates.Visible;
                myCart.Click += MyCart_Click; ;
            }

            mRecycleView = FindViewById<RecyclerView>(Resource.Id.offersRecyclerView);
            mLayoutManager = new LinearLayoutManager(this);
            mRecycleView.SetLayoutManager(mLayoutManager);
            mAdapter = new OffersListAdapter(offersList);
            mAdapter.ItemClick += MAdapter_ItemClick;
            mRecycleView.SetAdapter(mAdapter);
        }

        private void MyCart_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MyCartActivity));
            StartActivity(intent);
        }

        private void MAdapter_ItemClick(object sender, int e)
        {
            ItemsList itemsList = new ItemsList();
            itemsList.AddItems(offersList[e].items);
            FragmentTransaction transcation = FragmentManager.BeginTransaction();
            OfferFragment offerItems = new OfferFragment(itemsList,offersList[e]);
            offerItems.Show(transcation, "Dialog Fragment");
            if (!MainActivity.CartItems.isEmpty() || !MainActivity.CartOffers.isEmpty())
            {
                myCart.Visibility = ViewStates.Visible;
                myCart.Click += MyCart_Click; ;
            }
        }
    }
}