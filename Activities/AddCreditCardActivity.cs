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
    [Activity(Label = "AddCreditCardActivity")]
    public class AddCreditCardActivity : Activity
    {
        EditText creditCardNum, creditCardCVC;
        Button confirmBtn;
        Controller controller;
        string userName;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_add_credit_card);
            controller = new Controller();
            userName = Intent.GetStringExtra("UserName");
            creditCardNum = FindViewById<EditText>(Resource.Id.EnteredCreditCardNum);
            creditCardCVC = FindViewById<EditText>(Resource.Id.EnteredCreditCardCVC);
            confirmBtn = FindViewById<Button>(Resource.Id.confirmAddCreditCardBtn);
            confirmBtn.Click += ConfirmBtn_Click;
        }

        private void ConfirmBtn_Click(object sender, EventArgs e)
        {
            if(creditCardNum.Text.Length!=16)
            {
                Toast.MakeText(this, "Enter a Valid Credit Card Num", ToastLength.Short).Show(); ;
                return;
            }
            if (creditCardCVC.Text.Length != 3)
            {
                Toast.MakeText(this, "Enter a Valid Credit Card CVC", ToastLength.Short).Show(); ;
                return;
            }
            controller.InsertCreditCard(userName,creditCardNum.Text, int.Parse(creditCardCVC.Text));
            Toast.MakeText(this, "Added Successfully", ToastLength.Short).Show();
            Intent intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }
    }
}