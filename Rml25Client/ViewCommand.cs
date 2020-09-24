using System;
using System.Windows.Input;

namespace Rml25Client
{
	class ViewCommand : ICommand
	{
		private Action Action;

		public ViewCommand(Action action)
		{
			this.Action = action;
		}

		public void Execute(object parameter)
		{
			Action?.Invoke();
		}

		//===========================================================
		//not used
		public event EventHandler CanExecuteChanged { add {/*Do nothing*/} remove {/*Do nothing*/} }
		public bool CanExecute(object parameter) => true;
	}
}
