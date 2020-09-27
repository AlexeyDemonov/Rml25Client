using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Rml25Client
{
	partial class ViewModel : INotifyPropertyChanged
	{
		//==========================================================================
		//Binder
		public event PropertyChangedEventHandler PropertyChanged;

		private void RaisePropertyChange([CallerMemberName] string propertyname = default(string))
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
		}

		//==========================================================================
		//Binded properties
		public string Title { get; set; }
		public DeviceData[] CurrentData { get; private set; }
		public string[] DeviceList { get; private set; }
		public string SelectedDevice { get; set; } = Constants.NOT_SELECTED;
		public DateTime StartDateTime { get; set; }
		public DateTime EndDateTime { get; set; }
		public ICommand GetDataCommand { get; private set; }

		public UpdateRate[] UpdateRates { get; private set; }
		public UpdateScope[] UpdateScopes { get; private set; }
		public int SelectedUpdateRate { get; set; } = 0;
		public int SelectedUpdateScope { get; set; } = 0;
		public ICommand StartAutoUpdateCommand { get; private set; }

		private void InitializeBindedProperties()
		{
			Title = $"{Constants.TITLE} {Constants.VERSION}";
			DeviceList = new string[] { Constants.NOT_SELECTED };
			StartDateTime = DateTime.Now.Subtract(TimeSpan.FromDays(1.0));
			EndDateTime = DateTime.Now;
			GetDataCommand = new ViewCommand(GetDataFromDevice);

			UpdateRates = Enum.GetValues(typeof(UpdateRate)).Cast<UpdateRate>().ToArray();
			UpdateScopes = Enum.GetValues(typeof(UpdateScope)).Cast<UpdateScope>().ToArray();
			StartAutoUpdateCommand = new ViewCommand(StartAutoUpdate);
		}
	}
}