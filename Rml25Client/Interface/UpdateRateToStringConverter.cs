using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Rml25Client.Interface
{
	class UpdateRateToStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value != null && value is UpdateRate updateRate)
			{
				switch (updateRate)
				{
					case UpdateRate.NONE:
						return Constants.NOT_SELECTED;
					case UpdateRate.ONE_MINUE:
						return "1 минуту";
					default:
						return $"{(int)updateRate} минут";
				}
			}
			else
			{
				return DependencyProperty.UnsetValue;
			}
		}


		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value != null && value is string valueAsString)
			{
				if (valueAsString == Constants.NOT_SELECTED)
					return UpdateRate.NONE;

				if (int.TryParse(valueAsString.Split(' ')[0], out int valueAsMinutes))
					return (UpdateRate)valueAsMinutes;
			}

			/*else*/
			return UpdateRate.NONE;
		}
	}
}
