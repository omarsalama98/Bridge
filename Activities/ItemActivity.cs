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
    [Activity(Label = "ItemActivity")]
    public class ItemActivity : Activity
    {
        ImageView itemImageIV;
        LinearLayout myCart;
        TextView itemNameTV, itemTypeTV, itemPriceTV, itemQuantityTV;
        RelativeLayout addBtn, removeBtn;
        Button AddCartBtn;
        Item item;
        RatingBar itemRating;
        Controller controller;
        int itemID;
        string userName;
        bool changed;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_item);
            itemID = Intent.GetIntExtra("Itemid", 0);
            controller = new Controller();
            item = controller.getItem(itemID);

            ISharedPreferences pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
            userName = pref.GetString("Username", String.Empty);

            AddCartBtn = (Button)FindViewById(Resource.Id.AddToCartBtn);
            itemQuantityTV = (TextView)FindViewById(Resource.Id.itemQuantity);
            itemImageIV = (ImageView)FindViewById(Resource.Id.itemImageView);
            itemNameTV = (TextView)FindViewById(Resource.Id.ItemNameTextView);
            itemTypeTV = (TextView)FindViewById(Resource.Id.ItemTypeTextView);
            itemPriceTV = (TextView)FindViewById(Resource.Id.ItemPriceTextView);
            addBtn = (RelativeLayout)FindViewById(Resource.Id.addIcon);
            removeBtn = (RelativeLayout)FindViewById(Resource.Id.removeIcon);
            myCart = (LinearLayout)FindViewById(Resource.Id.cartInItem);
            itemRating = (RatingBar)FindViewById(Resource.Id.itemRating);
            if (MainActivity.CartItems.numitems != 0)
            {
                myCart.Visibility = ViewStates.Visible;
                myCart.Click += MyCart_Click;
            }
            changed = false;
            itemRating.RatingBarChange += ItemRating_RatingBarChange;
            itemImageIV.SetImageBitmap(item.itemImage);
            itemNameTV.Text = item.itemName;
            itemTypeTV.Text = item.itemType;
            itemPriceTV.Text = item.itemPrice.ToString();

            addBtn.Click += AddBtn_Click;
            removeBtn.Click += RemoveBtn_Click;
            AddCartBtn.Click += AddCartBtn_Click;
            // Create your application here
        }

        private void ItemRating_RatingBarChange(object sender, RatingBar.RatingBarChangeEventArgs e)
        {
            changed = true;
        }

        private void MyCart_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MyCartActivity));
            StartActivity(intent);
        }

        private void RemoveBtn_Click(object sender, EventArgs e)
        {
            if(int.Parse(itemQuantityTV.Text)>1)
                itemQuantityTV.Text = (int.Parse(itemQuantityTV.Text) - 1).ToString();
            else
            {

            }

        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            itemQuantityTV.Text = (int.Parse(itemQuantityTV.Text) + 1).ToString();
        }

        private void AddCartBtn_Click(object sender, EventArgs e)
        {
            item.itemQuantity = int.Parse(itemQuantityTV.Text);
            MainActivity.CartItems.Add(item);
            if (changed)
            {
                controller.InsertReview((int)itemRating.Rating, " ");
                controller.InsertReviewItem(controller.getLatestReviewId(), userName, itemID);
            }
            base.OnBackPressed();
        }
    }
}