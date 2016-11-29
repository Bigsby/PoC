using Android.App;
using Android.Widget;
using Android.OS;
using System.Net.Http;

namespace DroidAppSetting
{
    [Activity(Label = "DroidAppSetting", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var text = FindViewById<TextView>(Resource.Id.textView1);

            try
            {
                var client = new HttpClient();
                var result = await client.GetStringAsync("http://ov-dub-ltp-016/OneViewManager/Service/MealOrdering.svc");
                text.Text = result;
            }
            catch (System.Exception ex)
            {
                var m = ex.Message;
                text.Text = m;
            }

            
        }
    }
}

