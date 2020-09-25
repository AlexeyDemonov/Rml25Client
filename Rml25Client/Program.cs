﻿using System;
using System.Windows;

namespace Rml25Client
{
	class Program
    {
		[STAThread]
		public static void Main(string[] args)
		{
			var app = new App();
			app.DispatcherUnhandledException += (sender, excArgs) => { Logger.LogTheException(excArgs.Exception); excArgs.Handled = true; };

			var window = new MainWindow();
			app.MainWindow = window;

			var viewModel = new ViewModel();
			window.DataContext = viewModel;

			var model = new Model();
			viewModel.DeviceListRequest += model.RequestDeviceList;
			viewModel.DeviceDataRequest += model.RequestDeviceData;
			model.DeviceListArrived += viewModel.OnDeviceListArrived;
			model.DeviceDataArrived += viewModel.OnDeviceDataArrived;
			model.Exception += viewModel.OnAppException;

			model.Exception += Logger.LogTheException;

			app.InitializeComponent();
			app.Run();
		}
	}
}
