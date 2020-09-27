using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Rml25Client.Interface
{
	class UpdateScopeToStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value != null && value is UpdateScope updateScope)
			{
				switch (updateScope)
				{
					case UpdateScope.NONE:
						return Constants.NOT_SELECTED;
					case UpdateScope.HOUR:
						return "1 час";
					case UpdateScope.TWO_HOURS:
					case UpdateScope.FOUR_HOURS:
					case UpdateScope.DAY:
						return $"{(int)updateScope} часа";
					case UpdateScope.SIX_HOURS:
					case UpdateScope.TWELVE_HOURS:
						return $"{(int)updateScope} часов";
					default:
						return DependencyProperty.UnsetValue;
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
					return UpdateScope.NONE;

				if (int.TryParse(valueAsString.Split(' ')[0], out int valueAsMinutes))
					return (UpdateScope)valueAsMinutes;
			}

			/*else*/
			return UpdateScope.NONE;
		}
	}
}
