using LoaderLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWPApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Current = this;
            updateAction.Click += (s, e) => Loader.UpdateMessage();
            Loader.Start<UWPMessageShower>();
        }

        public void ShowMessage(string message)
        {
            Task.Run(() => Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => messageDisplay.Text = message));
        }

        public static MainPage Current
        { get; private set; }
    }

    internal class UWPMessageShower : IShowMessage
    {
        private readonly IMessageProvider _messageProvider;
        public UWPMessageShower(IMessageProvider messageProvider)
        {
            _messageProvider = messageProvider;
        }

        public void ShowMessage()
        {
            MainPage.Current.ShowMessage(_messageProvider.Message);
        }
    }
}
