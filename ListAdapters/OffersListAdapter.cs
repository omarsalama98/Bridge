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
    public class OffersListAdapter : RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick;
        public OffersList offers;
        public OffersListAdapter(OffersList offers)
        {
            this.offers = offers;
        }
        public override int ItemCount
        {
            get { return offers.numoffers; }
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            OfferViewHolder vh = holder as OfferViewHolder;

            vh.Image.SetImageBitmap(offers[position].offerImage);
            vh.Name.Text = offers[position].offerName;
            vh.Rating.Rating = offers[position].offerRating;
            vh.Price.Text = offers[position].offerPrice.ToString();
        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.offers_list_item, parent, false);
            OfferViewHolder vh = new OfferViewHolder(itemView, OnClick);
            return vh;
        }
        private void OnClick(int obj)
        {
            if (ItemClick != null)
                ItemClick(this, obj);
        }
    }
}

