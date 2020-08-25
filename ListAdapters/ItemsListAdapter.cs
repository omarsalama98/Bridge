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
    class ItemsListAdapter : RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick;
        public ItemsList items;
        public ItemsListAdapter(ItemsList mItems)
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
            ItemViewHolder vh = holder as ItemViewHolder;
            vh.Image.SetImageBitmap(items[position].itemImage);
            vh.Name.Text = items[position].itemName;
            vh.Rating.Rating = items[position].itemRating;
            vh.Category.Text = items[position].itemCategory;
            vh.Price.Text = items[position].itemPrice.ToString();
            if(items[position].itemStoreCount <= 0)
            {
                vh.outOfStock.Visibility = ViewStates.Visible;
            }
            else
            {
                vh.outOfStock.Visibility = ViewStates.Invisible;
            }
        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.items_list_item, parent, false);
            ItemViewHolder vh = new ItemViewHolder(itemView, OnClick);
            return vh;
        }
        private void OnClick(int obj)
        {
            if (ItemClick != null)
                ItemClick(this, obj);
        }
    }
}
