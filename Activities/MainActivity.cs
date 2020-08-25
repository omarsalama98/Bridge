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
using SupportToolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;
using Android.Content.Res;
using CourierUser.Fragments;

namespace CourierUser
{

    [Activity(Label = "Home",MainLauncher =false,Theme ="@style/MyTheme")]
    public class MainActivity : AppCompatActivity
    {
        DrawerLayout drawerLayout;
        NavigationView navigationView;
        IMenuItem previousItem;
        public static ItemsList CartItems;
        public static OffersList CartOffers;
        Android.Support.V7.App.ActionBarDrawerToggle toggle;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_drawer);
            CartItems = new ItemsList();
            CartOffers = new OffersList();
            SupportFragmentManager.BeginTransaction()
                               .Add(Resource.Id.content_frame, new HomeFragment())
                               .Commit();
            //Finding toolbar and adding to actionbar  
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            //For showing back button  
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);
            SupportActionBar.SetDisplayShowTitleEnabled(false);
            //setting Hamburger icon Here  
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);
            //Getting Drawer Layout declared in UI and handling closing and open events  
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawerLayout.DrawerOpened += DrawerLayout_DrawerOpened;
            drawerLayout.DrawerClosed += DrawerLayout_DrawerClosed;
            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            toggle = new Android.Support.V7.App.ActionBarDrawerToggle
            (
                    this,
                    drawerLayout,
                    Resource.String.openDrawer,
                    Resource.String.closeDrawer
            );
            drawerLayout.AddDrawerListener(toggle);
            //Synchronize the state of the drawer indicator/affordance with the linked DrawerLayout  
            toggle.SyncState();
            //Handling click events on Menu items  
            navigationView.NavigationItemSelected += (sender, e) =>
            {

                if (previousItem != null)
                    previousItem.SetChecked(false);

                navigationView.SetCheckedItem(e.MenuItem.ItemId);

                previousItem = e.MenuItem;

                switch (e.MenuItem.ItemId)
                {
                    case Resource.Id.nav_home:
                        ListItemClicked(0);
                        break;
                    case Resource.Id.nav_profile:
                        ListItemClicked(1);
                        break;
                    case Resource.Id.nav_payment:
                        ListItemClicked(2);
                        break;
                    case Resource.Id.nav_FAQ:
                        ListItemClicked(3);
                        break;
                    case Resource.Id.nav_contactUs:
                        ListItemClicked(4);
                        break;
                    case Resource.Id.nav_logOut:
                        ListItemClicked(5);
                        break;
                    case Resource.Id.nav_current_order:
                        ListItemClicked(6);
                        break;
                    case Resource.Id.nav_my_orders:
                        ListItemClicked(7);
                        break;
                    case Resource.Id.nav_request:
                        ListItemClicked(8);
                        break;
                }

                drawerLayout.CloseDrawers();
            };
        }


        private void DrawerLayout_DrawerClosed(object sender, DrawerLayout.DrawerClosedEventArgs e)
        {
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);  
        }

        private void DrawerLayout_DrawerOpened(object sender, DrawerLayout.DrawerOpenedEventArgs e)
        {
             SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_arrow_back);  
        }

        private void ListItemClicked(int position)
        {
            Android.Support.V4.App.Fragment fragment = null;
            switch (position)
            {
                case 0:
                    fragment = new HomeFragment();
                    break;
                case 1:
                    fragment = new ProfileFragment();
                    break;
                case 2:
                    fragment = new PaymentFragment();
                    break;
                case 3:
                    fragment = new FAQFragment();
                    break;
                case 4:
                    fragment = new ContactUSFragment();
                    break;
                case 5:
                    new Android.Support.V7.App.AlertDialog.Builder(this)
                   .SetIcon(Resource.Drawable.ic_alert)
                   .SetTitle("Closing Activity")
                   .SetMessage("Are you sure you want to Log Out?")
                   .SetPositiveButton("Yes", (c, ev) =>
                   {
                       ISharedPreferences pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
                       ISharedPreferencesEditor edit = pref.Edit();
                       edit.PutString("Username", String.Empty);
                       edit.PutBoolean("SignedIn", false);
                       edit.Apply();
                       var activity = (Activity)this;
                       activity.FinishAffinity();
                   })
                    .SetNegativeButton("No", (c, ev) => { })
                    .Show();
                    break;
                case 6:
                    fragment = new CurrentOrderFragment();
                    break;
                case 7:
                    fragment = new MyOrdersFragment();
                    break;
                case 8:
                    fragment = new MakeARequestFragment();
                    break;


            }
            if (fragment != null)
            {
                SupportFragmentManager.BeginTransaction()
                               .Replace(Resource.Id.content_frame, fragment)
                               .Commit();
            }
            

        }

        //Handling Back Key Press  
        public override void OnBackPressed()
        {
            if (drawerLayout.IsDrawerOpen((int)GravityFlags.Start))
            {
                drawerLayout.CloseDrawer((int)GravityFlags.Start);
            }
            else
            {
                new Android.Support.V7.App.AlertDialog.Builder(this)
                  .SetIcon(Resource.Drawable.ic_alert)
                  .SetTitle("Exiting App")
                  .SetMessage("Are you sure you want to exit?")
                  .SetPositiveButton("Yes", (c, ev) =>
                  {
                      CartItems.Clear();
                      CartOffers.Clear();
                      var activity = (Activity)this;
                      activity.FinishAffinity();
                  })
                   .SetNegativeButton("No", (c, ev) => { })
                   .Show();
            }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    if(drawerLayout.IsDrawerOpen(Android.Support.V4.View.GravityCompat.Start))
                            drawerLayout.CloseDrawer(Android.Support.V4.View.GravityCompat.Start);
                    else    drawerLayout.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
                    return true;
                
            }
            return base.OnOptionsItemSelected(item);
        }

        //Resposnible for mainting state,suppose if you suddenly rotated screen than drawer should not losse it context so you have save drawer states like below  
        protected override void OnPostCreate(Bundle savedInstanceState)
        {

            base.OnPostCreate(savedInstanceState);
            toggle.SyncState();

        }
        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            toggle.OnConfigurationChanged(newConfig);
        }

    }
}
