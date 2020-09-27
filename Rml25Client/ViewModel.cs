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

		public ViewModel()
		{
			Application.Current.Startup += OnApplicationStart;
			Application.Current.MainWindow.Loaded += OnMainWindowLoaded;
		}

		private void OnApplicationStart(object sender, StartupEventArgs e)
		{
			Title = $"{Constants.TITLE} {Constants.VERSION}";
			DeviceList = new string[] { Constants.NOT_SELECTED };
			SelectedDevice = DeviceList[0];
			StartDateTime = DateTime.Now.Subtract(TimeSpan.FromDays(1.0));
			EndDateTime = DateTime.Now;
			GetDataCommand = new ViewCommand(GetDataFromDevice);
		}
		
		private void OnMainWindowLoaded(object sender, RoutedEventArgs e)
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
		}

		public void OnDeviceDataArrived(DeviceData[] deviceData)
		{
			CurrentData = null;
			CurrentData = deviceData;
			RaisePropertyChange(nameof(CurrentData));
		}

		public void OnAppException(Exception exception)
		{
			MessageBox.Show(exception.Message, Constants.ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);
			Application.Current.MainWindow.Close();
		}

		private void GetDataFromDevice()
		{
			var dataChoiceValid = CheckDataChoiceValid(StartDateTime, EndDateTime);
			var deviceChoiceValid = CheckDeviceChoiceValid(SelectedDevice);

			if (!(dataChoiceValid && deviceChoiceValid))
				return;

			DeviceDataRequest?.Invoke(SelectedDevice, StartDateTime, EndDateTime);
		}

		private bool CheckDataChoiceValid(DateTime startDate, DateTime endDate)
		{
			var validStartEnd = DateTime.Compare(startDate, endDate) < 0;

			if (!validStartEnd)
				MessageBox.Show(Constants.NOT_VALID_STARTTIME_ENDTIME);

			var validStartNow = DateTime.Compare(startDate, DateTime.Now) < 0;

			if (!validStartNow)
				MessageBox.Show(Constants.NOT_VALID_STARTTIME_TIMENOW);

			return validStartEnd && validStartNow;
		}

		private bool CheckDeviceChoiceValid(string selectedDevice)
		{
			var valid = selectedDevice != Constants.NOT_SELECTED;

			if (!valid)
				MessageBox.Show(Constants.NOT_VALID_DEVICE_CHOICE);

			return valid;
		}
	}
}
