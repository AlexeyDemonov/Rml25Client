using System;

namespace Rml25Client
{
	class FakeModel
	{
#pragma warning disable 0067
		public event Action<Exception> ExceptionArrived;
#pragma warning restore 0067
		public event Action<string[]> DeviceListArrived;
		public event Action<DeviceData[]> DeviceDataArrived;

		private string _serverAddress;

		public void SetServerAddress(string address)
		{
			_serverAddress = address;
		}

		public void RequestDeviceList()
		{
			//TODO
			DeviceListArrived?.Invoke( new string[] { "Датчик 1", "Датчик 2" } );
		}

		public void RequestDeviceData(string deviceName, DateTime from, DateTime to)
		{
			//TODO
			var testDateTime = DateTime.Now;

			DeviceDataArrived?.Invoke(
				new DeviceData[]
				{
					new DeviceData(){ WaterLevel=30, DateTime=testDateTime },
					new DeviceData(){ WaterLevel=25, DateTime=testDateTime.AddSeconds(10.0) },
					new DeviceData(){ WaterLevel=15, DateTime=testDateTime.AddSeconds(20.0) },
					new DeviceData(){ WaterLevel=35, DateTime=testDateTime.AddSeconds(30.0) },
					new DeviceData(){ WaterLevel=40, DateTime=testDateTime.AddSeconds(40.0) },
					new DeviceData(){ WaterLevel=35, DateTime=testDateTime.AddSeconds(50.0) },
				}
			);
		}
	}
}
