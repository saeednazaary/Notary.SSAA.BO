using Notary.SSAA.BO.SharedKernel.Enumerations;

namespace Notary.SSAA.BO.SharedKernel.Contracts.Coordinator.Document
{
    /// <summary>
    /// Defines the <see cref="RequsetType" />
    /// </summary>
    public class RequsetType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequsetType"/> class.
        /// </summary>
        /// <param name="id">The id<see cref="string"/></param>
        /// <param name="documentTypeGroup1Id">The documentTypeGroup1Id<see cref="string"/></param>
        /// <param name="documentTypeGroup2Id">The documentTypeGroup2Id<see cref="string"/></param>
        /// <param name="documentState">The documentState<see cref="string"/></param>
        /// <param name="title">The title<see cref="string"/></param>
        /// <param name="isSupportive">The isSupportive<see cref="string"/></param>
        /// <param name="hasAsset">The hasAsset<see cref="string"/></param>
        /// <param name="assetIsRequired">The assetIsRequired<see cref="string"/></param>
        /// <param name="wealthType">The wealthType<see cref="string"/></param>
        /// <param name="documentTextWritingAllowed">The documentTextWritingAllowed<see cref="string"/></param>
        /// <param name="hasRelatedDocument">The hasRelatedDocument<see cref="string"/></param>
        /// <param name="hasCount">The hasCount<see cref="string"/></param>
        /// <param name="hasSubject">The hasSubject<see cref="string"/></param>
        /// <param name="subjectIsRequired">The subjectIsRequired<see cref="string"/></param>
        /// <param name="hasEstateInquiry">The hasEstateInquiry<see cref="string"/></param>
        /// <param name="estateInquiryIsRequired">The estateInquiryIsRequired<see cref="string"/></param>
        /// <param name="hasNonregisteredEstate">The hasNonregisteredEstate<see cref="string"/></param>
        /// <param name="hasEstateAttachments">The hasEstateAttachments<see cref="string"/></param>
        /// <param name="hasAssetType">The hasAssetType<see cref="string"/></param>
        /// <param name="assetTypeIsRequired">The assetTypeIsRequired<see cref="string"/></param>
        /// <param name="generalPersonPostTitle">The generalPersonPostTitle<see cref="string"/></param>
        public RequsetType(string id, string documentTypeGroup1Id, string documentTypeGroup2Id, string documentState, string title, string isSupportive
            , string hasAsset, string assetIsRequired, string wealthType, string documentTextWritingAllowed, string hasRelatedDocument,
            string hasCount, string hasSubject, string subjectIsRequired, string hasEstateInquiry, string estateInquiryIsRequired,
            string hasNonregisteredEstate, string hasEstateAttachments, string hasAssetType, string assetTypeIsRequired, string generalPersonPostTitle)
        {

            Id = id;
            DocumentTypeGroup1Id = documentTypeGroup1Id;
            DocumentTypeGroup2Id = documentTypeGroup2Id;
            DocumentState = documentState;
            Title = title;
            IsSupportive = isSupportive;
            HasAsset = hasAsset;
            AssetIsRequired = assetIsRequired;
            WealthType = wealthType;
            DocumentTextWritingAllowed = documentTextWritingAllowed;
            HasRelatedDocument = hasRelatedDocument;
            HasCount = hasCount;
            HasSubject = hasSubject;
            SubjectIsRequired = subjectIsRequired;
            HasEstateInquiry = hasEstateInquiry;
            EstateInquiryIsRequired = estateInquiryIsRequired;
            HasNonregisteredEstate = hasNonregisteredEstate;
            HasEstateAttachments = hasEstateAttachments;
            HasAssetType = hasAssetType;
            AssetTypeIsRequired = assetTypeIsRequired;
            GeneralPersonPostTitle = generalPersonPostTitle;
        }

