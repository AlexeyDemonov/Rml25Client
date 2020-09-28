using System;

namespace Rml25Client
{
	class DeviceData
	{
		public string DeviceName;
		public float BatteryLevel;
		public DateTime BatteryLevelDate;
		public int SignalLevel;
		public DateTime SignalLevelDate;

		public WaterLevelData[] WaterLevelData;
	}
}
