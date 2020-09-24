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
		public bool SetServerAddress(string address, out Exception exception)
		{
			

			//TODO
			exception = null;
			return true;
		}

		public string[] GetDeviceList()
		{
			//TODO
			return new string[] { "Датчик 1", "Датчик 2" };
		}

		public DeviceData[] GetDeviceData(string deviceName, DateTime from, DateTime to)
		{
			//TODO
			var testDateTime = DateTime.Now;

			return new DeviceData[]
			{
				new DeviceData(){ WaterLevel=0.3f, DateTime=testDateTime },
				new DeviceData(){ WaterLevel=0.25f, DateTime=testDateTime.AddSeconds(10.0) },
				new DeviceData(){ WaterLevel=0.15f, DateTime=testDateTime.AddSeconds(20.0) },
				new DeviceData(){ WaterLevel=0.35f, DateTime=testDateTime.AddSeconds(30.0) },
				new DeviceData(){ WaterLevel=0.40f, DateTime=testDateTime.AddSeconds(40.0) },
				new DeviceData(){ WaterLevel=0.35f, DateTime=testDateTime.AddSeconds(50.0) },
			};
		}
	}
}
