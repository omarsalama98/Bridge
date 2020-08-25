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
    class CartItemsListAdapter : RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick;
        public ItemsList items;
        public CartItemsListAdapter(ItemsList mItems)
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
            CartListItemViewHolder vh = holder as CartListItemViewHolder;
            vh.Image.SetImageBitmap(items[position].itemImage);
            vh.Name.Text = items[position].itemName;
            vh.Rating.Rating = items[position].itemRating;
            vh.Category.Text = items[position].itemCategory;
            vh.Price.Text = items[position].itemPrice.ToString();
            vh.quantity.Text += items[position].itemQuantity.ToString();
        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.cart_list_item, parent, false);
            CartListItemViewHolder vh = new CartListItemViewHolder(itemView, OnClick);
            return vh;
        }
        private void OnClick(int obj)
        {
            if (ItemClick != null)
                ItemClick(this, obj);
        }
    }
}
