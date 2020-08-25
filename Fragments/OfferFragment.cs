using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using DBapplication;

namespace CourierUser.Fragments
{
    public class OfferFragment : DialogFragment
    {
        RecyclerView mRecycleView;
        RecyclerView.LayoutManager mLayoutManager;
        OfferItemsListAdapter mAdapter;
        ItemsList itemsList;
        Offer offer;
        Controller controller;
        Button AddOfferToCartBtn;
        RatingBar offerRating;
        string userName;
        bool changed = false;
        public OfferFragment(ItemsList items, Offer offer)
        {
            itemsList = items;
            this.offer = offer;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.offer_items, container, false);

            ISharedPreferences pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
            userName = pref.GetString("Username", String.Empty);

            controller = new Controller();
            AddOfferToCartBtn = view.FindViewById<Button>(Resource.Id.AddOfferToCartBtn);
            offerRating = view.FindViewById<RatingBar>(Resource.Id.offerRating);
            offerRating.RatingBarChange += OfferRating_RatingBarChange;
            mRecycleView = view.FindViewById<RecyclerView>(Resource.Id.offerItemsRecyclerView);
            mLayoutManager = new LinearLayoutManager(Activity);
            mRecycleView.SetLayoutManager(mLayoutManager);
            mAdapter = new OfferItemsListAdapter(itemsList);
            mRecycleView.SetAdapter(mAdapter);
            AddOfferToCartBtn.Click += AddOfferToCartBtn_Click;
            return view;

        }

        private void OfferRating_RatingBarChange(object sender, RatingBar.RatingBarChangeEventArgs e)
        {
            changed = true;
        }

        private void AddOfferToCartBtn_Click(object sender, EventArgs e)
        {
            if (changed)
            {
                controller.InsertReview((int)offerRating.Rating, " ");
                controller.InsertReviewOffer(controller.getLatestReviewId(), userName, offer.offerID);
            }
            MainActivity.CartOffers.Add(offer);
            Dialog.Cancel();
        }
    }
}