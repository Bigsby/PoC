using System;
using System.Windows;
using System.Windows.Interop;

namespace KeyboardKeys
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var hook = new HwndSourceHook(Handler);
            Loaded += (s, e) => HwndSource.FromHwnd(new WindowInteropHelper(this).Handle).AddHook(hook);
            Closing += (s, e) => HwndSource.FromHwnd(new WindowInteropHelper(this).Handle).RemoveHook(hook);

            KeyDown += (s, e) =>
            {
                output.Text += $"KeyDown:\nkey:{(int)e.Key}\n\n";
                output.ScrollToEnd();
            };
        }

        private const int WM_GETDLGCODE = 0x0087;
        private IntPtr Handler(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == 132) return IntPtr.Zero;

            if (!notify.IsChecked.HasValue || !notify.IsChecked.Value || msg != WM_GETDLGCODE) return IntPtr.Zero;

            output.Text += $"Hook:\nhwnd:{hwnd}\nmsg:{msg}\nwParam:{wParam}\nlParam:{lParam}\n-----\n\n";
            output.ScrollToEnd();
            return IntPtr.Zero;
        }
    }
}
