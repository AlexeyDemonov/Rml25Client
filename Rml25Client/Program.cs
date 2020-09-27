using System;

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
			viewModel.CredentialsArrived += model.SetConnectionData;
			viewModel.DeviceListRequest += model.RequestDeviceList;
			viewModel.DeviceDataRequest += model.RequestDeviceData;
			model.DeviceListArrived += viewModel.OnDeviceListArrived;
			model.DeviceDataArrived += viewModel.OnDeviceDataArrived;
			model.ExceptionArrived += viewModel.OnAppException;

			var credentialsSaveLoader = new CredentialsSaveLoader();
			viewModel.LoadCredentialsRequest += credentialsSaveLoader.LoadCredentials;
			viewModel.CredentialsArrived += credentialsSaveLoader.SaveCredentials;

			model.ExceptionArrived += Logger.LogTheException;

			app.InitializeComponent();
			app.Run();
		}
	}
}
