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
    class CheckoutListAdapter : RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick;
        public ItemsList items;
        public CheckoutListAdapter(ItemsList mItems)
        {
            items = mItems;
        }
        public void setItems(ItemsList items)
        {
            this.items = items;
        }
        public override int ItemCount
        {
            get { return items.numitems; }
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            CheckoutItemViewHolder vh = holder as CheckoutItemViewHolder;
            vh.Name.Text = items[position].itemName;
            vh.Price.Text = items[position].itemPrice.ToString();
            vh.quantity.Text += items[position].itemQuantity.ToString();
            vh.TotPrice.Text = (items[position].itemPrice * items[position].itemQuantity).ToString();
        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.checkout_items_list_item, parent, false);
            CheckoutItemViewHolder vh = new CheckoutItemViewHolder(itemView, OnClick);
            return vh;
        }
        private void OnClick(int obj)
        {
            if (ItemClick != null)
                ItemClick(this, obj);
        }
    }
}
