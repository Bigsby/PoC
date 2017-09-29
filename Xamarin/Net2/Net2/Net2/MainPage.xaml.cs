using Xamarin.Forms;

namespace Net2
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            theLabel.Text = SharedLibrary.Shared.Message;
        }
    }
}
