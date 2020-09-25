using System;
using System.Collections.Generic;
using System.Windows;

namespace Rml25Client
{
	partial class ViewModel
	{
		public event Action DeviceListRequest;
		public event Action<string, DateTime, DateTime> DeviceDataRequest;

		public ViewModel()
		{
			Application.Current.Startup += OnApplicationStart;
		}

		private void OnApplicationStart(object sender, StartupEventArgs e)
		{
			DeviceList = new string[] { Constants.NOT_SELECTED };
			SelectedDevice = DeviceList[0];
			StartDateTime = DateTime.Now.Subtract(TimeSpan.FromDays(1.0));
			EndDateTime = DateTime.Now;
			GetDataCommand = new ViewCommand(GetDataFromDevice);

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
		}

		private void GetDataFromDevice()
		{
			var dataChoiceValid = CheckDataChoiceValid(this.StartDateTime, this.EndDateTime);
			var deviceChoiceValid = CheckDeviceChoiceValid(this.SelectedDevice);

			if (!(dataChoiceValid && deviceChoiceValid))
				return;

			DeviceDataRequest?.Invoke(this.SelectedDevice, this.StartDateTime, this.EndDateTime);
		}

		private bool CheckDataChoiceValid(DateTime startDate, DateTime endDate)
		{
			var valid = DateTime.Compare(startDate, endDate) < 0;

			if (!valid)
				MessageBox.Show(Constants.NOT_VALID_STARTTIME_ENDTIME);

			valid = DateTime.Compare(startDate, DateTime.Now) < 0;

			if (!valid)
				MessageBox.Show(Constants.NOT_VALID_STARTTIME_TIMENOW);

			return valid;
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
