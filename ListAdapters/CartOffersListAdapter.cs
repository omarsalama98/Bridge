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

namespace CourierUser
{
    class CartOffersListAdapter : RecyclerView.Adapter
    {
        public event EventHandler<int> OfferClick;
        public OffersList Offers;
        public CartOffersListAdapter(OffersList mOffers)
        {
            Offers = mOffers;
        }
        public void setOffers(OffersList Offers)
        {
            this.Offers = Offers;
        }
        public override int ItemCount
        {
            get { return Offers.numoffers; }
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            OfferViewHolder vh = holder as OfferViewHolder;
            vh.Image.SetImageBitmap(Offers[position].offerImage);
            vh.Name.Text = Offers[position].offerName;
            vh.Rating.Rating = Offers[position].offerRating;
            vh.Price.Text = Offers[position].offerPrice.ToString();
        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View OfferView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.offers_list_item, parent, false);
            OfferViewHolder vh = new OfferViewHolder(OfferView, OnClick);
            return vh;
        }
        private void OnClick(int obj)
        {
            if (OfferClick != null)
                OfferClick(this, obj);
        }
    }
}
