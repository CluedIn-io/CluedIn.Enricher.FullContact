﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FullContactOrganizationVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the FullContactOrganizationVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.ExternalSearch.Providers.FullContact.Vocabularies
{
    /// <summary>The clear bit organization vocabulary.</summary>
    /// <seealso cref="CluedIn.Core.Data.Vocabularies.SimpleVocabulary" />
    public class FullContactPersonVocabulary : SimpleVocabulary
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FullContactPersonVocabulary"/> class.
        /// </summary>
        public FullContactPersonVocabulary()
        {
            this.VocabularyName = "FullContact Autocomplete Person";
            this.KeyPrefix      = "FullContact.person";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.Person;

            this.Domain         = this.Add(new VocabularyKey("domain", VocabularyKeyDataType.Uri));
            this.Logo           = this.Add(new VocabularyKey("logo", VocabularyKeyDataType.Uri, VocabularyKeyVisibility.Hidden));

            this.FirstName = this.Add(new VocabularyKey("name"));
            this.LastName = this.Add(new VocabularyKey("lastName"));
            this.Avatar = this.Add(new VocabularyKey("avatar"));
            this.Bio = this.Add(new VocabularyKey("bio"));
            this.Email = this.Add(new VocabularyKey("email"));
            this.IndexedAt = this.Add(new VocabularyKey("indexedAt"));
            this.Location = this.Add(new VocabularyKey("location"));
            this.Site = this.Add(new VocabularyKey("site"));
            this.TimeZone = this.Add(new VocabularyKey("timeZone"));
            this.UtcOffset = this.Add(new VocabularyKey("utcOffset"));
            this.AboutMe = this.Add(new VocabularyKey("aboutMe"));
            this.EmailProvider = this.Add(new VocabularyKey("emailProvider"));
            this.Employment = this.Add(new VocabularyKey("employment"));
            this.Facebook = this.Add(new VocabularyKey("facebook"));
            this.Fuzzy = this.Add(new VocabularyKey("fuzzy"));
            this.Gender = this.Add(new VocabularyKey("gender"));
            this.Geo = this.Add(new VocabularyKey("geo"));
            this.Github = this.Add(new VocabularyKey("github"));
            this.GooglePlus = this.Add(new VocabularyKey("googlePlus"));
            this.Gravatar = this.Add(new VocabularyKey("gravatar"));
            this.LinkedIn = this.Add(new VocabularyKey("linkedIn"));
            this.Twitter = this.Add(new VocabularyKey("twitter"));
            this.AboutMeBio = this.Add(new VocabularyKey("aboutMeBio"));
            this.AboutMeAvatar = this.Add(new VocabularyKey("aboutMeAvatar"));
            this.EmploymentDomain = this.Add(new VocabularyKey("employmentDomain"));
            this.EmploymentRole = this.Add(new VocabularyKey("employmentRole"));
            this.EmploymentSeniority = this.Add(new VocabularyKey("employmentSeniority"));
            this.EmploymentTitle = this.Add(new VocabularyKey("employmentTitle"));
            this.City = this.Add(new VocabularyKey("city"));
            this.Country = this.Add(new VocabularyKey("country"));
            this.CountryCode = this.Add(new VocabularyKey("countryCode"));
            this.Lat = this.Add(new VocabularyKey("lat"));
            this.Long = this.Add(new VocabularyKey("long"));
            this.State = this.Add(new VocabularyKey("state"));
            this.StateCode = this.Add(new VocabularyKey("stateCode"));
            this.GithubAvatar = this.Add(new VocabularyKey("githubAvatar"));
            this.GithubBlog = this.Add(new VocabularyKey("githubBlog"));
            this.GithubCompany = this.Add(new VocabularyKey("githubCompany"));
            this.GithubFollowers = this.Add(new VocabularyKey("githubFollowers"));
            this.GithubFollowing = this.Add(new VocabularyKey("githubFollowing"));
            this.GithubHandle = this.Add(new VocabularyKey("githubHandle"));
            this.GravatarAvatar = this.Add(new VocabularyKey("gravatarAvatar"));
            this.GravatarAvatars = this.Add(new VocabularyKey("gravatarAvatars"));
            this.TwitterAvatar = this.Add(new VocabularyKey("twitterAvatar"));
            this.GravatarUrls = this.Add(new VocabularyKey("GravatarsUrls"));
            this.TwitterBio = this.Add(new VocabularyKey("twitterBio"));
            this.TwitterFavourites = this.Add(new VocabularyKey("twitterFavourites"));
            this.TwitterFollowers = this.Add(new VocabularyKey("twitterFollowers"));
            this.TwitterFollowing = this.Add(new VocabularyKey("twitterFollowing"));
            this.TwitterHandle = this.Add(new VocabularyKey("twitterHandle"));
            this.TwitterLocation = this.Add(new VocabularyKey("twitterLocation"));
            this.TwitterSite = this.Add(new VocabularyKey("twitterSite"));
            this.TwitterStatuses = this.Add(new VocabularyKey("twitterStatuses"));

            this.AddMapping(this.City, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInPerson.HomeAddressCity);
            this.AddMapping(this.CountryCode, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInPerson.HomeAddressCountryCode);
            this.AddMapping(this.Email, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInPerson.Email);
            this.AddMapping(this.FirstName, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInPerson.FirstName);
            this.AddMapping(this.Gender, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInPerson.Gender);
            this.AddMapping(this.LastName, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInPerson.LastName);
            this.AddMapping(this.State, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInPerson.HomeAddressState);
            this.AddMapping(this.TimeZone, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInUser.TimeZone);

            this.AddMapping(this.Domain, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.Website);


            this.Websites = this.Add(new VocabularyKey("Websites"));
            this.Quora = this.Add(new VocabularyKey("Quora"));
            this.FourSquare = this.Add(new VocabularyKey("FourSquare"));
            this.YouTube = this.Add(new VocabularyKey("YouTube"));
            this.Picasa = this.Add(new VocabularyKey("Picasa"));
            this.PlanCast = this.Add(new VocabularyKey("PlanCast"));
            this.Klout = this.Add(new VocabularyKey("Klout"));
            this.Flickr = this.Add(new VocabularyKey("Flickr"));
            this.LikeliHood = this.Add(new VocabularyKey("LikeliHood"));
            this.RequestId = this.Add(new VocabularyKey("RequestId"));
            this.Status = this.Add(new VocabularyKey("Status"));
            this.FullName = this.Add(new VocabularyKey("FullName"));
            this.Age = this.Add(new VocabularyKey("Age"));
            this.AgeRange = this.Add(new VocabularyKey("AgeRange"));
            this.LocationDeducedState = this.Add(new VocabularyKey("LocationDeducedState"));
            this.LocationGeneral = this.Add(new VocabularyKey("LocationGeneral"));
            this.LocationDeducedCity = this.Add(new VocabularyKey("LocationDeducedCity"));
            this.LocationDeducedContinent = this.Add(new VocabularyKey("LocationDeducedContinent"));
            this.LocationDeducedCountry = this.Add(new VocabularyKey("LocationDeducedCountry"));
            this.LocationDeducedCounty = this.Add(new VocabularyKey("LocationDeducedCounty"));
            this.LocationDeducedLocation = this.Add(new VocabularyKey("LocationDeducedLocation"));
            this.LocationDeducedLikelihood = this.Add(new VocabularyKey("LocationDeducedLikelihood"));
            this.LocationDeducedNormalized = this.Add(new VocabularyKey("LocationDeducedNormalized"));
        }


        public VocabularyKey Websites { get; internal set; }
        public VocabularyKey Quora { get; internal set; }
        public VocabularyKey FourSquare { get; internal set; }
        public VocabularyKey YouTube { get; internal set; }
        public VocabularyKey Picasa { get; internal set; }
        public VocabularyKey PlanCast { get; internal set; }
        public VocabularyKey Klout { get; internal set; }
        public VocabularyKey Flickr { get; internal set; }
        public VocabularyKey LikeliHood { get; internal set; }
        public VocabularyKey RequestId { get; internal set; }
        public VocabularyKey Status { get; internal set; }
        public VocabularyKey FullName { get; internal set; }
        public VocabularyKey Age { get; internal set; }
        public VocabularyKey AgeRange { get; internal set; }
        public VocabularyKey LocationDeducedState { get; internal set; }
        public VocabularyKey LocationGeneral { get; internal set; }
        public VocabularyKey LocationDeducedCity { get; internal set; }
        public VocabularyKey LocationDeducedContinent { get; internal set; }
        public VocabularyKey LocationDeducedCountry { get; internal set; }
        public VocabularyKey LocationDeducedCounty { get; internal set; }
        public VocabularyKey LocationDeducedLocation { get; internal set; }
        public VocabularyKey LocationDeducedLikelihood { get; internal set; }
        public VocabularyKey LocationDeducedNormalized { get; internal set; }

        public VocabularyKey Domain { get; protected set; }
        public VocabularyKey Logo { get; protected set; }
        public VocabularyKey FirstName { get; set; }
        public VocabularyKey LastName { get; set; }
        public VocabularyKey Avatar { get; set; }
        public VocabularyKey Bio { get; set; }
        public VocabularyKey Email { get; set; }
        public VocabularyKey IndexedAt { get; set; }
        public VocabularyKey Location { get; set; }
        public VocabularyKey Site { get; set; }
        public VocabularyKey TimeZone { get; set; }
        public VocabularyKey UtcOffset { get; set; }
        public VocabularyKey AboutMe { get; set; }
        public VocabularyKey EmailProvider { get; set; }
        public VocabularyKey Employment { get; set; }
        public VocabularyKey Facebook { get; set; }
        public VocabularyKey Fuzzy { get; set; }
        public VocabularyKey Gender { get; set; }
        public VocabularyKey Geo { get; set; }
        public VocabularyKey Github { get; set; }
        public VocabularyKey GooglePlus { get; set; }
        public VocabularyKey Gravatar { get; set; }
        public VocabularyKey LinkedIn { get; set; }
        public VocabularyKey Twitter { get; set; }
        public VocabularyKey AboutMeBio { get; set; }
        public VocabularyKey AboutMeAvatar { get; set; }
        public VocabularyKey EmploymentDomain { get; set; }
        public VocabularyKey EmploymentRole { get; set; }
        public VocabularyKey EmploymentSeniority { get; set; }
        public VocabularyKey EmploymentTitle { get; set; }
        public VocabularyKey City { get; set; }
        public VocabularyKey Country { get; set; }
        public VocabularyKey CountryCode { get; set; }
        public VocabularyKey Lat { get; set; }
        public VocabularyKey Long { get; set; }
        public VocabularyKey State { get; set; }
        public VocabularyKey StateCode { get; set; }
        public VocabularyKey GithubAvatar { get; set; }
        public VocabularyKey GithubBlog { get; set; }
        public VocabularyKey GithubCompany { get; set; }
        public VocabularyKey GithubFollowers { get; set; }
        public VocabularyKey GithubFollowing { get; set; }
        public VocabularyKey GithubHandle { get; set; }
        public VocabularyKey GravatarAvatar { get; set; }
        public VocabularyKey GravatarAvatars { get; set; }
        public VocabularyKey TwitterAvatar { get; set; }
        public VocabularyKey GravatarUrls { get; set; }
        public VocabularyKey TwitterBio { get; set; }
        public VocabularyKey TwitterFavourites { get; set; }
        public VocabularyKey TwitterFollowers { get; set; }
        public VocabularyKey TwitterFollowing { get; set; }
        public VocabularyKey TwitterHandle { get; set; }
        public VocabularyKey TwitterLocation { get; set; }
        public VocabularyKey TwitterSite { get; set; }
        public VocabularyKey TwitterStatuses { get; set; }


    }
}
