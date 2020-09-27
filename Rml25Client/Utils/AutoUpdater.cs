using System;
using System.Threading;
using System.Threading.Tasks;

namespace Rml25Client
{
	class AutoUpdater
	{
		public event Action<string, DateTime, DateTime> DeviceDataRequest;

		private CancellationTokenSource _cancellation;

		public bool StopAutoUpdate()
		{
			if (_cancellation != null)
			{
				_cancellation.Cancel();
				_cancellation.Dispose();
				_cancellation = null;
				return true;
			}
			else
				return false;
		}

		public void StartAutoUpdate(string device, UpdateRate updateRate, UpdateScope updateScope)
		{
			StopAutoUpdate();
			_cancellation = new CancellationTokenSource();
			Task.Run(() => AutoUpdate(device, updateRate, updateScope, _cancellation.Token));
		}

		private async void AutoUpdate(string device, UpdateRate updateRate, UpdateScope updateScope, CancellationToken cancellationToken)
		{
			while (!cancellationToken.IsCancellationRequested)
			{
				var fromTime = DateTime.Now.Subtract(TimeSpan.FromHours((double)updateScope));
				var toTime = DateTime.Now;

				DeviceDataRequest?.Invoke(device, fromTime, toTime);

				await Task.Delay(TimeSpan.FromMinutes((double)updateRate));
			}
		}
	}
}
