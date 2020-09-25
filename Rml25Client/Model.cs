using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rml25Client
{
	//For now it's all gonna be sync operations, let that UI freeze while magic is happening
	class Model
	{
		public event Action<Exception> Exception;
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
					new DeviceData(){ WaterLevel=0.3f, DateTime=testDateTime },
					new DeviceData(){ WaterLevel=0.25f, DateTime=testDateTime.AddSeconds(10.0) },
					new DeviceData(){ WaterLevel=0.15f, DateTime=testDateTime.AddSeconds(20.0) },
					new DeviceData(){ WaterLevel=0.35f, DateTime=testDateTime.AddSeconds(30.0) },
					new DeviceData(){ WaterLevel=0.40f, DateTime=testDateTime.AddSeconds(40.0) },
					new DeviceData(){ WaterLevel=0.35f, DateTime=testDateTime.AddSeconds(50.0) },
				}
			);
		}
	}
}
