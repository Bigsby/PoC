using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace OnTheTop
{
    public partial class MainPage : ContentPage
    {
        private readonly INotifier _notifier;
        public MainPage()
        {
            InitializeComponent();
            _notifier = DependencyService.Get<INotifier>();
            BindingContext = new MainPageViewModel(this)
            {
                Phrase = _notifier.Phrase,
                Text = "This is a text to be displayed in the toast notifications",
                
            };

        }

        class MainPageViewModel : INotifyPropertyChanged
        {
            private readonly INotifier _notifier;
            private readonly Page _page;

            public MainPageViewModel(Page page)
            {
                _notifier = DependencyService.Get<INotifier>();
                _page = page;
            }

            public string Phrase { get; set; }
            public string Text { get; set; }
            public bool ShowImage { get; set; }
            public event PropertyChangedEventHandler PropertyChanged;

            public ICommand Notify => new Command(() => _notifier.Notify(new NotifyViewModel
            {
                Text = Text,
                ImageUrl = "https://i.pinimg.com/originals/8d/0a/c7/8d0ac7252339ddfe2caf4990043f0bfe.png"
                //ImageUrl = ShowImage ? "http://vignette.wikia.nocookie.net/lego/images/2/2d/LEGO_logo.jpg" : null,
                //Action = () =>
                //    Device.BeginInvokeOnMainThread(() =>
                //        _page.DisplayAlert(" ", "The is an alert from a notification", "OK")
                //    )
            }));
        }
    }
}
