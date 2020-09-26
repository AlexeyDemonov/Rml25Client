using System;
using System.ComponentModel;
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
		public string SelectedDevice { get; set; }
		public DateTime StartDateTime { get; set; }
		public DateTime EndDateTime { get; set; }
		public ICommand GetDataCommand { get; private set; }
	}
}