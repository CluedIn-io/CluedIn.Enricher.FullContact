using System.Collections.Generic;

namespace CluedIn.ExternalSearch.Providers.FullContact.Model
{
	public class ContactInfo
	{
		public string familyName { get; set; }
		public string givenName { get; set; }
		public string fullName { get; set; }
		public List<Website> websites { get; set; }
	}
}