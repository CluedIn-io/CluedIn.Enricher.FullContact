namespace CluedIn.ExternalSearch.Providers.FullContact.Model
{
	public class Address
	{
		public string addressLine1 { get; set; }
		public string addressLine2 { get; set; }
		public string locality { get; set; }
		public Region region { get; set; }
		public Country country { get; set; }
		public string postalCode { get; set; }
		public string label { get; set; }
	}
}