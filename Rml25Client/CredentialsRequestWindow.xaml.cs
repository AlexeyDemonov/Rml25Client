using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Rml25Client
{
	/// <summary>
	/// Interaction logic for CredentialsRequestWindow.xaml
	/// </summary>
	public partial class CredentialsRequestWindow : Window, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private void RaisePropertyChange([CallerMemberName] string propertyname = default(string))
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
		}

		public string Address { get; set; }
		public string Port { get; set; }
		public string Login { get; set; }

		public CredentialsRequestWindow()
		{
			InitializeComponent();

			Address = Constants.DEFAULT_ADDRESS;
			Port = Constants.DEFAULT_PORT;

			this.DataContext = this;
		}

		public void GetCredentials(out string address, out string port, out string login, out string password)
		{
			address = Address;
			port = Port;
			login = Login;
			password = this.passwordBox.Password;
		}

		private void OKButtonClick(object sender, RoutedEventArgs e)
		{
			var valid = !string.IsNullOrEmpty(Address) && !string.IsNullOrEmpty(Port);
			
			if (valid)
			{
				this.DialogResult = true;
				this.Close();
			}
		}
	}
}
