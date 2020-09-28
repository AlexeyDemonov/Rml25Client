namespace Rml25Client
{
	static class Constants
	{
		public const string TITLE = "RML 25 Клиент";
		public const string VERSION = "1.0.4";

		public const string TABLE_TITLE_TEMPLATE = "Датчик: {0} (Батарея:{1}В Сигнал:{2})";

		public const string DEFAULT_ADDRESS = "127.0.0.1";
		public const string DEFAULT_PORT = "3306";
		public const string DATABASE_NAME = "workdb";

		public const string REFUSED_ENTER = "Для работы приложения необходимо ввести данные и нажать кнопку 'Подключиться'";

		public const string NOT_SELECTED = "----";

		public const string ERROR_CAPTION = "Ошибка";

		public const string NOT_VALID_STARTTIME_ENDTIME = "Время начала отчёта не может быть больше времени конца отчёта";
		public const string NOT_VALID_STARTTIME_TIMENOW = "Время начала отчёта не может быть больше текущего времени";
		public const string NOT_VALID_DEVICE_CHOICE = "Выберите датчик";
		public const string NOT_VALID_UPDATE_CHOICE = "Выберите период и масштаб автообновления данных";
		public const string AUTOUPDATE_START = "Автообновление запущено";
		public const string AUTOUPDATE_END = "Автообновление остановлено";
	}
}
