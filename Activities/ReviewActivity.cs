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

namespace CourierUser.Activities
{
    [Activity(Label = "ReviewActivity")]
    public class ReviewActivity : Activity
    {
        EditText storeReviewDets, courierReviewDets;
        string storeRevDets, courierRevDets;
        RatingBar storeRating, orderRating, courierRating;
        Button submitBtn;
        Controller controller;
        int OrderID;
        string userName;
        List<int> orderDetails;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_review);
            storeReviewDets = (EditText)FindViewById(Resource.Id.storeReviewDetails);
            courierReviewDets = (EditText)FindViewById(Resource.Id.courierReviewDetails);
            courierRating = (RatingBar)FindViewById(Resource.Id.courierReviewRating);
            storeRating = (RatingBar)FindViewById(Resource.Id.storeReviewRating);
            orderRating = (RatingBar)FindViewById(Resource.Id.orderReviewRating);
            submitBtn = (Button)FindViewById(Resource.Id.submitReviewBtn);

            ISharedPreferences pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
            userName = pref.GetString("Username", String.Empty);

            OrderID = Intent.GetIntExtra("OrderID", 0);
            controller = new Controller();                                                               //StoreID = 0 
            orderDetails = controller.getOrderDets(OrderID);                                             //CourierID = 1
            
            submitBtn.Click += SubmitBtn_Click;
            // Create your application here
        }

        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            courierRevDets = courierReviewDets.Text;
            storeRevDets = storeReviewDets.Text;
            controller.InsertReview((int)storeRating.Rating,storeRevDets.Length==0?" ":storeRevDets);
            controller.InsertReviewStore(controller.getLatestReviewId(), userName, orderDetails[0]);

            controller.InsertReview((int)courierRating.Rating, courierRevDets.Length == 0 ? " " : courierRevDets);
            controller.InsertReviewCourier(controller.getLatestReviewId(), userName, orderDetails[1]);
            controller.UpdateOrderRating(OrderID, (int)orderRating.Rating);
            Toast.MakeText(this, "Review Added Successfully", ToastLength.Short).Show();
            base.OnBackPressed();
        }
    }
}