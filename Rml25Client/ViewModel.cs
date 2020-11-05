using System;
using System.Collections.Generic;
using System.Windows;

namespace Rml25Client
{
	partial class ViewModel
	{
		public event Func<Credentials> LoadCredentialsRequest;
		public event Action<Credentials> CredentialsArrived;
		public event Action DeviceListRequest;
		public event Action<string, DateTime, DateTime> DeviceDataRequest;
		public event Action<string, UpdateRate,UpdateScope> AutoUpdateRequest;
		public event Func<bool> StopAutoUpdateRequest;

		private bool _closeOnException = true;

		public void OnApplicationStart(object sender, StartupEventArgs e)
		{
			InitializeBindedProperties();
		}
		
		public void OnMainWindowLoaded(object sender, RoutedEventArgs e)
		{
			var loadedCredentials = LoadCredentialsRequest?.Invoke();
			var enterWindow = new CredentialsRequestWindow();

			if(loadedCredentials != null)
				enterWindow.SetCredentials(loadedCredentials);

			var dialogResult = enterWindow.ShowDialog();

			if(dialogResult != true)
			{
				OnAppException(new Exception(message: Constants.REFUSED_ENTER));
				return;
			}

			var userInputCredentials = enterWindow.GetCredentials();
			CredentialsArrived?.Invoke(userInputCredentials);
			DeviceListRequest?.Invoke();
		}

		public void OnDeviceListArrived(string[] deviceList)
		{
			DeviceList = null;
			var newDeviceList = new List<string>(deviceList);
			newDeviceList.Insert(0, Constants.NOT_SELECTED);
			DeviceList = newDeviceList.ToArray();

			SelectedDevice = DeviceList[0];

			RaisePropertyChange(nameof(DeviceList));
			RaisePropertyChange(nameof(SelectedDevice));

			_closeOnException = false;
		}

		public void OnDeviceDataArrived(DeviceData deviceData)
		{
			CurrentData = null;
			CurrentData = deviceData.WaterLevelData;
			RaisePropertyChange(nameof(CurrentData));

			TableTitle = string.Format(Constants.TABLE_TITLE_TEMPLATE, deviceData.DeviceName, deviceData.BatteryLevel.ToString("F2"), deviceData.SignalLevel);
			RaisePropertyChange(nameof(TableTitle));
		}

		public void OnAppException(Exception exception)
		{
			MessageBox.Show(exception.Message, Constants.ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);

			if (_closeOnException)
				Application.Current.MainWindow.Close();
		}

		private void WarnUser(string warning)
		{
			MessageBox.Show(warning, string.Empty, MessageBoxButton.OK, MessageBoxImage.Warning);
		}
	}
}
