// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FullContactExternalSearchProvider.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the FullContactExternalSearchProvider type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.Core.Data.Parts;
using CluedIn.Crawling.Helpers;
using CluedIn.ExternalSearch.Providers.FullContact.Model;
using CluedIn.ExternalSearch.Providers.FullContact.Vocabularies;
using RestSharp;

namespace CluedIn.ExternalSearch.Providers.FullContact
{
    /// <summary>The clear bit external search provider.</summary>
    /// <seealso cref="CluedIn.ExternalSearch.ExternalSearchProviderBase" />
    public class FullContactPhoneExternalSearchProvider : ExternalSearchProviderBase
    {
        /**********************************************************************************************************
         * CONSTRUCTORS
         **********************************************************************************************************/

        /// <summary>
        /// Initializes a new instance of the <see cref="FullContactPhoneExternalSearchProvider" /> class.
        /// </summary>
        public FullContactPhoneExternalSearchProvider()
            : base(Constants.ExternalSearchProviders.FullContactPhoneId, EntityType.Person, EntityType.Infrastructure.User, EntityType.Infrastructure.Contact)
        {
        }

        /**********************************************************************************************************
         * METHODS
         **********************************************************************************************************/

        /// <summary>Builds the queries.</summary>
        /// <param name="context">The context.</param>
        /// <param name="request">The request.</param>
        /// <returns>The search queries.</returns>
        public override IEnumerable<IExternalSearchQuery> BuildQueries(ExecutionContext context, IExternalSearchRequest request)
        {
            if (!this.Accepts(request.EntityMetaData.EntityType))
                yield break;

            var existingResults = request.GetQueryResults<Person>(this).ToList();

            Func<string, bool> domainFilter = value => existingResults.Any(r => string.Equals(r.Data.requestId, value, StringComparison.InvariantCultureIgnoreCase));

            // Query Input
            var entityType = request.EntityMetaData.EntityType;
            var phoneNumbers = request.QueryParameters.GetValue(CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInPerson.PhoneNumber, null);

            if (phoneNumbers != null)
            {
                var values = phoneNumbers.SelectMany(v => v.Split(new[] { ",", ";", "|" }, StringSplitOptions.RemoveEmptyEntries)).ToHashSet();

                foreach (var value in values.Where(v => !domainFilter(v)))
                    yield return new ExternalSearchQuery(this, entityType, ExternalSearchQueryParameter.Name, value);
            }
        }

        /// <summary>Executes the search.</summary>
        /// <param name="context">The context.</param>
        /// <param name="query">The query.</param>
        /// <returns>The results.</returns>
        public override IEnumerable<IExternalSearchQueryResult> ExecuteSearch(ExecutionContext context, IExternalSearchQuery query)
        {
            var name = query.QueryParameters[ExternalSearchQueryParameter.Name].FirstOrDefault();

            if (string.IsNullOrEmpty(name))
                yield break;

            var sharedApiToken = "19b2044e39f45d92"; // ConfigurationManager.AppSettings["Providers.ExternalSearch.FullContact.ApiToken"];

            var client = new RestClient("https://api.fullcontact.com");
            var request = new RestRequest(string.Format("/v2/person.json?phone={0}", name), Method.GET);
            request.AddHeader("X-FullContact-APIKey", sharedApiToken);

            var response = client.ExecuteTaskAsync<Person>(request).Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                yield return new ExternalSearchQueryResult<Person>(query, response.Data);
            }
            else if (response.StatusCode == HttpStatusCode.NoContent || response.StatusCode == HttpStatusCode.NotFound)
                yield break;
            else if (response.ErrorException != null)
                throw new AggregateException(response.ErrorException.Message, response.ErrorException);
            else
                throw new ApplicationException("Could not execute external search query - StatusCode:" + response.StatusCode);
        }

        /// <summary>Builds the clues.</summary>
        /// <param name="context">The context.</param>
        /// <param name="query">The query.</param>
        /// <param name="result">The result.</param>
        /// <param name="request">The request.</param>
        /// <returns>The clues.</returns>
        public override IEnumerable<Clue> BuildClues(ExecutionContext context, IExternalSearchQuery query, IExternalSearchQueryResult result, IExternalSearchRequest request)
        {
            var resultItem = result.As<Person>();

            var code = this.GetOriginEntityCode(resultItem);

            var clue = new Clue(code, context.Organization);
            clue.Data.OriginProviderDefinitionId = this.Id;

            this.PopulateMetadata(clue.Data.EntityData, resultItem);
            this.DownloadPreviewImage(context, resultItem.Data.photos.First().url, clue);

            return new[] { clue };
        }

