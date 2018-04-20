using System;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace OnTheTop.UWP
{
    class Notifier : INotifier
    {
        public string Phrase => "UWP Notifier";

        public async void Notify(NotifyViewModel viewModel)
        {
            var compactViewId = 0;
            await CoreApplication.CreateNewView().Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                var applicationView = ApplicationView.GetForCurrentView();
                compactViewId = applicationView.Id;
                var appTitleBar = applicationView.TitleBar;
                appTitleBar.ButtonBackgroundColor = Colors.White;
                appTitleBar.ButtonForegroundColor = Colors.White;
                appTitleBar.ButtonHoverBackgroundColor = Colors.White;
                appTitleBar.ButtonHoverForegroundColor = Colors.White;
                appTitleBar.ButtonInactiveBackgroundColor = Colors.White;
                appTitleBar.ButtonInactiveForegroundColor = Colors.White;
                appTitleBar.ButtonPressedBackgroundColor = Colors.White;
                appTitleBar.ButtonPressedForegroundColor = Colors.White;


                var notifyPage = new NotifyPage(viewModel);
                Window.Current.Content = notifyPage;
                //Window.Current.SetTitleBar(notifyPage);
                Window.Current.Activate();
                CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
            });
            bool viewShown = await ApplicationViewSwitcher.TryShowAsViewModeAsync(compactViewId, ApplicationViewMode.CompactOverlay);
        }
    }
}
