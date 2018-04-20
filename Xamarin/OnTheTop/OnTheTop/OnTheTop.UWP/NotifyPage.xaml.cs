using System.Windows.Input;
using Windows.UI.Xaml;
using Xamarin.Forms;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace OnTheTop.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NotifyPage : Windows.UI.Xaml.Controls.Page
    {
        public NotifyPage(NotifyViewModel vm)
        {
            this.InitializeComponent();
            DataContext = new NotifyPageViewModel(vm);
            //Tapped += (s, e) => vm.Action.Execute(null);
        }

        class NotifyPageViewModel
        {
            public NotifyPageViewModel(NotifyViewModel vm)
            {
                Text = vm.Text;
                Action = new Command(() => {
                    Window.Current.Close();
                    vm.Action();
                });
            }
            public string Text { get; }
            public ICommand Action { get; }
        }
    }
}
