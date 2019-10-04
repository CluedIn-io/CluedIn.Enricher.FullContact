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
using CluedIn.ExternalSearch.Providers.Fullcontact.Model;
using CluedIn.ExternalSearch.Providers.Fullcontact.Vocabularies;
using RestSharp;

namespace CluedIn.ExternalSearch.Providers.Fullcontact
{
    /// <summary>The clear bit external search provider.</summary>
    /// <seealso cref="CluedIn.ExternalSearch.ExternalSearchProviderBase" />
    public class FullContactCompanyDomainExternalSearchProvider : ExternalSearchProviderBase
    {
        /**********************************************************************************************************
         * CONSTRUCTORS
         **********************************************************************************************************/

        /// <summary>
        /// Initializes a new instance of the <see cref="FullContactCompanyDomainExternalSearchProvider" /> class.
        /// </summary>
        public FullContactCompanyDomainExternalSearchProvider()
            : base(Constants.ExternalSearchProviders.FullContactCompanyDomainId, EntityType.Organization)
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

            var existingResults = request.GetQueryResults<Company>(this).ToList();

            Func<string, bool> domainFilter = value => existingResults.Any(r => string.Equals(r.Data.requestId, value, StringComparison.InvariantCultureIgnoreCase));

            // Query Input
            var entityType = request.EntityMetaData.EntityType;
            var website = request.QueryParameters.GetValue(CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.Website, null);
            var emailDomainNames = request.QueryParameters.GetValue(CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.EmailDomainNames, null);

            if (website != null)
            {
                var values = website;

                foreach (var value in values)
                {
                    Uri uri;

                    if (Uri.TryCreate(value, UriKind.Absolute, out uri) && !domainFilter(uri.Host))
                        yield return new ExternalSearchQuery(this, entityType, ExternalSearchQueryParameter.Name, uri.Host);
                    else if (!domainFilter(value))
                        yield return new ExternalSearchQuery(this, entityType, ExternalSearchQueryParameter.Name, value);
                }
            }
            else if (emailDomainNames != null)
            {
                var values = emailDomainNames.SelectMany(v => v.Split(new[] { ",", ";", "|" }, StringSplitOptions.RemoveEmptyEntries)).ToHashSet();

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
            var request = new RestRequest(string.Format("/v2/company/lookup.json?domain={0}", name), Method.GET);
            request.AddHeader("X-FullContact-APIKey", sharedApiToken);

            var response = client.ExecuteTaskAsync<Company>(request).Result;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                yield return new ExternalSearchQueryResult<Company>(query, response.Data);
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
            var resultItem = result.As<Company>();

            var code = this.GetOriginEntityCode(resultItem);

            var clue = new Clue(code, context.Organization);
            clue.Data.OriginProviderDefinitionId = this.Id;

            this.PopulateMetadata(clue.Data.EntityData, resultItem);
            this.DownloadPreviewImage(context, resultItem.Data.logo, clue);

            return new[] { clue };
        }

        /// <summary>Gets the primary entity metadata.</summary>
        /// <param name="context">The context.</param>
        /// <param name="result">The result.</param>
        /// <param name="request">The request.</param>
        /// <returns>The primary entity metadata.</returns>
        public override IEntityMetadata GetPrimaryEntityMetadata(ExecutionContext context, IExternalSearchQueryResult result, IExternalSearchRequest request)
        {
            var resultItem = result.As<Company>();
            return this.CreateMetadata(resultItem);
        }

        /// <summary>Gets the preview image.</summary>
        /// <param name="context">The context.</param>
        /// <param name="result">The result.</param>
        /// <param name="request">The request.</param>
        /// <returns>The preview image.</returns>
        public override IPreviewImage GetPrimaryEntityPreviewImage(ExecutionContext context, IExternalSearchQueryResult result, IExternalSearchRequest request)
        {
            return this.DownloadPreviewImageBlob<Company>(context, result, r => r.Data.logo);
        }

        /// <summary>Creates the metadata.</summary>
        /// <param name="resultItem">The result item.</param>
        /// <returns>The metadata.</returns>
        private IEntityMetadata CreateMetadata(IExternalSearchQueryResult<Company> resultItem)
        {
            var metadata = new EntityMetadataPart();

            this.PopulateMetadata(metadata, resultItem);

            return metadata;
        }

