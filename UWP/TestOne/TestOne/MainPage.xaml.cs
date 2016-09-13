using System;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TestOne
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

            webBrowser.NavigationStarting += (s, e) =>
            {
                if (null != e.Uri)
                    address.Text = e.Uri.ToString();
            };


            go.Click += (s, e) => {
                try
                {
                    webBrowser.Navigate(new Uri(address.Text));
                }
                catch (Exception ex)
                {
                    webBrowser.NavigateToString(ex.Message);
                }
            };

        }
    }
}