        /// <summary>Gets the primary entity metadata.</summary>
        /// <param name="context">The context.</param>
        /// <param name="result">The result.</param>
        /// <param name="request">The request.</param>
        /// <returns>The primary entity metadata.</returns>
        public override IEntityMetadata GetPrimaryEntityMetadata(ExecutionContext context, IExternalSearchQueryResult result, IExternalSearchRequest request)
        {
            var resultItem = result.As<Person>();
            return this.CreateMetadata(resultItem);
        }

        /// <summary>Gets the preview image.</summary>
        /// <param name="context">The context.</param>
        /// <param name="result">The result.</param>
        /// <param name="request">The request.</param>
        /// <returns>The preview image.</returns>
        public override IPreviewImage GetPrimaryEntityPreviewImage(ExecutionContext context, IExternalSearchQueryResult result, IExternalSearchRequest request)
        {
            return this.DownloadPreviewImageBlob<Person>(context, result, r => r.Data.photos.First().url);
        }

        /// <summary>Creates the metadata.</summary>
        /// <param name="resultItem">The result item.</param>
        /// <returns>The metadata.</returns>
        private IEntityMetadata CreateMetadata(IExternalSearchQueryResult<Person> resultItem)
        {
            var metadata = new EntityMetadataPart();

            this.PopulateMetadata(metadata, resultItem);

            return metadata;
        }

        /// <summary>Gets the origin entity code.</summary>
        /// <param name="resultItem">The result item.</param>
        /// <returns>The origin entity code.</returns>
        private EntityCode GetOriginEntityCode(IExternalSearchQueryResult<Person> resultItem)
        {
            return new EntityCode(EntityType.Person, CodeOrigin.CluedIn.CreateSpecific("fullContact"), resultItem.Data.requestId);
        }

