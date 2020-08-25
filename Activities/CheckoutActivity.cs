using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    [Activity(Label = "CheckoutActivity")]
    public class CheckoutActivity : Activity
    {
        Spinner spinner;
        Button confirmPurchaseBtn;
        RadioGroup locationRG;
        RadioButton selectedLocation;
        EditText otherLocation;
        RecyclerView mRecycleView;
        RecyclerView.LayoutManager mLayoutManager;
        CheckoutListAdapter mAdapter;
        ItemsList items;
        OffersList offers;
        TextView TotPrice, TotTax, TotPriceAfTax;
        Controller controller;
        int CheckedIndex = 0;
        List<string> userInfo;
        string SelectedPaymentMeth, userName;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_checkout);

            ISharedPreferences pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
            userName = pref.GetString("Username", String.Empty);

            controller = new Controller();
            userInfo = controller.RetreiveUser(userName);
            TotPrice = (TextView)FindViewById(Resource.Id.checkoutListTPTextView);
            TotTax = (TextView)FindViewById(Resource.Id.checkoutListTPTTextView);
            TotPriceAfTax = (TextView)FindViewById(Resource.Id.checkoutListTPATTextView);
            otherLocation = (EditText)FindViewById(Resource.Id.otherLocationEditText);
            spinner = FindViewById<Spinner>(Resource.Id.payMethSpinner);
            confirmPurchaseBtn = FindViewById<Button>(Resource.Id.ConfirmPurchaseBtn);
            confirmPurchaseBtn = (Button)FindViewById(Resource.Id.ConfirmPurchaseBtn);
            locationRG = (RadioGroup)FindViewById(Resource.Id.LocationRadioGrp);
            locationRG.Check(0);
            locationRG.CheckedChange += LocationRG_CheckedChange;
            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.paymentMethArray, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;

            items = MainActivity.CartItems;
            offers = MainActivity.CartOffers;
            for(int i = 0 ; i < offers.numoffers ; i++)
            {
                Item item = new Item(3, 4, offers[i].offerName, "d", null, 4, offers[i].offerPrice, 1," ",1);             //Adding Offer as an Item
                items.Add(item);
            }
            mRecycleView = FindViewById<RecyclerView>(Resource.Id.ItemsCheckoutRecyclerView);
            mLayoutManager = new LinearLayoutManager(this);
            mRecycleView.SetLayoutManager(mLayoutManager);
            mAdapter = new CheckoutListAdapter(items);
            mAdapter.ItemClick += MAdapter_ItemClick; ;
            confirmPurchaseBtn.Click += ConfirmPurchaseBtn_Click;
            mRecycleView.SetAdapter(mAdapter);
            TotPrice.Text = items.CalcTotal().ToString();
            TotTax.Text = items.CalcTax().ToString();
            TotPriceAfTax.Text = (items.CalcTotal() + items.CalcTax()+10).ToString();
        }

        private void ConfirmPurchaseBtn_Click(object sender, EventArgs e)
        {
            string AddressLoc;
            if (CheckedIndex == 0)
            {
                AddressLoc = userInfo[User.columnAddress];
                if (AddressLoc == " ") locationRG.Check(1);
                Toast.MakeText(this, "No Address Registered, Enter Custom Address", ToastLength.Short).Show();
                return;
            }
            else
            {
                if(otherLocation.Text.Length == 0)
                {
                    Toast.MakeText(this, "Enter your Address", ToastLength.Short).Show();
                    return;
                }
                else
                {
                    AddressLoc = otherLocation.Text;
                    List<string> items;
                    items = controller.getAllAreas();
                    if (!items.Contains(AddressLoc))
                    {
                        Toast.MakeText(this, "Enter a Valid Address", ToastLength.Short).Show();
                        return;
                    }
                    else
                    {

                    }
                }
            }
            int storeId = MainActivity.CartItems[0].itemStoreID;
            List<int> couriersList = controller.getAvailableCouriers(storeId);
            while (couriersList.Count == 0)
            {
                DelayActionAsync(1000);
                couriersList = controller.getAvailableCouriers(storeId);
            }
            
            int courierId = ShuffleCouriers(couriersList);
            controller.insertOrder(storeId, int.Parse(TotPriceAfTax.Text), DateTime.Now, DateTime.Now.AddHours(1));
            int oId = controller.getLatestOrderId();
            for(int i = 0 ; i< MainActivity.CartItems.numitems ; i++)
            { 
                controller.insertOrderContainsItem(oId, MainActivity.CartItems[i].itemID, MainActivity.CartItems[i].itemQuantity);
            }
            for (int i = 0; i < MainActivity.CartOffers.numoffers; i++)
            {
                controller.insertOrderContainsOffer(oId, MainActivity.CartOffers[i].offerID);
            }
            int done = controller.insertCurrentOrder(oId, userName, AddressLoc, courierId);
            if (done == 1)
            {
                Toast.MakeText(this, "Order Added Succesfully", ToastLength.Long).Show();
            }
            else
            {
                Toast.MakeText(this, "Error Adding Order", ToastLength.Long).Show();
            }
            Intent intent = new Intent(this,typeof(MainActivity));
            StartActivity(intent);
        }

        private void MAdapter_ItemClick(object sender, int e)
        {
            
        }

        private void LocationRG_CheckedChange(object sender, RadioGroup.CheckedChangeEventArgs e)
        {
            selectedLocation = (RadioButton)FindViewById(locationRG.CheckedRadioButtonId);
            if (selectedLocation.Text == "Other")
            {
                otherLocation.Visibility = ViewStates.Visible;
                CheckedIndex = 1;
            }
            else
            {
                otherLocation.Visibility = ViewStates.Invisible;
                CheckedIndex = 0;
            }
        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            SelectedPaymentMeth = spinner.SelectedItem.ToString();
        }
        private int ShuffleCouriers(List<int> list)
        {
            int size = list.Count;
            int n1, n2, n3, n4, n5;
            n1 = list[size - 1];
            n2 = list[size / 2];
            n3 = list[DateTime.Now.Second%size];
            n4 = list[0];
            n4 += n2*6 - n3/4 + n4/9 - n1;
            n4 %= size;
            return list[n4];
        }
        public async Task DelayActionAsync(int delay)
        {
            await Task.Delay(delay);

        }
    }
}