﻿using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Rml25Client
{
	class Model
	{
		public event Action<Exception> ExceptionArrived;
		public event Action<string[]> DeviceListArrived;
		public event Action<DeviceData[]> DeviceDataArrived;

		private string _serverAddress = @"root.gprs.dns-cloud.net";
		private string _port = "3306";
		private string _login = "remote_app";
		private string _password = "#Er45ty6";

		private string ConnectionString => $"Server={_serverAddress};Port={_port};Database=test1;Uid={_login};Pwd={_password};";

		public void SetServerAddress(string address, string port, string login, string password)
		{
			_serverAddress = address;
			_port = port;
			_login = login;
			_password = password;
		}

		public void RequestDeviceList()
		{
			try
			{
				using (var connection = new MySqlConnection(ConnectionString))
				{
					connection.Open();
					var query = "SELECT device_id FROM sim800 GROUP BY device_id";
					var command = new MySqlCommand(query, connection);
					var reader = command.ExecuteReader();

					var result = new List<string>();

					while (reader.Read())
						result.Add(reader.GetString("device_id"));

					DeviceListArrived?.Invoke(result.ToArray());
				}
			}
			catch (Exception exception)
			{
				ExceptionArrived?.Invoke(exception);
			}
		}

		public void RequestDeviceData(string deviceName, DateTime from, DateTime to)
		{
			try
			{
				using (var connection = new MySqlConnection(ConnectionString))
				{
					connection.Open();

					var fromFormatted = FormatDateTime(from);
					var toFormatted = FormatDateTime(to);
					var query = $"SELECT * FROM sim800 WHERE device_id = '{deviceName}' AND last_update >= '{fromFormatted}' AND last_update <= '{toFormatted}'";
					var command = new MySqlCommand(query, connection);
					var reader = command.ExecuteReader();

					var result = new List<DeviceData>();

					while (reader.Read())
					{
						result.Add(new DeviceData() { DateTime = reader.GetDateTime("last_update"), WaterLevel = reader.GetInt32("water_level") });
					}

					DeviceDataArrived?.Invoke(result.ToArray());
				}
			}
			catch (Exception exception)
			{
				ExceptionArrived?.Invoke(exception);
			}
		}

		private string FormatDateTime(DateTime dateTime)
		{
			return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
		}
	}
}
