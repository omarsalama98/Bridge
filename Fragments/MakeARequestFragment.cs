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
    public class MakeARequestFragment : Android.Support.V4.App.Fragment
    {
        Controller controller;
        EditText details;
        TextView errorTv;
        Button submit;
        string requestDetails,userName;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View v = inflater.Inflate(Resource.Layout.fragment_make_a_request, container, false);
            controller = new Controller();
            ISharedPreferences pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
            userName = pref.GetString("Username", String.Empty);

            details = v.FindViewById<EditText>(Resource.Id.requestDetailsEditText);
            submit = v.FindViewById<Button>(Resource.Id.SubmitRequestBtn);
            errorTv = v.FindViewById<TextView>(Resource.Id.nodetailsErrorTextView);
            submit.Click += Submit_Click;
            return v;
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            requestDetails = details.Text;
            if (requestDetails.Length == 0)
            {
                errorTv.Visibility = ViewStates.Visible;
                return;
            }
            else
            {
                errorTv.Visibility = ViewStates.Invisible;
                controller.InsertRequest(userName, requestDetails.Substring(0, requestDetails.Length<300?requestDetails.Length:299));
                Toast.MakeText(Activity, "Request Submitted Successfully", ToastLength.Short).Show();
                Intent intent = new Intent(Activity, typeof(MainActivity));
                StartActivity(intent);
            }
        }
    }
}