using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFPanel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var a = IsATrueValue("a", null, false);
            a = IsATrueValue(1, null, false);
            a = IsATrueValue(null, null, false);
            a = IsATrueValue(2L, null, false);
            a = IsATrueValue(1m, null, false);
            a = IsATrueValue(1f, null, false);
            a = IsATrueValue(1.0, null, false);
        }

        protected virtual bool IsATrueValue(object value, object parameter, bool defaultValue)
        {
            if (value == null)
            {
                return false;
            }
            if (value is bool)
            {
                return (bool)value;
            }
            if (value is int)
            {
                if (parameter == null)
                {
                    return (int)value > 0;
                }
                else
                {
                    return (int)value > int.Parse(parameter.ToString());
                }
            }
            if (value is double)
            {
                return (double)value > 0;
            }
            if (value is string)
            {
                return !string.IsNullOrWhiteSpace(value as string);
            }
            return defaultValue;
        }
    }
}
