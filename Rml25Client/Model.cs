using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Rml25Client
{
	class Model
	{
		public event Action<Exception> ExceptionArrived;
		public event Action<string[]> DeviceListArrived;
		public event Action<DeviceData> DeviceDataArrived;

		private string _serverAddress = "root.gprs.dns-cloud.net";
		private string _port = "3306";
		private string _login = "remote_app";
		private string _password = "#Er45ty6";

		private string ConnectionString => $"Server={_serverAddress};Port={_port};Database={Constants.DATABASE_NAME};Uid={_login};Pwd={_password};";

		public void SetConnectionData(Credentials credentials)
		{
			_serverAddress = credentials.Address;
			_port = credentials.Port;
			_login = credentials.Login;
			_password = credentials.Password;
		}

		public void RequestDeviceList()
		{
			var query = $"SELECT device_id FROM WaterLevel GROUP BY device_id";
			var result = new List<string>();
			Action<MySqlDataReader> readerAction = reader => result.Add(reader.GetString("device_id"));
			
			var success = ExecuteSqlCommand(query, readerAction);

			if (success)
				DeviceListArrived?.Invoke(result.ToArray());
		}

		public void RequestDeviceData(string deviceName, DateTime from, DateTime to)
		{
			var fromFormatted = FormatDateTime(from);
			var toFormatted = FormatDateTime(to);
			var query = $"SELECT * FROM WaterLevel WHERE device_id = '{deviceName}' AND last_update >= '{fromFormatted}' AND last_update <= '{toFormatted}'";
			var result = new DeviceData();
			result.DeviceName = deviceName;

			var waterLevel = new List<WaterLevelData>();
			Action<MySqlDataReader> readerAction = reader => waterLevel.Add(new WaterLevelData() { DateTime = reader.GetDateTime("last_update"), WaterLevel = reader.GetInt32("water_level") });

			var success = ExecuteSqlCommand(query, readerAction);

			if (success)
				result.WaterLevelData = waterLevel.ToArray();
			else
				return;

			query = $"SELECT * FROM BatteryLevel WHERE device_id = '{deviceName}' ORDER BY last_update DESC LIMIT 1";
			readerAction = reader => { result.BatteryLevel = (float)reader.GetInt32("Battery_Level") / 100f; result.BatteryLevelDate = reader.GetDateTime("last_update"); };

			success = ExecuteSqlCommand(query, readerAction);
			if (!success) return;

			query = $"SELECT * FROM GsmLevel WHERE device_id = '{deviceName}' ORDER BY last_update DESC LIMIT 1";
			readerAction = reader => { result.SignalLevel = reader.GetInt32("Gsm_Level"); result.SignalLevelDate = reader.GetDateTime("last_update");};

			success = ExecuteSqlCommand(query, readerAction);
			if (!success) return;

			DeviceDataArrived?.Invoke(result);
		}

		private bool ExecuteSqlCommand(string query, Action<MySqlDataReader> readerAction)
		{
			try
			{
				using (var connection = new MySqlConnection(ConnectionString))
				{
					connection.Open();
					var command = new MySqlCommand(query, connection);
					var reader = command.ExecuteReader();

					while (reader.Read())
					{
						readerAction?.Invoke(reader);
					}
				}

				return true;
			}
			catch (Exception exception)
			{
				ExceptionArrived?.Invoke(exception);
				return false;
			}
		}

		private string FormatDateTime(DateTime dateTime)
		{
			return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
		}
	}
}
