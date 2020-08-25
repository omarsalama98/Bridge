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
using CourierUser.Activities;
using DBapplication;

namespace CourierUser
{
    public class ProfileFragment : Android.Support.V4.App.Fragment
    {
        Controller controllerobj = new Controller();
        Button editBtn;
        TextView user_nameTV,EmailTV,PhoneNumTV;
        string userName, Fname = "", Mname = "", Lname = "", Email = "", PhoneNum = "", CreditCardNum = "", CreditCardCVC = "";
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
          
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.fragment_profile, container, false);
            user_nameTV = (TextView) view.FindViewById(Resource.Id.User_Name_TextView);
            EmailTV = (TextView)view.FindViewById(Resource.Id.Email_TextView);
            PhoneNumTV = (TextView)view.FindViewById(Resource.Id.Phone_Num_TextView);
            editBtn = (Button)view.FindViewById(Resource.Id.EditProfileBtn);
            editBtn.Click += EditBtn_Click;
            ISharedPreferences pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
            userName = pref.GetString("Username", String.Empty);
            List<string> userList = controllerobj.RetreiveUser(userName);
            Fname = userList[User.columnFName];
            Mname = userList[User.columnMName];
            Lname = userList[User.columnLName];
            Email = userList[User.columnEmail];
            PhoneNum = userList[User.columnPhoneNum];
            Fname += " " + Lname;
            user_nameTV.Text = Fname;
            EmailTV.Text = Email;
            PhoneNumTV.Text = PhoneNum;
            return view;
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(Activity, typeof(EditProfileActivity));
            StartActivity(intent);
        }
    }
}