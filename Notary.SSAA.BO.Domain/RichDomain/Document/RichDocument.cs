namespace Notary.SSAA.BO.Domain.RichDomain.Document
{
    using Notary.SSAA.BO.Domain.Entities;
    using Notary.SSAA.BO.SharedKernel.Enumerations;
    using System.Runtime.Serialization;
    using System.Text;
    using static Notary.SSAA.BO.SharedKernel.Constants.ExecutiveRequestConstant;
    using YesNo = Notary.SSAA.BO.SharedKernel.Enumerations.YesNo;
    using SharedKernel.Enumerations;
    using Notary.SSAA.BO.SharedKernel.Contracts.Coordinator.Document;
    using SharedKernel.Enumerations;
    /// <summary>
    /// Defines the <see cref="RichDocument" />
    /// </summary>
    public static class RichDocument
    {

        public static  string RegisterServiceReqTitle( this Domain.Entities.Document document)
        {
            if ( document.DocumentType != null )
            {
                return document.DocumentType.Title + " - شناسه یکتا ثبت: " + document.NationalNo + " - شماره ترتیب: " + document.ClassifyNo.ToString () + " - به تاریخ: " + document.DocumentDate;
            }
            return null; 
                
            
        }
        public static string Persons(this Domain.Entities.Document document )
        {
            if ( document.DocumentPeople != null )
            {


                string personsSpec = "";
                //#if SERVER_SIDE
                foreach ( DocumentPerson prs in document.DocumentPeople )
                {
                    if ( personsSpec != "" )
                        personsSpec += " - ";
                    personsSpec += prs.FullName();
                }
                //#endif
                return personsSpec;
            }
            return null;

            
        }
        public static decimal? SumPrices( this Domain.Entities.Document document )
        {
            if ( document.DocumentCosts != null )
            {

                long sumPrices = 0;

                foreach ( DocumentCost oneCost in document.DocumentCosts )
                    sumPrices += ( long ) oneCost.Price;

                return sumPrices;
            }
            return null;
        }

        public static string RelatedDocInfo ( this Domain.Entities.Document document )
        {
            
                string relDocInfo = "";

                if ( document.RelatedDocumentIsInSsar!=SharedKernel.Enumerations.YesNo.None.GetString() )
                {
                    relDocInfo =
                           ( ( document.RelatedDocumentType == null ) ? "" : document.RelatedDocumentType.Title ) + " " +
                           ( ( document.RelatedDocumentIsInSsar == SharedKernel.Enumerations.YesNo.Yes.GetString () ) ? "با شناسه " : "با شماره " ) + document.RelatedDocumentNo + " " +
                           "به تاریخ " + document.RelatedDocumentDate + " " +
                           "صادر شده توسط " + document.RelatedDocumentScriptorium;

                }

            foreach ( DocumentRelation relDoc in document.DocumentRelationDocuments )
            {
                if ( relDoc.RelatedDocumentIsInSsar != YesNo.None.GetString() )
                {
                    if ( !string.IsNullOrEmpty ( relDocInfo ) )
                        relDocInfo += "\n";

                    relDocInfo +=
                        ( ( relDoc.RelatedDocumentType == null ) ? "" : relDoc.RelatedDocumentType.Title ) + " " +
                        ( ( relDoc.RelatedDocumentIsInSsar == YesNo.Yes.GetString () ) ? "با شناسه " : "با شماره " ) + relDoc.RelatedDocumentNo + " " +
                        "به تاریخ " + relDoc.RelatedDocumentDate + " " +
                        "صادر شده توسط " + ( ( !string.IsNullOrEmpty ( relDoc.RelatedDocumentScriptorium ) ) ? relDoc.RelatedDocumentScriptorium : "");// to do list
                }
            }

                return relDocInfo;
       }
        

        public static string Cases ( this Domain.Entities.Document document )
        {
            string casesSpec = "";
            if ( document != null )
            {
                if ( document.HasCases () )
                {
                    if ( document.DocumentCases != null )
                    {
                        foreach ( var item in document.DocumentCases )
                        {
                            if ( casesSpec != "" )
                                casesSpec += " - ";
                            casesSpec += item.RegCaseText ();
                        }
                    }
                }
                else
            if ( document.HasEstates () )
                {
                    if ( document.DocumentEstates != null )
                    {
                        foreach ( var documentEstate in document.DocumentEstates )
                        {
                            if ( casesSpec != "" )
                                casesSpec += " - ";
                            casesSpec += documentEstate.RegCaseText();
                        }
                    }
                }
                else
            if ( document.HasVehicles () )
                {
                    if ( document.DocumentVehicles != null )
                    {
                        foreach ( var item in document.DocumentVehicles )
                        {
                            if ( casesSpec != "" )
                                casesSpec += " - ";
                            casesSpec += item.RegCaseText ();
                        }
                    }

                }
            }
            return casesSpec;
        }

        /// <summary>
        /// The HasVehicles
        /// </summary>
        /// <param name="document">The document<see cref="Domain.Entities.Document"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public static bool HasVehicles ( this Domain.Entities.Document document )
        {
            if ( document != null
                && document.DocumentType != null
                && document.DocumentType.HasAsset ==YesNo.Yes.GetString ()
                && document.DocumentType.WealthType ==WealthType.Linkages.GetString ()
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
        /// <param name="document">The document<see cref="Domain.Entities.Document"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public static bool HasCases ( this Domain.Entities.Document document )
        {
            if ( document != null
                && document.DocumentType != null
                && document.DocumentType.HasAsset ==YesNo.No.GetString ()
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
        /// <param name="document">The document<see cref="Domain.Entities.Document"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public static bool HasEstates ( this Domain.Entities.Document document )
        {
            if ( document != null
                && document.DocumentType != null
                && document.DocumentType.HasAsset ==YesNo.Yes.GetString ()
                && document.DocumentType.WealthType ==WealthType.Immovable.GetString ()

            )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsEditable(this Domain.Entities.Document document)
        {

            if(document!=null && (document.State==NotaryRegServiceReqState.Created.GetString() ||
                                  document.State==NotaryRegServiceReqState.WaitForInquiry.GetString() ||
                                   document.State==NotaryRegServiceReqState.CostCalculated.GetString() ||
                                   document.State==NotaryRegServiceReqState.CostPaid.GetString() ||
                                   document.State==NotaryRegServiceReqState.SetNationalDocumentNo.GetString() ||
                                   document.State==NotaryRegServiceReqState.FinalPrinted.GetString() 
                                                  ))
            {
                return true;

            }
            return false;
        }
        public static bool IsFinalSate ( this Domain.Entities.Document document )
        {

            if ( document != null && ( document.State == NotaryRegServiceReqState.Finalized.GetString() ||
                                       document.State == NotaryRegServiceReqState.CanceledAfterGetCode.GetString() ||
                                       document.State == NotaryRegServiceReqState.CanceledBeforeGetCode.GetString () 
                              
                ) )
            {
                return true;

            }
            return false;
        }
                public static bool CanNotCalculatePaperOverCost(this Domain.Entities.Document document)
        {

            // By: MNG - 93/01/25
            if (document.DocumentType.Code == "009" || document.DocumentType.Code == "007" ||    // صدور اجراييه
                document.DocumentType.Code == "0034")     // تهيه رونوشت از سند
                return true;

            else return false;
        }

    }

    /// <summary>
    /// Defines the <see cref="AnnotationPack" />
    /// </summary>

 
}
