using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace WPF.StringIntrapolation
{
    [MarkupExtensionReturnType(typeof(string))]
    public class Intrapolate : MarkupExtension
    {
        public Intrapolate() { }

        public Intrapolate(string format)
        { Format = format; }

        private readonly static IntrapolationConverter converter = new IntrapolationConverter();
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var targetProvider = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
            var target = targetProvider.TargetObject as FrameworkElement;

            var binding = new Binding
            {
                ConverterParameter = Format,
                Converter = converter
            };
            return binding.ProvideValue(serviceProvider);
        }

        public string Format { get; set; }
    }
}
