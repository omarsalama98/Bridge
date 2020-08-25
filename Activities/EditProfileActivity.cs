using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DBapplication;

namespace CourierUser.Activities
{
    [Activity(Label = "EditProfileActivity")]
    public class EditProfileActivity : Activity
    {
        EditText fName, mName, lName, password, email;
        Button confirmBtn;
        Controller controller;
        string userName;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_edit_profile);
            controller = new Controller();
            ISharedPreferences pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
            userName = pref.GetString("Username", String.Empty);
            fName = (EditText)FindViewById(Resource.Id.FirstNameEditingProfileET);
            mName = (EditText)FindViewById(Resource.Id.MiddleNameEditingProfileET);
            lName = (EditText)FindViewById(Resource.Id.LastNameEditingProfileET);
            password = (EditText)FindViewById(Resource.Id.PasswordEditingProfileET);
            email = (EditText)FindViewById(Resource.Id.EmailEditingProfileET);
            confirmBtn = (Button)FindViewById(Resource.Id.confirmEditingProfileBtn);
            confirmBtn.Click += ConfirmBtn_Click;
            // Create your application here
        }

        private void ConfirmBtn_Click(object sender, EventArgs e)
        {
            controller.UpdateClient(userName, password.Text, fName.Text, mName.Text, lName.Text, email.Text);
            Intent intent = new Intent(this,typeof(MainActivity));
            StartActivity(intent);
        }
    }
}