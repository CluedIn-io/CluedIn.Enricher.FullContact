using System.Collections.Generic;

namespace CluedIn.ExternalSearch.Providers.FullContact.Model
{
	public class Person
	{
		public int status { get; set; }
		public double likelihood { get; set; }
		public string requestId { get; set; }
		public ContactInfo contactInfo { get; set; }
		public Demographics demographics { get; set; }
		public List<SocialProfile> socialProfiles { get; set; }
		public List<Organization> organizations { get; set; }
		public DigitalFootprint digitalFootprint { get; set; }
		public List<Photo> photos { get; set; }
	}
}