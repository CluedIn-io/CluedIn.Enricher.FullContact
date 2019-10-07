// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FullContactVocabulary.cs" company="Clued In">
//   Copyright Clued In
// </copyright>
// <summary>
//   Defines the FullContactVocabulary type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CluedIn.ExternalSearch.Providers.FullContact.Vocabularies
{
    /// <summary>The clear bit vocabulary.</summary>
    public static class FullContactVocabulary
    {
        /// <summary>
        /// Initializes static members of the <see cref="FullContactVocabulary" /> class.
        /// </summary>
        static FullContactVocabulary()
        {
            Organization    = new FullContactOrganizationVocabulary();
            Person    = new FullContactPersonVocabulary();
        }

        /// <summary>Gets the organization.</summary>
        /// <value>The organization.</value>
        public static FullContactOrganizationVocabulary Organization { get; private set; }
        public static FullContactPersonVocabulary Person { get; private set; }
    }
}