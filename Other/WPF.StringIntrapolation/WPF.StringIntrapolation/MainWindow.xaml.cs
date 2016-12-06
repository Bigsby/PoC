using System.Windows;

namespace WPF.StringIntrapolation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new Item
            {
                Format = "This is the formatted version {Child.Name}",
                Name = "Parent",
                Child = new Item
                {
                    Name = "Child",
                    Child = new Item
                    {
                        Name = "Grandchild",
                        Child = new Item
                        {
                            Name = "Grangrandchild"
                        }
                    }
                }
            };

            InitializeComponent();
        }
    }

    public static class Strings
    {
        public const string ItemText = "This is the {Name}";
    }

    public class Item
    {
        public string Format { get; set; }
        public string Name { get; set; }
        public Item Child { get; set; }
    }
}
