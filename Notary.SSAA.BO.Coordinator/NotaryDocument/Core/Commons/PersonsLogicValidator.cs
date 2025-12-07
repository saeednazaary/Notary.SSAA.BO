namespace Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Commons
{
    using Notary.SSAA.BO.Domain.Entities;
    using Notary.SSAA.BO.Domain.RichDomain.Document;
    using Notary.SSAA.BO.SharedKernel.Enumerations;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Defines the <see cref="PersonsLogicValidator" />
    /// </summary>
    public class PersonsLogicValidator
    {
        /// <summary>
        /// The IsRelationsGraphSingleSide
        /// </summary>
        /// <param name="theOneDocPerson">The theOneDocPerson<see cref="DocumentPerson"/></param>
        /// <param name="theGraphIncludingPersonIDs">The theGraphIncludingPersonIDs<see cref="List{string}"/></param>
        /// <param name="relationsGraph">The relationsGraph<see cref="string"/></param>
        /// <param name="message">The message<see cref="string"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public static bool IsRelationsGraphSingleSide ( DocumentPerson theOneDocPerson, ref List<string>? theGraphIncludingPersonIDs, ref string? relationsGraph, ref string message )
        {
            string fullName = theOneDocPerson.FullName();

            if ( theOneDocPerson.Document.State == NotaryRegServiceReqState.FinalPrinted.GetString () || theOneDocPerson.IsRelated != YesNo.Yes.GetString () || theOneDocPerson.DocumentPersonRelatedAgentPeople == null || theOneDocPerson.DocumentPersonRelatedAgentPeople.Count == 0 )
                return true;

            if ( theGraphIncludingPersonIDs == null )
                theGraphIncludingPersonIDs = new List<string> ();

            if (  theGraphIncludingPersonIDs.Count != 0 && theGraphIncludingPersonIDs.Contains ( theOneDocPerson.Id.ToString () ) )
            {
                message =
                    "در تعریف اشخاص وابسته مربوط به " + theOneDocPerson.FullName() + " دقت فرمایید. در لیست اشخاص وابسته، حلقه تکرار بوجود آمده است." +
                    System.Environment.NewLine +
                    System.Environment.NewLine +
                    relationsGraph;

                return false;
            }

            theGraphIncludingPersonIDs.Add ( theOneDocPerson.Id.ToString () );

            foreach ( DocumentPersonRelated theOneDocAgent in theOneDocPerson.DocumentPersonRelatedAgentPeople )
            {
                if ( theGraphIncludingPersonIDs != null && theGraphIncludingPersonIDs.Count != 0 && theOneDocAgent.MainPerson != null )
                    relationsGraph += theGraphIncludingPersonIDs.Count + " - " + theOneDocPerson.FullName() + " ---" + theOneDocAgent.AgentType.Title + "---> " + theOneDocAgent.MainPerson.FullName () + System.Environment.NewLine;

                bool isSingleSide = IsRelationsGraphSingleSide(theOneDocAgent.MainPerson, ref theGraphIncludingPersonIDs, ref relationsGraph, ref message);
                if ( !isSingleSide )
                    return false;
            }

            theGraphIncludingPersonIDs = null;
            relationsGraph = null;
            return true;
        }

        /// <summary>
        /// The IsRelationsGraphSingleSideInDocReq
        /// </summary>
        /// <param name="theOneDocPerson">The theOneDocPerson<see cref="DocumentPerson"/></param>
        /// <param name="theGraphIncludingPersonIDs">The theGraphIncludingPersonIDs<see cref="List{string}"/></param>
        /// <param name="relationsGraph">The relationsGraph<see cref="string"/></param>
        /// <param name="message">The message<see cref="string"/></param>
        /// <returns>The <see cref="bool"/></returns>
        public static bool IsRelationsGraphSingleSideInDocReq ( DocumentPerson theOneDocPerson, ref List<string> theGraphIncludingPersonIDs, ref string relationsGraph, ref string message )
        {
            string fullName = theOneDocPerson.FullName();

            if ( theOneDocPerson.Document.State == DocReqState.Canceled.GetString () ||
                theOneDocPerson.Document.State == DocReqState.Sent.GetString () ||
                theOneDocPerson.IsRelated != YesNo.Yes.GetString () ||
                theOneDocPerson.DocumentPersonRelatedAgentPeople == null ||
                theOneDocPerson.DocumentPersonRelatedAgentPeople.Count == 0 )
                return true;

            if ( theGraphIncludingPersonIDs == null )
                theGraphIncludingPersonIDs = new List<string> ();

            if ( theGraphIncludingPersonIDs.Count != 0 && theGraphIncludingPersonIDs.Contains ( theOneDocPerson.Id.ToString () ) )
            {
                message =
                    "در تعریف اشخاص وابسته مربوط به " + theOneDocPerson.FullName() + " دقت فرمایید. در لیست اشخاص وابسته، حلقه تکرار بوجود آمده است." +
                    System.Environment.NewLine +
                    System.Environment.NewLine +
                    relationsGraph;

                return false;
            }

            theGraphIncludingPersonIDs.Add ( theOneDocPerson.Id.ToString () );

            foreach ( DocumentPersonRelated theOneDocAgent in theOneDocPerson.DocumentPersonRelatedAgentPeople )
            {
                if ( theGraphIncludingPersonIDs != null && theGraphIncludingPersonIDs.Count != 0 && theOneDocAgent.MainPerson != null )
                    relationsGraph += theGraphIncludingPersonIDs.Count + " - " + theOneDocPerson.FullName() + " ---" + theOneDocAgent.AgentType.Title + "---> " + theOneDocAgent.MainPerson.FullName () + System.Environment.NewLine;

                bool isSingleSide = IsRelationsGraphSingleSideInDocReq(theOneDocAgent.MainPerson, ref theGraphIncludingPersonIDs, ref relationsGraph, ref message);
                if ( !isSingleSide )
                    return false;
            }

            theGraphIncludingPersonIDs = null;
            relationsGraph = null;
            return true;
        }
    }

}
