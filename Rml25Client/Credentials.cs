using System;
using System.Xml.Serialization;

namespace Rml25Client
{
	[XmlRoot("Credentials")]
	public class Credentials
	{
		[XmlElement]
		public string Address;
		[XmlElement]
		public string Port;
		[XmlElement]
		public string Login;
		[XmlIgnore]
		public string Password;
	}
}
