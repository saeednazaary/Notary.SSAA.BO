using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;
using Notary.SSAA.BO.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Estate.Core
{
    public class ExportManager
    {
        internal string GenerateXML ( List<DSUDealSummaryObject> inputObject )
        {
            string generatedString = string.Empty;

            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<DSUDealSummaryObject>));
            using ( System.IO.MemoryStream memStream = new System.IO.MemoryStream () )
            {
                serializer.Serialize ( memStream, inputObject );
                memStream.Seek ( 0, System.IO.SeekOrigin.Begin );

                var reader = new System.IO.StreamReader(memStream);
                generatedString = reader.ReadToEnd ();
            }

            return generatedString;
        }

        internal string GenerateXML ( RestrictionDealSummaryListItem inputObject )
        {
            string generatedString = string.Empty;

            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(RestrictionDealSummaryListItem));
            using ( System.IO.MemoryStream memStream = new System.IO.MemoryStream () )
            {
                serializer.Serialize ( memStream, inputObject );
                memStream.Seek ( 0, System.IO.SeekOrigin.Begin );

                var reader = new System.IO.StreamReader(memStream);
                generatedString = reader.ReadToEnd ();
            }

            return generatedString;
        }

        public List<DSUDealSummaryObject> ExportDSUObjectFromXML ( string xml )
        {
            List<DSUDealSummaryObject> castedObject = null;

            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<DSUDealSummaryObject>));
            var stringReader = new System.IO.StringReader(xml.GetStandardFarsiString(false));
            castedObject = serializer.Deserialize ( stringReader ) as List<DSUDealSummaryObject>;

            return castedObject;
        }

        public DSUDealSummaryObject ExportIndividualDSUObjectFromXML ( string xml )
        {
            DSUDealSummaryObject castedObject = null;

            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(DSUDealSummaryObject));
            var stringReader = new System.IO.StringReader(xml.GetStandardFarsiString(false));
            castedObject = serializer.Deserialize ( stringReader ) as DSUDealSummaryObject;

            return castedObject;
        }
    }

}
