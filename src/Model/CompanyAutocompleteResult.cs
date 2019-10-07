// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompanyAutocompleteResult.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the CompanyAutocompleteResult type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Newtonsoft.Json;

namespace CluedIn.ExternalSearch.Providers.FullContact.Model
{
    /// <summary>The company autocomplete result.</summary>
    public class CompanyAutocompleteResult
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("logo")]
        public string Logo { get; set; }

        [JsonProperty("domain")]
        public string Domain { get; set; }
    }
}
