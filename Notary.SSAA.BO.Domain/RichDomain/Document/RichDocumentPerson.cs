using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.Enumerations;
namespace Notary.SSAA.BO.Domain.RichDomain.Document
{
    /// <summary>
    /// Defines the <see cref="documentPerson" />
    /// </summary>
    public static class documentPerson
    {
        /// <summary>
        /// The FullName
        /// </summary>
        /// <param name="documentPerson">The documentPerson<see cref="Domain.Entities.DocumentPerson"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static string FullName ( this Domain.Entities.DocumentPerson documentPerson )
        {
            if ( documentPerson != null )
            {
                if ( documentPerson.PersonType == PersonType.NaturalPerson.GetString()  )
                {
                    string fName = documentPerson.Name + ' ' + documentPerson.Family;
                    return fName;
                }
                else
                {
                    return documentPerson.Name;
                }

            }
            return null;
        }
        public static bool IsMartyrExistsInhaghSabHalfPercent(this ICollection<DocumentPerson> documentPeople)
        {
            bool isMartyrExists = false;
            foreach (DocumentPerson oneDocPerson in documentPeople)
            {
                if (oneDocPerson.IsMartyrApplicant == YesNo.Yes.GetString() &&
                    oneDocPerson.IsMartyrIncluded == YesNo.Yes.GetString())
                {
                    isMartyrExists = true;
                }
            }
            return isMartyrExists;
        }
        public static bool IsMartyrExistsInhaghSabt(this ICollection<DocumentPerson> documentPeople)
        {
            bool isMartyrExists = false;
            foreach (DocumentPerson oneDocPerson in documentPeople)
            {
                if (oneDocPerson.IsMartyrIncluded == YesNo.Yes.GetString())
                {
                    isMartyrExists = true;
                }
            }
            return isMartyrExists;
        }
        public static bool IsTheBuyerAlawyerTheSeller(this DocumentPerson person, DocumentPersonRelated agent)
        {

            bool result = false;

            // آیا خریدار وکیل فروشنده است؟
            if (person.DocumentPersonType != null && agent.MainPerson != null)
            {
                if (agent.MainPerson.DocumentPersonType != null)
                {

                    if ((person.DocumentPersonType.Code == "2" || person.DocumentPersonType.Code == "20") &&    // خریدار یا متصالح
                        (agent.MainPerson.DocumentPersonType.Code == "1" || agent.MainPerson.DocumentPersonType.Code == "19") &&     // فروشنده یا مصالح
                       agent.AgentType.Code == "1")     // وکیل
                        result = true;
                }

            }
            return result;

        }
    }
}
