using System.Windows;

namespace TestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int count = 0;
        private int interval = 1000;
        private int changes = 2;
        private PCLTimer.Timer _timer;
        public MainWindow()
        {
            InitializeComponent();

            start.Click += (s, e)=>
            _timer = new PCLTimer.Timer(
                state => Dispatcher.Invoke(() => output.Text = (count++).ToString()), 
                null, interval, interval);

            change.Click += (s, e) => _timer.Change(0, interval / changes++);
            change1.Click += (s, e) => _timer.Change(-1, interval);

            cancel.Click += (s, e) => _timer.Change(-1, interval);
        }
    }
}
