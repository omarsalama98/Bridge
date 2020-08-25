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
using CourierUser.Activities;
using DBapplication;

namespace CourierUser.Fragments
{
    public class MyOrdersFragment : Android.Support.V4.App.Fragment
    {
        RecyclerView mRecycleView;
        RecyclerView.LayoutManager mLayoutManager;
        OrderListAdapter mAdapter;
        OrdersList ordersList;
        Controller controller;
        string userName;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View v = inflater.Inflate(Resource.Layout.fragment_my_orders, container, false);
            ISharedPreferences pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
            userName = pref.GetString("Username", String.Empty);

            controller = new Controller();
            mRecycleView = v.FindViewById<RecyclerView>(Resource.Id.ordersListRecyclerView);
            mLayoutManager = new LinearLayoutManager(Activity);
            mRecycleView.SetLayoutManager(mLayoutManager);
            ordersList = new OrdersList(userName);
            mAdapter = new OrderListAdapter(ordersList);
            mAdapter.ItemClick += MAdapter_ItemClick;
            mRecycleView.SetAdapter(mAdapter);

            return v;

        }

        private void MAdapter_ItemClick(object sender, int e)
        {
            Intent intent = new Intent(Activity, typeof(ReviewActivity));
            intent.PutExtra("OrderID",ordersList[e].OrderID);
            StartActivity(intent);
        }
    }
}