        /// <summary>Populates the metadata.</summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="resultItem">The result item.</param>
        private void PopulateMetadata(IEntityMetadata metadata, IExternalSearchQueryResult<Person> resultItem)
        {
            var code = this.GetOriginEntityCode(resultItem);

            metadata.EntityType = EntityType.Person;
            metadata.Name = resultItem.Data.contactInfo.fullName;
            metadata.OriginEntityCode = code;

            metadata.Codes.Add(code);
            metadata.Codes.Add(new EntityCode(EntityType.Person, CodeOrigin.CluedIn.CreateSpecific("email"), resultItem.Data.requestId));

            metadata.Properties[FullContactVocabulary.Person.FirstName] = resultItem.Data.contactInfo.givenName.PrintIfAvailable();
            metadata.Properties[FullContactVocabulary.Person.FullName] = resultItem.Data.contactInfo.fullName.PrintIfAvailable();
            metadata.Properties[FullContactVocabulary.Person.LastName] = resultItem.Data.contactInfo.familyName.PrintIfAvailable();
            metadata.Properties[FullContactVocabulary.Person.Websites] = string.Join(",", resultItem.Data.contactInfo.websites.Select(s => s.url));

            if (resultItem.Data.demographics != null)
            {
                metadata.Properties[FullContactVocabulary.Person.Age] = resultItem.Data.demographics.age.PrintIfAvailable();
                metadata.Properties[FullContactVocabulary.Person.AgeRange] = resultItem.Data.demographics.ageRange.PrintIfAvailable();
                metadata.Properties[FullContactVocabulary.Person.Gender] = resultItem.Data.demographics.gender.PrintIfAvailable();
                metadata.Properties[FullContactVocabulary.Person.LocationDeducedCity] = resultItem.Data.demographics.locationDeduced.city.name.PrintIfAvailable();
                metadata.Properties[FullContactVocabulary.Person.LocationDeducedContinent] = resultItem.Data.demographics.locationDeduced.continent.name.PrintIfAvailable();
                metadata.Properties[FullContactVocabulary.Person.LocationDeducedCountry] = resultItem.Data.demographics.locationDeduced.country.name.PrintIfAvailable();
                metadata.Properties[FullContactVocabulary.Person.LocationDeducedCounty] = resultItem.Data.demographics.locationDeduced.county.name.PrintIfAvailable();
                metadata.Properties[FullContactVocabulary.Person.LocationDeducedLocation] = resultItem.Data.demographics.locationDeduced.deducedLocation.PrintIfAvailable();
                metadata.Properties[FullContactVocabulary.Person.LocationDeducedLikelihood] = resultItem.Data.demographics.locationDeduced.likelihood.PrintIfAvailable().ToString();
                metadata.Properties[FullContactVocabulary.Person.LocationDeducedNormalized] = resultItem.Data.demographics.locationDeduced.normalizedLocation.PrintIfAvailable();
                metadata.Properties[FullContactVocabulary.Person.LocationDeducedState] = resultItem.Data.demographics.locationDeduced.state.name.PrintIfAvailable();
                metadata.Properties[FullContactVocabulary.Person.LocationGeneral] = resultItem.Data.demographics.locationGeneral.PrintIfAvailable();
            }

            if (resultItem.Data.digitalFootprint != null)
            {
                if (resultItem.Data.digitalFootprint.scores != null)
                    foreach (var score in resultItem.Data.digitalFootprint.scores)
                    {
                        metadata.Properties[string.Format("fullContact.person.scores.custom-{0}", score.provider)] = score.value.ToString();
                    }

                if (resultItem.Data.digitalFootprint.topics != null)
                    foreach (var topic in resultItem.Data.digitalFootprint.topics)
                    {
                        metadata.Tags.Add(new Tag(topic.value));
                    }
            }

            if (resultItem.Data.organizations != null)
                foreach (var organization in resultItem.Data.organizations)
                {
                    var from = new EntityReference(code);
                    var to = new EntityReference(new EntityCode(EntityType.Organization, CodeOrigin.CluedIn.CreateSpecific("fullContact"), organization.name));
                    var edge = new EntityEdge(from, to, EntityEdgeType.PartOf);
                    metadata.OutgoingEdges.Add(edge);
                }

            if (resultItem.Data.photos != null)
                foreach (var photo in resultItem.Data.photos)
                {
                    metadata.Properties[string.Format("fullContact.person.photo.custom-{0}", photo.typeId)] = photo.url;
                }

            if (resultItem.Data.socialProfiles != null)
                foreach (var socialProfile in resultItem.Data.socialProfiles)
                {
                    if (socialProfile.typeId == "twitter")
                    {
                        metadata.Properties[FullContactVocabulary.Person.Twitter] = socialProfile.url;
                    }
                    else if (socialProfile.typeId == "linkedin")
                    {
                        metadata.Properties[FullContactVocabulary.Person.LinkedIn] = socialProfile.url;
                    }
                    else if (socialProfile.typeId == "aboutme")
                    {
                        metadata.Properties[FullContactVocabulary.Person.AboutMe] = socialProfile.url;
                    }
                    else if (socialProfile.typeId == "quora")
                    {
                        metadata.Properties[FullContactVocabulary.Person.Quora] = socialProfile.url;
                    }
                    else if (socialProfile.typeId == "foursquare")
                    {
                        metadata.Properties[FullContactVocabulary.Person.FourSquare] = socialProfile.url;
                    }
                    else if (socialProfile.typeId == "youtube")
                    {
                        metadata.Properties[FullContactVocabulary.Person.YouTube] = socialProfile.url;
                    }
                    else if (socialProfile.typeId == "picasa")
                    {
                        metadata.Properties[FullContactVocabulary.Person.Picasa] = socialProfile.url;
                    }
                    else if (socialProfile.typeId == "plancast")
                    {
                        metadata.Properties[FullContactVocabulary.Person.PlanCast] = socialProfile.url;
                    }
                    else if (socialProfile.typeId == "googleplus")
                    {
                        metadata.Properties[FullContactVocabulary.Person.GooglePlus] = socialProfile.url;
                    }
                    else if (socialProfile.typeId == "klout")
                    {
                        metadata.Properties[FullContactVocabulary.Person.Klout] = socialProfile.url;
                    }
                    else if (socialProfile.typeId == "flickr")
                    {
                        metadata.Properties[FullContactVocabulary.Person.Flickr] = socialProfile.url;
                    }
                }

            metadata.Properties[FullContactVocabulary.Person.LikeliHood] = resultItem.Data.likelihood.PrintIfAvailable().ToString();
            metadata.Properties[FullContactVocabulary.Person.RequestId] = resultItem.Data.requestId.PrintIfAvailable();
            metadata.Properties[FullContactVocabulary.Person.Status] = resultItem.Data.status.PrintIfAvailable().ToString();

        }
    }
}
