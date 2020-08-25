using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Icu.Text;
using Android.Icu.Util;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DBapplication;

namespace CourierUser.Activities
{
    [Activity(Label = "SignUpActivity")]
    public class SignUpActivity : Activity
    {
        EditText userName, fName, mName, lName, password, email, address, phoneNum, creditCardNum, creditCardCVC;
        DatePicker birthDate;
        Button confirmSignUp;
        Controller controller;
        string UserName, FName, MName, LName, Password, Email, Address, PhoneNum, CreditCardNum, BDate;
        int CreditCardCVC;
        DateTime BirthDate;
        Spinner sexSpinner;
        char selectedSex;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_sign_up);
            controller = new Controller();

            userName = (EditText)FindViewById(Resource.Id.UserNamesignUpET);
            fName = (EditText)FindViewById(Resource.Id.FirstNamesignUpET);
            mName = (EditText)FindViewById(Resource.Id.MiddleNamesignUpET);
            lName = (EditText)FindViewById(Resource.Id.LastNamesignUpET);
            password = (EditText)FindViewById(Resource.Id.PasswordsignUpET);
            email = (EditText)FindViewById(Resource.Id.EmailsignUpET);
            address = (EditText)FindViewById(Resource.Id.AddressSignUpET);
            phoneNum = (EditText)FindViewById(Resource.Id.phoneNumsignUpET);
            creditCardNum = (EditText)FindViewById(Resource.Id.creditCardNumsignUpET);
            creditCardCVC = (EditText)FindViewById(Resource.Id.creditCardCVCsignUpET);
            confirmSignUp = FindViewById<Button>(Resource.Id.confirmSignUpBtn);
            birthDate = FindViewById<DatePicker>(Resource.Id.birthDatesignUpET);

            confirmSignUp.Click += ConfirmSignUp_Click;
           

            sexSpinner = FindViewById<Spinner>(Resource.Id.sexSpinner);
            selectedSex = 'M';
            sexSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(sexSpinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.sexArray, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            sexSpinner.Adapter = adapter;
            // Create your application here
        }

        private void ConfirmSignUp_Click(object sender, EventArgs e)
        {
            UserName = userName.Text;
            FName = fName.Text;
            MName = mName.Text;
            LName = lName.Text;
            Password = password.Text;
            Email = email.Text;
            Address = address.Text;
            PhoneNum = phoneNum.Text;
            CreditCardNum = creditCardNum.Text;
            CreditCardCVC = creditCardCVC.Text.Length == 0 ? 0 : int.Parse(creditCardCVC.Text);
            BirthDate = birthDate.DateTime;
            BDate = String.Format("{0:dd-MM-yyyy}", BirthDate);     

            if (UserName.Length == 0)
            {
                userName.Text = "Enter User Name!!!";
                return;
            }
            if (FName.Length == 0)
            {
                fName.Text = "Enter First Name!!!";
                return;
            }
            if (MName.Length == 0)
            {
                mName.Text = "Enter Middle Name!!!";
                return;
            }
            if (LName.Length == 0)
            {
                lName.Text = "Enter Last Name!!!";
                return;
            }
            if (Password.Length == 0)
            {
                password.Text = "Enter Password!!!";
                return;
            }
            if (Email.Length == 0)
            {
                email.Text = "Enter Email!!!";
                return;
            }
            if (PhoneNum.Length == 0)
            {
                phoneNum.Text = "Enter Phone Number!!!";
                return;
            }
            if (Address.Length == 0)
            {
                Address = " ";
            }
            controller.InsertClient(UserName, FName, MName, LName, selectedSex, Password, Email, BDate, Address);
            controller.InsertClientPhoneNum(UserName, PhoneNum);
            if (CreditCardNum.Length==16 && CreditCardCVC.ToString().Length == 3) controller.InsertCreditCard(UserName,CreditCardNum,CreditCardCVC);
            Intent intent = new Intent(this, typeof(LoginActivity));
            StartActivity(intent);
        }

        private void sexSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            selectedSex = spinner.SelectedItem.ToString()[0];
        }
    }
}