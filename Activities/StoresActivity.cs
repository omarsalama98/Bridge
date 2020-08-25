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
using CourierUser.Activities;
using DBapplication;

namespace CourierUser
{

    [Activity(Label = "StoresActivity")]
    public class StoresActivity : Activity
    {
        RecyclerView mRecycleView;
        LinearLayout myCart;
        RecyclerView.LayoutManager mLayoutManager;
        StoresList storesList;
        StoresListAdapter mAdapter;
        string userName;
        Controller controller;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //ISharedPreferences pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
            //userName = pref.GetString("Username", String.Empty);
            //List<string> userList = controller.RetreiveUser(userName);
            //storesList = new StoresList(userList[User.columnAddress]);
            // Set our view from the "main" layout resource  
            storesList = new StoresList();
            SetContentView(Resource.Layout.activity_stores);
            myCart = (LinearLayout)FindViewById(Resource.Id.cartInStores);

            if (!MainActivity.CartItems.isEmpty() || !MainActivity.CartOffers.isEmpty())
            {
                myCart.Visibility = ViewStates.Visible;
                myCart.Click += MyCart_Click; ;
            }

            mRecycleView = FindViewById<RecyclerView>(Resource.Id.storesRecyclerView);
            mLayoutManager = new LinearLayoutManager(this);
            mRecycleView.SetLayoutManager(mLayoutManager);
            mAdapter = new StoresListAdapter(storesList);
            mAdapter.ItemClick += MAdapter_ItemClick;
            mRecycleView.SetAdapter(mAdapter);
        }

        private void MyCart_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MyCartActivity));
            StartActivity(intent);
        }

        private void MAdapter_ItemClick(object sender, int e)
        {
            Intent itemsIntent = new Intent(this, typeof(ItemsActivity));
            itemsIntent.PutExtra("Storeid",storesList[e].storeID);
            StartActivity(itemsIntent);
        }
        public override void OnBackPressed()
        {
            /*if (Cart.Count!=0)
            {
                new Android.Support.V7.App.AlertDialog.Builder(this)
                  .SetIcon(Resource.Drawable.ic_alert)
                  .SetTitle("Clear Cart")
                  .SetMessage("By exiting this page all cart items will be lost")
                  .SetPositiveButton("Yes", (c, ev) =>
                  {
                      Cart.Clear();
                      base.OnBackPressed();
                  })
                   .SetNegativeButton("No", (c, ev) => { })
                   .Show();
            }*/
            base.OnBackPressed();
        }
    }
}
