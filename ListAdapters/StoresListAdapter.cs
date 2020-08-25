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
    public class StoresListAdapter : RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick;
        public StoresList storeL;
        public StoresListAdapter(StoresList mstore)
        {
            storeL = mstore;
        }
        public override int ItemCount
        {
            get { return storeL.numStores; }
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            StoreViewHolder vh = holder as StoreViewHolder;
            
            vh.Image.SetImageBitmap(storeL[position].storeImage);
            vh.Name.Text = storeL[position].storeName;
            vh.Rating.Rating = storeL[position].storeRating;
        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.stores_list_item, parent, false);
            StoreViewHolder vh = new StoreViewHolder(itemView, OnClick);
            return vh;
        }
        private void OnClick(int obj)
        {
            if (ItemClick != null)
                ItemClick(this, obj);
        }
    }
}  
    
