using Android.App;
using Android.Widget;
using Android.OS;
using LoaderLibrary;

namespace AndroidApp
{
    [Activity(Label = "AndroidApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        public MainActivity()
        {
            Current = this;
        }
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            // SetContentView (Resource.Layout.Main);
            SetContentView(Resource.Layout.Main);

            var button = FindViewById<Button>(Resource.Id.updateAction);
            button.Click += delegate
            {
                Loader.UpdateMessage();
            };
            Loader.Start<AndroidMessageShower>();
        }

        public void ShowMessage(string message)
        {
            var text = FindViewById<TextView>(Resource.Id.messageDisplay);
            text.Text = message;
        }

        public static MainActivity Current { get; private set; }
    }

    internal class AndroidMessageShower : IShowMessage
    {
        private readonly IMessageProvider _messageProvider;
        public AndroidMessageShower(IMessageProvider messageProvider)
        {
            _messageProvider = messageProvider;
        }

        public void ShowMessage()
        {
            MainActivity.Current.ShowMessage(_messageProvider.Message);
        }
    }
}

