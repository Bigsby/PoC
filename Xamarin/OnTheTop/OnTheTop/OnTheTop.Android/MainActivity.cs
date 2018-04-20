using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Content;

namespace OnTheTop.Droid
{
    [Activity(Label = "OnTheTop", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        internal static Context Context;
        protected override void OnCreate(Bundle bundle)
        {
            Context = this.ApplicationContext;
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            Xamarin.Forms.DependencyService.Register<Notifier>();

            LoadApplication(new App());
        }
    }
}

