using System;

namespace Rml25Client
{
	partial class ViewModel
	{
		private void GetDataFromDevice()
		{
			StopAutoUpdateIfAny();

			var dataChoiceValid = CheckDataChoiceValid(StartDateTime, EndDateTime);
			var deviceChoiceValid = CheckDeviceChoiceValid(SelectedDevice);

			if (!(dataChoiceValid && deviceChoiceValid))
				return;

			DeviceDataRequest?.Invoke(SelectedDevice, StartDateTime, EndDateTime);
		}

		private bool CheckDataChoiceValid(DateTime startDate, DateTime endDate)
		{
			var validStartEnd = DateTime.Compare(startDate, endDate) < 0;

			if (!validStartEnd)
				WarnUser(Constants.NOT_VALID_STARTTIME_ENDTIME);

			var validStartNow = DateTime.Compare(startDate, DateTime.Now) < 0;

			if (!validStartNow)
				WarnUser(Constants.NOT_VALID_STARTTIME_TIMENOW);

			return validStartEnd && validStartNow;
		}

		private bool CheckDeviceChoiceValid(string selectedDevice)
		{
			var valid = selectedDevice != Constants.NOT_SELECTED;

			if (!valid)
				WarnUser(Constants.NOT_VALID_DEVICE_CHOICE);

			return valid;
		}

		private void StartAutoUpdate()
		{
			StopAutoUpdateIfAny();

			var selectedDevice = SelectedDevice;
			var selectedRate = UpdateRates[SelectedUpdateRate];
			var selectedScope = UpdateScopes[SelectedUpdateScope];

			if (!CheckDeviceChoiceValid(selectedDevice))
				return;

			if (selectedRate == UpdateRate.NONE || selectedScope == UpdateScope.NONE)
			{
				WarnUser(Constants.NOT_VALID_UPDATE_CHOICE);
				return;
			}

			AutoUpdateRequest?.Invoke(selectedDevice, selectedRate, selectedScope);
			WarnUser(Constants.AUTOUPDATE_START);
		}

		private void StopAutoUpdateIfAny()
		{
			if (StopAutoUpdateRequest != null)
			{
				var updateStoppped = StopAutoUpdateRequest.Invoke();
				if (updateStoppped)
					WarnUser(Constants.AUTOUPDATE_END);
			}
		}
	}
}
