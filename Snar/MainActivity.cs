using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace Snar
{
    [Activity(Label = "Snar", MainLauncher = false, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += Button_Click;
        }

        private void Button_Click(object sender, EventArgs e1)
        {
            var builder = new AlertDialog.Builder(this);
            builder.SetMessage("Hello, World!");
            builder.SetPositiveButton("OK", (s, e) => { /* do something on OK click */ });
            builder.SetNegativeButton("Cancel", (s, e) => { /* do something on Cancel click */ });
            builder.Create().Show();
        }
    }
}

