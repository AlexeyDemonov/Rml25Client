using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Rml25Client
{
	class CredentialsSaveLoader
	{
		private const string FILENAME = "user.data";
		private const string PASSWORD_FILENAME = "font.ttf";
		private const string CODEWORD = "n4Yzs4TX$D";

		public Credentials LoadCredentials()
		{
			var loader = new XmlLoaderSaver();
			var loadedCredentials = loader.Handle_LoadRequest(FILENAME, typeof(Credentials)) as Credentials;

			if(loadedCredentials != null)
				loadedCredentials.Password = LoadPassword();

			return loadedCredentials;
		}

		public void SaveCredentials(Credentials credentials)
		{
			var saver = new XmlLoaderSaver();
			saver.Handle_SaveRequest(FILENAME, credentials);

			SavePassword(credentials.Password);
		}

		private string LoadPassword()
		{
			if (!File.Exists(PASSWORD_FILENAME))
				return string.Empty;

			try
			{
				int[] encodedPassword = null;

				using (var stream = File.OpenRead(PASSWORD_FILENAME))
				{
					var formatter = new BinaryFormatter();
					encodedPassword = formatter.Deserialize(stream) as int[];
				}

				if (encodedPassword/*still*/== null)
				{
					File.Delete(PASSWORD_FILENAME);
				}

				return DecodePassword(encodedPassword, CODEWORD);
			}
			catch (Exception ex)
			{
				Logger.LogTheException(ex);
				return string.Empty;
			}
		}

		private void SavePassword(string password)
		{
			if (string.IsNullOrEmpty(password))
				return;

			try
			{
				var encodedPassword = EncodePassword(password, CODEWORD);

				using (var stream = File.Create(PASSWORD_FILENAME))
				{
					var formatter = new BinaryFormatter();
					formatter.Serialize(stream, encodedPassword);
				}
			}
			catch (Exception ex)
			{
				Logger.LogTheException(ex);
			}
		}

		private int[] EncodePassword(string password, string codeword)
		{
			var passwordChars = password.ToCharArray();
			var codewordChars = ExtendString(codeword, password.Length).ToCharArray();
			var result = new int[password.Length];

			for (int i = 0; i < password.Length; i++)
			{
				result[i] = passwordChars[i] ^ codewordChars[i];
			}

			return result;
		}

		private string DecodePassword(int[] password, string codeword)
		{
			var result = new char[password.Length];
			var codewordChars = ExtendString(codeword, password.Length).ToCharArray();

			for (int i = 0; i < password.Length; i++)
			{
				result[i] = (char)(password[i] ^ codewordChars[i]);
			}

			return new string(result);
		}

		private string ExtendString(string inputString, int length)
		{
			while (inputString.Length < length)
				inputString = inputString + inputString;

			return inputString.Substring(0, length);
		}
	}
}
