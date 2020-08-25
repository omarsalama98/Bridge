using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using CourierUser.Activities;
using DBapplication;

namespace CourierUser
{
    public class PaymentFragment : Android.Support.V4.App.Fragment
    {
        TextView CreditCardFirstDigits, noRegisteredCards;
        LinearLayout CreditCardView;
        FloatingActionButton addCreditCard;
        String userName;
        Controller controller;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            controller = new Controller();
            View v = inflater.Inflate(Resource.Layout.fragment_payment, container, false);
            CreditCardFirstDigits = v.FindViewById<TextView>(Resource.Id.userCreditCardNumFirstDigitsTV);
            CreditCardView = v.FindViewById<LinearLayout>(Resource.Id.UserCreditCardView);
            addCreditCard = v.FindViewById<FloatingActionButton>(Resource.Id.addCreditCardFB);
            noRegisteredCards = v.FindViewById<TextView>(Resource.Id.noRegisteredCardsTV);
            addCreditCard.Visibility = ViewStates.Invisible;
            noRegisteredCards.Visibility = ViewStates.Invisible;
            CreditCardView.Visibility = ViewStates.Invisible;

            ISharedPreferences pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
            userName = pref.GetString("Username", String.Empty);
            List<string> userList = controller.RetreiveUser(userName);
            try
            {
                CreditCardFirstDigits.Text = userList[User.columnCreditCardNum].Substring(0, 3);
                CreditCardView.Visibility = ViewStates.Visible;
            }
            catch(Exception ex)
            {
                addCreditCard.Visibility = ViewStates.Visible;
                addCreditCard.Click += AddCreditCard_Click;
                noRegisteredCards.Visibility = ViewStates.Visible;
                CreditCardView.Visibility = ViewStates.Invisible;
            }
            return v;

        }

        private void AddCreditCard_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(Activity, typeof(AddCreditCardActivity));
            intent.PutExtra("UserName", userName);
            StartActivity(intent);
        }
    }
}