        public RequsetType(string id, string documentTypeGroup1Id, string documentTypeGroup2Id, string documentState, string title, string isSupportive, string hasAsset,
            string assetIsRequired, string wealthType, string documentTextWritingAllowed, string hasRelatedDocument, string hasCount, string hasSubject, string subjectIsRequired,
            string hasEstateInquiry, string estateInquiryIsRequired, string hasNonregisteredEstate, string hasEstateAttachments, string hasAssetType, string assetTypeIsRequired,
            string generalPersonPostTitle, string documentId) : this(id, documentTypeGroup1Id, documentTypeGroup2Id, documentState, title, isSupportive, hasAsset, assetIsRequired,
                wealthType, documentTextWritingAllowed, hasRelatedDocument, hasCount, hasSubject, subjectIsRequired, hasEstateInquiry, estateInquiryIsRequired, hasNonregisteredEstate,
                hasEstateAttachments, hasAssetType, assetTypeIsRequired, generalPersonPostTitle)
        {
            DocumentId = documentId;
        }

        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the DocumentTypeGroup1Id
        /// </summary>
        public string DocumentTypeGroup1Id { get; set; }

        /// <summary>
        /// Gets or sets the DocumentTypeGroup2Id
        /// </summary>
        public string DocumentTypeGroup2Id { get; set; }

        /// <summary>
        /// Gets or sets the DocumentState
        /// </summary>
        public string DocumentState { get; set; }

        /// <summary>
        /// Gets or sets the Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the IsSupportive
        /// </summary>
        public string IsSupportive { get; set; }

        /// <summary>
        /// Gets or sets the HasAsset
        /// </summary>
        public string HasAsset { get; set; }

        /// <summary>
        /// Gets or sets the AssetIsRequired
        /// </summary>
        public string AssetIsRequired { get; set; }

        /// <summary>
        /// Gets or sets the WealthType
        /// </summary>
        public string WealthType { get; set; }

        /// <summary>
        /// Gets or sets the DocumentTextWritingAllowed
        /// </summary>
        public string DocumentTextWritingAllowed { get; set; }

        /// <summary>
        /// Gets or sets the HasRelatedDocument
        /// </summary>
        public string HasRelatedDocument { get; set; }

        /// <summary>
        /// Gets or sets the HasCount
        /// </summary>
        public string HasCount { get; set; }

        /// <summary>
        /// Gets or sets the HasSubject
        /// </summary>
        public string HasSubject { get; set; }

        /// <summary>
        /// Gets or sets the SubjectIsRequired
        /// </summary>
        public string SubjectIsRequired { get; set; }

        /// <summary>
        /// Gets or sets the HasEstateInquiry
        /// </summary>
        public string HasEstateInquiry { get; set; }

        /// <summary>
        /// Gets or sets the EstateInquiryIsRequired
        /// </summary>
        public string EstateInquiryIsRequired { get; set; }

        /// <summary>
        /// Gets or sets the HasNonregisteredEstate
        /// </summary>
        public string HasNonregisteredEstate { get; set; }

        /// <summary>
        /// Gets or sets the HasEstateAttachments
        /// </summary>
        public string HasEstateAttachments { get; set; }

        /// <summary>
        /// Gets or sets the HasAssetType
        /// </summary>
        public string HasAssetType { get; set; }

        /// <summary>
        /// Gets or sets the AssetTypeIsRequired
        /// </summary>
        public string AssetTypeIsRequired { get; set; }

        /// <summary>
        /// Gets or sets the GeneralPersonPostTitle
        /// </summary>
        public string GeneralPersonPostTitle { get; set; }
        public string DocumentId { get; set; }


        public bool HasVehicles()
        {
            if (this != null
                && HasAsset == YesNo.Yes.GetString()
                && WealthType == Enumerations.WealthType.Linkages.GetString()
            )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// The HasCases
        /// </summary>
        /// <param name="requsetType">The requsetType<see cref="RequsetType"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public bool HasCases()
        {
            if (this != null
                && HasAsset == YesNo.No.GetString()
            )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// The HasEstates
        /// </summary>
        /// <param name="requsetType">The requsetType<see cref="RequsetType"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public bool HasEstates()
        {
            if (this != null
                && HasAsset == YesNo.Yes.GetString()
                && WealthType == Enumerations.WealthType.Immovable.GetString()

            )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }


}
