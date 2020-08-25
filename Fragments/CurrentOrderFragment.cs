using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using DBapplication;

namespace CourierUser.Fragments
{
    public class CurrentOrderFragment : Android.Support.V4.App.Fragment
    {
        TextView storeName, OrderPrice, courierName;
        ImageView storeImage;
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
            View v = inflater.Inflate(Resource.Layout.fragment_current_order, container, false);
            controller = new Controller();
            storeName = v.FindViewById<TextView>(Resource.Id.currOrderStoreNameItemTV);
            OrderPrice = v.FindViewById<TextView>(Resource.Id.currOrderPriceItemTV);
            storeImage = v.FindViewById<ImageView>(Resource.Id.currOrderStoreImageIV);
            courierName = v.FindViewById<TextView>(Resource.Id.currOrderCourierNameItemTV);
            ISharedPreferences pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
            userName = pref.GetString("Username", String.Empty);
            List<string> orderDetails = controller.getCurrentOrderDets(userName);
            courierName.Text += orderDetails[0] +" "+ orderDetails[1];
            storeImage.SetImageBitmap(loadingImages.GetImageBitmapFromUrl(orderDetails[2]));
            storeName.Text = orderDetails[3];
            OrderPrice.Text += orderDetails[4];
            return v;
        }
    }
}