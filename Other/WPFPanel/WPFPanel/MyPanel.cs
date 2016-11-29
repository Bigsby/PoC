using System.Windows;
using System.Windows.Controls;

namespace WPFPanel
{
    public class MyPanel : Panel
    {
        protected override Size MeasureOverride(Size availableSize)
        {
            foreach (UIElement child in Children)
            {
                child.Measure(availableSize);
            }
            return base.MeasureOverride(availableSize);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            foreach (FrameworkElement child in Children)
            {
                child.Arrange(new Rect(-child.Margin.Left, -child.Margin.Top, finalSize.Width, finalSize.Height));
            }
            return finalSize;
        }
    }
}
