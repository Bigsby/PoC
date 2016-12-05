using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace WPF.StringIntrapolation
{
    public class IntrapolationConverter : IValueConverter
    {
        private static Regex _replaceRegex = new Regex(@"(\{@?(?:[A-Za-z_][A-Za-z0-9_]+)(?:\.@?(?:[A-Za-z_][A-Za-z0-9_]+))*\})", RegexOptions.Compiled);
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var format = parameter as string;
            if (string.IsNullOrEmpty(format))
                return string.Empty;

            return _replaceRegex.Replace(format, match => Process(value, match));
        }

        public int @a { get; set; }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private string Process(object dataContext, Match match)
        {
            var propertyPath = match.Groups[1].Value.Trim('{', '}');

            var value = GetValue(dataContext, propertyPath);
            
            return null == value ? string.Empty : value.ToString();
        }

        private object GetValue(object dataContext, string propertyPath)
        {
            try
            {
                var propertyNames = propertyPath.Split('.');
                var currentValue = dataContext;
                foreach (var property in propertyNames)
                {
                    if (currentValue == null)
                        return null;

                    currentValue = currentValue.GetType().GetProperty(property).GetValue(currentValue);
                }

                return currentValue;
            }
            catch
            {
                return null;
            }
        }
    }
}
