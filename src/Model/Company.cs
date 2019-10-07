using System.Collections.Generic;

namespace CluedIn.ExternalSearch.Providers.FullContact.Model
{
	public class Company
	{
		public int status { get; set; }
		public string requestId { get; set; }
		public List<Category> category { get; set; }
		public string logo { get; set; }
		public string website { get; set; }
		public string languageLocale { get; set; }
		public Organization organization { get; set; }
		public List<SocialProfile> socialProfiles { get; set; }
		public Traffic traffic { get; set; }
	}
}