using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Util;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace Snar
{
    [Activity(Theme = "@style/Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        static readonly string TAG = "X:" + typeof(SplashActivity).Name;

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
        }


        private void CheckServer(Action complete)
        {
            try
            {

                var request = HttpWebRequest.Create(GetString(Resource.String.Host) + GetString(Resource.String.ApiIsALife));
                request.ContentType = "application/json";
                request.Method = "GET";

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new Exception();
                    }

                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        var content = reader.ReadToEnd();
                        var result = JsonConvert.DeserializeObject<bool>(content);
                        if (!result)
                        {
                            throw new Exception();
                        }

                        complete();
                    }
                }
            }
            catch
            {
                var builder = new AlertDialog.Builder(this);
                builder.SetMessage(Resource.String.ServerNotFound);
                builder.SetPositiveButton("OK", (s, e) =>
                {
                    Process.KillProcess(Process.MyPid());
                });
                builder.Create().Show();
            }
        }
        protected override void OnResume()
        {
            base.OnResume();

            Task startupWork = new Task(() =>
            {
            });

            startupWork.ContinueWith(t =>
            {
                Log.Debug(TAG, "Work is finished - start Activity1.");
                CheckServer(() =>
                {
                    StartActivity(new Intent(Application.Context, typeof(MainActivity)));
                });
            }, TaskScheduler.FromCurrentSynchronizationContext());

            startupWork.Start();
        }
    }
}

