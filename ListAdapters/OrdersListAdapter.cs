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
using DBapplication;

namespace CourierUser
{
    class OrderListAdapter : RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick;
        private OrdersList orders;
        public OrderListAdapter(OrdersList orders)
        {
            this.orders = orders;
        }
        public void setOrders(OrdersList orders)
        {
            this.orders = orders;
        }
        public override int ItemCount
        {
            get { return orders.numOrders; }
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            OrderItemViewHolder vh = holder as OrderItemViewHolder;
            Controller controller = new Controller();
            Store store = controller.getStoreById(orders[position].storeID);
            vh.Name.Text = store.storeName;
            vh.Price.Text = "Price: " + orders[position].OrderPrice.ToString();
            vh.TimeStamp.Text = orders[position].OrderTimeStamp.ToString();
            vh.Image.SetImageBitmap(store.storeImage);
        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.orders_list_item, parent, false);
            OrderItemViewHolder vh = new OrderItemViewHolder(itemView, OnClick);
            return vh;
        }
        private void OnClick(int obj)
        {
            if (ItemClick != null)
                ItemClick(this, obj);
        }
    }
}
