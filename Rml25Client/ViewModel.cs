using System;
using System.Collections.Generic;
using System.Windows;

namespace Rml25Client
{
	partial class ViewModel
	{
		public event Func<string[]> GetDeviceListRequest;
		public event Func<string, DateTime, DateTime, DeviceData[]> GetDeviceDataRequest;

		public ViewModel()
		{
			Application.Current.Startup += OnApplicationStart;
		}

		private void OnApplicationStart(object sender, StartupEventArgs e)
		{
			var deviceList = new List<string>() { Constants.NOT_SELECTED };
			var obtainedDeviceList = GetDeviceListRequest?.Invoke();

			if (obtainedDeviceList != null)
				deviceList.AddRange(obtainedDeviceList);

			this.DeviceList = deviceList.ToArray();
			this.SelectedDevice = DeviceList[0];
			this.StartDateTime = DateTime.Now.Subtract(TimeSpan.FromDays(1.0));
			this.EndDateTime = DateTime.Now;
			this.GetDataCommand = new ViewCommand(GetData);
		}

		private void GetData()
		{
			var dataChoiceValid = CheckDataChoiceValid(this.StartDateTime, this.EndDateTime);
			var deviceChoiceValid = CheckDeviceChoiceValid(this.SelectedDevice);

			if (!(dataChoiceValid && deviceChoiceValid))
				return;

			this.CurrentData = GetDeviceDataRequest?.Invoke(this.SelectedDevice, this.StartDateTime, this.EndDateTime);
			var dataSwap = this.CurrentData;
			this.CurrentData = null;
			this.CurrentData = dataSwap;
			RaisePropertyChange(nameof(CurrentData));
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
