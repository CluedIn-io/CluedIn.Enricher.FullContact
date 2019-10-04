// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FullContactOrganizationVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the FullContactOrganizationVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.ExternalSearch.Providers.Fullcontact.Vocabularies
{
    /// <summary>The clear bit organization vocabulary.</summary>
    /// <seealso cref="CluedIn.Core.Data.Vocabularies.SimpleVocabulary" />
    public class FullContactOrganizationVocabulary : SimpleVocabulary
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FullContactOrganizationVocabulary"/> class.
        /// </summary>
        public FullContactOrganizationVocabulary()
        {
            this.VocabularyName = "FullContact Autocomplete Organization";
            this.KeyPrefix = "FullContact.organization";
            this.KeySeparator = ".";
            this.Grouping = EntityType.Organization;

            this.Domain = this.Add(new VocabularyKey("domain", VocabularyKeyDataType.Uri));
            this.Logo = this.Add(new VocabularyKey("logo", VocabularyKeyDataType.Uri, VocabularyKeyVisibility.Hidden));
            this.Current = this.Add(new VocabularyKey("current", VocabularyKeyDataType.Boolean));
            this.EndDate = this.Add(new VocabularyKey("endDate", VocabularyKeyDataType.DateTime));
            this.IsPrimary = this.Add(new VocabularyKey("isPrimary", VocabularyKeyDataType.Boolean));
            this.StartDate = this.Add(new VocabularyKey("startDate", VocabularyKeyDataType.DateTime));
            this.Title = this.Add(new VocabularyKey("title", VocabularyKeyDataType.Text));
            this.Status = this.Add(new VocabularyKey("status", VocabularyKeyDataType.Text));
            this.Website = this.Add(new VocabularyKey("website", VocabularyKeyDataType.Uri));


            this.Twitter = this.Add(new VocabularyKey("twitter", VocabularyKeyDataType.Uri));
            this.LinkedIn = this.Add(new VocabularyKey("linkedIn", VocabularyKeyDataType.Uri));
            this.AboutMe = this.Add(new VocabularyKey("aboutMe", VocabularyKeyDataType.Uri));
            this.Quora = this.Add(new VocabularyKey("quora", VocabularyKeyDataType.Uri));
            this.FourSquare = this.Add(new VocabularyKey("fourSquare", VocabularyKeyDataType.Uri));
            this.YouTube = this.Add(new VocabularyKey("youTube", VocabularyKeyDataType.Uri));
            this.Picasa = this.Add(new VocabularyKey("picasa", VocabularyKeyDataType.Uri));
            this.PlanCast = this.Add(new VocabularyKey("planCast", VocabularyKeyDataType.Uri));
            this.GooglePlus = this.Add(new VocabularyKey("googlePlus", VocabularyKeyDataType.Uri));
            this.Klout = this.Add(new VocabularyKey("klout", VocabularyKeyDataType.Uri));
            this.Flickr = this.Add(new VocabularyKey("flickr", VocabularyKeyDataType.Uri));

            this.AddMapping(this.Domain, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.Website);
            this.AddMapping(this.Twitter, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.Social.Twitter);
            this.AddMapping(this.LinkedIn, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.Social.AboutMe);
            this.AddMapping(this.AboutMe, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.Social.LinkedIn);
            this.AddMapping(this.Quora, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.Social.Quora);
            this.AddMapping(this.FourSquare, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.Social.FourSquare);
            this.AddMapping(this.YouTube, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.Social.YouTube);
            this.AddMapping(this.Picasa, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.Social.Picasa);
            this.AddMapping(this.PlanCast, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.Social.PlanCast);
            this.AddMapping(this.GooglePlus, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.Social.GooglePlus);
            this.AddMapping(this.Klout, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.Social.Klout);
            this.AddMapping(this.Flickr, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.Social.Flickr);
        }

        public VocabularyKey Domain { get; protected set; }
        public VocabularyKey Logo { get; protected set; }
        public VocabularyKey Current { get; internal set; }
        public VocabularyKey EndDate { get; internal set; }
        public VocabularyKey IsPrimary { get; internal set; }
        public VocabularyKey StartDate { get; internal set; }
        public VocabularyKey Title { get; internal set; }
        public VocabularyKey Status { get; internal set; }
        public VocabularyKey Website { get; internal set; }
        public VocabularyKey Twitter { get; internal set; }
        public VocabularyKey LinkedIn { get; internal set; }
        public VocabularyKey AboutMe { get; internal set; }
        public VocabularyKey Quora { get; internal set; }
        public VocabularyKey FourSquare { get; internal set; }
        public VocabularyKey YouTube { get; internal set; }
        public VocabularyKey Picasa { get; internal set; }
        public VocabularyKey PlanCast { get; internal set; }
        public VocabularyKey GooglePlus { get; internal set; }
        public VocabularyKey Klout { get; internal set; }
        public VocabularyKey Flickr { get; internal set; }
    }
}
