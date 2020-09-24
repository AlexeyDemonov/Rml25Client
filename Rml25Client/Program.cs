using System;
using System.Windows;

namespace Rml25Client
{
	class Program
    {
		[STAThread]
		public static void Main(string[] args)
		{
			var app = new App();
			var window = new MainWindow();
			app.MainWindow = window;

			var viewModel = new ViewModel();
			window.DataContext = viewModel;

			var model = new Model();
			viewModel.GetDeviceListRequest += model.GetDeviceList;
			viewModel.GetDeviceDataRequest += model.GetDeviceData;

			app.InitializeComponent();
			app.Run();
		}
	}
}