        /// <summary>Gets the origin entity code.</summary>
        /// <param name="resultItem">The result item.</param>
        /// <returns>The origin entity code.</returns>
        private EntityCode GetOriginEntityCode(IExternalSearchQueryResult<Company> resultItem)
        {
            return new EntityCode(EntityType.Organization, CodeOrigin.CluedIn.CreateSpecific("fullContact"), resultItem.Data.requestId);
        }

        /// <summary>Populates the metadata.</summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="resultItem">The result item.</param>
        private void PopulateMetadata(IEntityMetadata metadata, IExternalSearchQueryResult<Company> resultItem)
        {
            var code = this.GetOriginEntityCode(resultItem);

            metadata.EntityType = EntityType.Organization;
            metadata.Name = resultItem.Data.organization.name;
            metadata.OriginEntityCode = code;

            metadata.Codes.Add(code);

            if (resultItem.Data.category != null) metadata.Tags.AddRange(resultItem.Data.category.Select(s => new Tag(s.name)));

            metadata.Properties[FullContactVocabulary.Organization.Current] = resultItem.Data.organization.current.PrintIfAvailable().ToString();
            metadata.Properties[FullContactVocabulary.Organization.EndDate] = resultItem.Data.organization.endDate.PrintIfAvailable().ToString();
            metadata.Properties[FullContactVocabulary.Organization.IsPrimary] = resultItem.Data.organization.isPrimary.PrintIfAvailable().ToString();
            metadata.Properties[FullContactVocabulary.Organization.StartDate] = resultItem.Data.organization.startDate.PrintIfAvailable().ToString();
            metadata.Properties[FullContactVocabulary.Organization.Title] = resultItem.Data.organization.title.PrintIfAvailable().ToString();
            metadata.Properties[FullContactVocabulary.Organization.Status] = resultItem.Data.status.PrintIfAvailable().ToString();

            if (resultItem.Data.traffic.ranking != null)
                foreach (var ranking in resultItem.Data.traffic.ranking)
                {
                    metadata.Properties[string.Format("fullContact.organization.trafficRanking.custom-{0}", ranking.locale)] = ranking.rank.ToString();
                }

            if (resultItem.Data.traffic.topCountryRanking != null)
                foreach (var ranking in resultItem.Data.traffic.topCountryRanking)
                {
                    metadata.Properties[string.Format("fullContact.organization.topCountryRanking.custom-{0}", ranking.locale)] = ranking.rank.ToString();
                }

            metadata.Properties[FullContactVocabulary.Organization.Website] = resultItem.Data.website.ToString();

            if (resultItem.Data.socialProfiles != null)
                foreach (var socialProfile in resultItem.Data.socialProfiles)
                {
                    if (socialProfile.typeId == "twitter")
                    {
                        metadata.Properties[FullContactVocabulary.Organization.Twitter] = socialProfile.url;
                    }
                    else if (socialProfile.typeId == "linkedin")
                    {
                        metadata.Properties[FullContactVocabulary.Organization.LinkedIn] = socialProfile.url;
                    }
                    else if (socialProfile.typeId == "aboutme")
                    {
                        metadata.Properties[FullContactVocabulary.Organization.AboutMe] = socialProfile.url;
                    }
                    else if (socialProfile.typeId == "quora")
                    {
                        metadata.Properties[FullContactVocabulary.Organization.Quora] = socialProfile.url;
                    }
                    else if (socialProfile.typeId == "foursquare")
                    {
                        metadata.Properties[FullContactVocabulary.Organization.FourSquare] = socialProfile.url;
                    }
                    else if (socialProfile.typeId == "youtube")
                    {
                        metadata.Properties[FullContactVocabulary.Organization.YouTube] = socialProfile.url;
                    }
                    else if (socialProfile.typeId == "picasa")
                    {
                        metadata.Properties[FullContactVocabulary.Organization.Picasa] = socialProfile.url;
                    }
                    else if (socialProfile.typeId == "plancast")
                    {
                        metadata.Properties[FullContactVocabulary.Organization.PlanCast] = socialProfile.url;
                    }
                    else if (socialProfile.typeId == "googleplus")
                    {
                        metadata.Properties[FullContactVocabulary.Organization.GooglePlus] = socialProfile.url;
                    }
                    else if (socialProfile.typeId == "klout")
                    {
                        metadata.Properties[FullContactVocabulary.Organization.Klout] = socialProfile.url;
                    }
                    else if (socialProfile.typeId == "flickr")
                    {
                        metadata.Properties[FullContactVocabulary.Organization.Flickr] = socialProfile.url;
                    }
                }
        }
    }
}
