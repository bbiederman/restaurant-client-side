﻿using Android.App;
using Android.Widget;
using Android.OS;
using System;

namespace customer_client
{
    [Activity(Label = "Quick Byte", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private Button login;
        private EditText emailAddress;
        private Button password;
        private Button guestLogin;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            // Set our view from the "main" layout resource
            // SetContentView (Resource.Layout.Main);
            //Test

            findViews();
            //clickHandler();
            login.Click += delegate
            {
                //Get entered username & password
                string usr = emailAddress.Text;
                string pass = password.Text;

                //Get list of valid user-pass pairs
                string[,] users = getValidUsers();

                if( credentialsAreValid( usr,pass,users ) )
                {
                    submit(usr);
                }
            };

            //Guest Login
            guestLogin.Click += delegate
            {
                //Skips validation, submits w/ guest username
                submit("Guest@test.com");
            };
        }

        private bool credentialsAreValid(string usr, string pass, string[,] list)
        {
            bool result = false;

            for (int x = 0; x < list.GetLength(0); x += 1)
            {
                if( list[x,0].Equals(usr) && list[x,1].Equals(pass) )   //If usr-pass pair matches...
                {
                    result = true;
                }
            }

            return result;
        }

        //Contains hard code. Replace it with real code when possible
        private string[,] getValidUsers()
        {
            //Initialize 2D string array w/ [x,0]=usr & [x,1]=pass
            string[,] list = new string[,] {};

            //Begin hard code
            list[0,0] = "usr1";
            list[0,1] = "pass1";
            list[1,0] = "usr2";
            list[1,1] = "pass2";
            list[2,0] = "usr3";
            list[2,1] = "pass3";
            //End hard code

            return list;
        }

        private void submit(string user)
        {
            var gettableactivity = new Android.Content.Intent(this, typeof(GetTableActivity));
            //extras here
            char[] delimiterChar = { '@' };
            string[] usernameDelim = user.Split(delimiterChar);
            gettableactivity.PutExtra("username", usernameDelim[0]);
            Console.WriteLine(usernameDelim);
            StartActivity(gettableactivity);
        }

        private void findViews()
        {
            login = FindViewById<Button>(Resource.Id.login);
            emailAddress = FindViewById<EditText>(Resource.Id.emailAddress);
            password = FindViewById<Button>(Resource.Id.password);
            guestLogin = FindViewById<Button>(Resource.Id.guestLogin);
            //throw new NotImplementedException();//
        }
    }
}

