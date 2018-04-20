using Android.Content;
using Android.Runtime;
using Android.Views;

namespace OnTheTop.Droid
{
    public class Notifier : INotifier
    {
        public string Phrase => "Android Notifier";

        public void Notify(NotifyViewModel viewModel)
        {
            var windowManager = MainActivity.Context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();

            var theView = new NotificationView(MainActivity.Context, windowManager, viewModel);

            var param = new WindowManagerLayoutParams(
                            800, 300,
                            //ViewGroup.LayoutParams.WrapContent,
                            //ViewGroup.LayoutParams.WrapContent,
                            WindowManagerTypes.SystemAlert,
                            WindowManagerFlags.Fullscreen 
                            | WindowManagerFlags.WatchOutsideTouch 
                            | WindowManagerFlags.AllowLockWhileScreenOn
                            | WindowManagerFlags.NotFocusable,
                            Android.Graphics.Format.Translucent);
            param.Gravity = GravityFlags.Top
                | GravityFlags.Right
                ;

            windowManager.AddView(theView, param);
        }
    }
}