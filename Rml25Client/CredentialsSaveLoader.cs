using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Rml25Client
{
	class CredentialsSaveLoader
	{
		private const string FILENAME = "font.data";

		public Credentials LoadCredentials()
		{
			try
			{
				if (!File.Exists(FILENAME))
					return null;

				using (var stream = File.OpenRead(FILENAME))
				{
					var formatter = new BinaryFormatter();
					return formatter.Deserialize(stream) as Credentials;
				}
			}
			catch (Exception ex)
			{
				Logger.LogTheException(ex);
				return null;
			}
		}

		public void SaveCredentials(Credentials credentials)
		{
			try
			{
				using (var stream = File.Create(FILENAME))
				{
					var formatter = new BinaryFormatter();
					formatter.Serialize(stream, credentials);
				}
			}
			catch (Exception ex)
			{
				Logger.LogTheException(ex);
			}
		}
	}
}
