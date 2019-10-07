namespace CluedIn.ExternalSearch.Providers.FullContact.Model
{
	public class LocationDeduced
	{
		public string normalizedLocation { get; set; }
		public string deducedLocation { get; set; }
		public City city { get; set; }
		public State state { get; set; }
		public Country country { get; set; }
		public Continent continent { get; set; }
		public County county { get; set; }
		public double likelihood { get; set; }
	}
}