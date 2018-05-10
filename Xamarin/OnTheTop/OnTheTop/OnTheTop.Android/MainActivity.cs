using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;

namespace OnTheTop.Droid
{
    [Activity(Label = "OnTheTop", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            AndroidEnvironment.UnhandledExceptionRaiser += (s, e) =>
            {
                var error = e;
            };

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            Xamarin.Forms.DependencyService.Register<Notifier>();

            LoadApplication(new App());
        }
    }
}

