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
			this.CurrentData = GetDeviceDataRequest?.Invoke(this.SelectedDevice, this.StartDateTime, this.EndDateTime);
			var dataSwap = this.CurrentData;
			this.CurrentData = null;
			this.CurrentData = dataSwap;
			RaisePropertyChange(nameof(CurrentData));
		}
	}
}
