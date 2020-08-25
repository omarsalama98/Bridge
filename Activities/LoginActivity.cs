using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using DBapplication;
using System.Data;
using Android.Content;
using Android.Preferences;
using CourierUser.Activities;

namespace CourierUser
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = false)]
    public class LoginActivity : AppCompatActivity
    {
        Controller controller;
        Button signInButton;
        EditText UserNameET;
        EditText PasswordET;
        TextView errorTextV,signUp;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_login);

            controller = new Controller();
            signInButton = new Button(this);
            signInButton = (Button)FindViewById(Resource.Id.signInButton);
            UserNameET = (EditText)FindViewById(Resource.Id.userNameTextId);
            PasswordET = (EditText)FindViewById(Resource.Id.passwordTextId);
            errorTextV = (TextView)FindViewById(Resource.Id.error);
            signUp = (TextView)FindViewById(Resource.Id.sign_up);
            signUp.Click += SignUp_Click;
            signInButton.Click += SignInButton_Click;
        }

        private void SignUp_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(SignUpActivity));
            StartActivity(intent);
        }

        private void SignInButton_Click(object sender, System.EventArgs e)
        {
            
            string EnteredUserName, EnteredPassword;
            bool found;
            EnteredUserName = UserNameET.Text;
            EnteredPassword = PasswordET.Text;
            int res;

            res = controller.UserSignIn(EnteredUserName,EnteredPassword);
            found = res!=0?true:false;

            if (!found)
            {
                errorTextV.Visibility = Android.Views.ViewStates.Visible;
            }
            else
            {
                errorTextV.Visibility = Android.Views.ViewStates.Invisible;
                ISharedPreferences pref = Application.Context.GetSharedPreferences("UserInfo", FileCreationMode.Private);
                ISharedPreferencesEditor editor = pref.Edit();
                editor.PutString("Username", EnteredUserName);
                editor.PutBoolean("SignedIn", true);
                editor.Apply();
                Intent intent = new Intent(this,typeof(MainActivity));
                StartActivity(intent);
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}