using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace CourierUser
{
    [Activity(Label = "SplashActivity",MainLauncher = true)]
    public class SplashActivity : Activity
    {
        Intent intent;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_Splash);
            ISharedPreferences pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
            string userName = pref.GetString("Username", String.Empty);
            bool signedIn = pref.GetBoolean("SignedIn", false);
            
             
            if (signedIn==false)
            {
                //No saved credentials, take user to login screen  
                intent = new Intent(this, typeof(LoginActivity));
                //this.StartActivity(intent);
            }

            else
            {
                //There are saved credentials  

                //Successful take the user to application  
                intent = new Intent(this, typeof(MainActivity));

                intent.PutExtra("UserName",userName);

                //this.StartActivity(intent);

               
            }
            // Create your application here
        }
        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(() => { SimulateStartup(); });
            startupWork.Start();
        }

        // Simulates background work that happens behind the splash screen
        async void SimulateStartup()
        {
            await Task.Delay(4000); // Simulate a bit of startup work.
            StartActivity(intent);
        }
    }
}