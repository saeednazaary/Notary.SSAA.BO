using Notary.SSAA.BO.SharedKernel.Enumerations;
using Notary.SSAA.BO.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Notary.SSAA.BO.DataTransferObject.Coordinators.Estate;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.EstateInquiry;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using DocumentEstate = Notary.SSAA.BO.Domain.Entities.DocumentEstate;
using MediatR;
using Notary.SSAA.BO.SharedKernel.Result;
using Stimulsoft.Svg.FilterEffects;
using System.Runtime.Serialization;
using Notary.SSAA.BO.DataTransferObject.Queries.Estate.BaseInfoService;
using System.Xml.Linq;
using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices.Estate;

namespace Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Estate.Core
{
    public class SeperationDealSummaryEngineCore

    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IDateTimeService _dateTimeService;

        private readonly IRepository<DocumentEstateDealSummarySeparation>
            _documentEstateDealSummarySeparationRepository;

        private readonly IMediator _mediator;
        private readonly IUserService _userService;

        public SeperationDealSummaryEngineCore(IDocumentRepository documentRepository,
            IRepository<DocumentEstateDealSummarySeparation> documentEstateDealSummarySeparationRepository,
            IDateTimeService dateTimeService, IMediator mediator, IUserService userService)
        {
            _documentRepository = documentRepository;
            _documentEstateDealSummarySeparationRepository = documentEstateDealSummarySeparationRepository;
            _dateTimeService = dateTimeService;
            _mediator = mediator;
            _userService = userService;
        }

        public async Task<SetEstateSeparationElementsOwnershipQuery> CreateSeparationDSPack(string theCurrentReqId,
            CancellationToken cancellationToken)
        {
            Document theReq = await _documentRepository.GetDocumentById(Guid.Parse(theCurrentReqId),
                new List<string>() { "DocumentEstates", "DocumentPeople" }, cancellationToken);




            SetEstateSeparationElementsOwnershipQuery SetElementMainObject =
                new SetEstateSeparationElementsOwnershipQuery();
            SetElementMainObject.TheElementOwnershipInfoList = new List<ElementOwnershipInfo>();
            SetElementMainObject.TheElementsRelationInfoList = new List<ElementsRelationInfo>();
            List<ElementOwnershipInfo> ElementList = new List<ElementOwnershipInfo>();
            List<ElementsRelationInfo> RelationList = new List<ElementsRelationInfo>();

            foreach (DocumentEstate theOneRegCase in theReq.DocumentEstates)
            {
                SetElementMainObject.DocumentNo = theReq.NationalNo;
                SetElementMainObject.DocumentDate = theReq.DocumentDate;
                SetElementMainObject.NotaryOfficeCmsId = theReq.ScriptoriumId;

                foreach (DocumentEstateSeparationPiece theOnePiece in theOneRegCase.DocumentEstateSeparationPieces)
                {
                    if (theOnePiece.HasOwner == YesNo.No.GetString())
                    {
                        if (theOnePiece.DocumentEstateSeparationPieceKindId == "1" ||
                            theOnePiece.DocumentEstateSeparationPieceKindId == "2")
                        {
                            foreach (var theOneQuota in theOnePiece.DocumentEstateSeparationPiecesQuota)
                            {
                                ElementOwnershipInfo ElementOwner = new ElementOwnershipInfo();
                                ElementsRelationInfo ElementRelation = new ElementsRelationInfo();

                                //Owners
                                ElementOwner.OwnerBirthDate = theOneQuota.DocumentPerson.BirthDate;
                                ElementOwner.OwnerFatherName = theOneQuota.DocumentPerson.FatherName;
                                ElementOwner.OwnerFirstName = theOneQuota.DocumentPerson.Name;
                                ElementOwner.OwnerLastName = theOneQuota.DocumentPerson.Family;
                                ElementOwner.OwnerIdentityNo =
                                    (theOneQuota.DocumentPerson.PersonType == PersonType.NaturalPerson.GetString())
                                        ? theOneQuota.DocumentPerson.IdentityNo
                                        : theOneQuota.DocumentPerson.CompanyRegisterNo;
                                ElementOwner.OwnerNationalityCode = theOneQuota.DocumentPerson.NationalNo;
                                ElementOwner.PersonType = (theOneQuota.DocumentPerson.PersonType ==
                                                           PersonType.NaturalPerson.GetString())
                                    ? 1
                                    : 2;
                                ElementOwner.Sex = theOneQuota.DocumentPerson.SexType == SexType.Male.GetString()
                                    ? 2
                                    : 1;
                                ElementOwner.SharePart = (decimal)theOneQuota.DetailQuota;
                                ElementOwner.ShareTotal = (decimal)theOneQuota.TotalQuota;
                                ElementOwner.ShareText = theOneQuota.QuotaText;
                                ElementOwner.TextBetting = theOneQuota.DealSummaryPersonConditions;
                                ElementOwner.TheElementInfo = new SeparationElement();
                                ElementOwner.TheElementInfo = SetElementInfo(theOnePiece);

                                ElementOwner.IssueLocationId =
                                    theOneQuota.DocumentPerson.IdentityIssueGeoLocationId?.ToString();
                                if (theOneQuota.DocumentPerson.PersonType == PersonType.NaturalPerson.GetString() &&
                                    string.IsNullOrEmpty(ElementOwner.IssueLocationId))
                                {
                                    var response = await _mediator.Send(new GetGeolocationByNationalityCodeQuery(theOneQuota.DocumentPerson.NationalNo), cancellationToken);
                                    if (response.IsSuccess)
                                    {
                                        ElementOwner.IssueLocationId = response.Data.Geolocation.Id;
                                    }
                                    else
                                    {
                                        ElementOwner.IssueLocationId = null;
                                    }



                                }

                                ElementOwner.EMailAddress = theOneQuota.DocumentPerson.Email;
                                ElementOwner.MobileNumber4SMS = theOneQuota.DocumentPerson.MobileNo;
                                ElementOwner.NationalityId =
                                    (string.IsNullOrEmpty(theOneQuota.DocumentPerson.NationalityId?.ToString()))
                                        ? "0024000010000001"
                                        : theOneQuota.DocumentPerson.NationalityId.ToString();
                                ElementOwner.PostCode = theOneQuota.DocumentPerson.PostalCode;
                                ElementOwner.Address = theOneQuota.DocumentPerson.Address;

                                SetElementMainObject.TheElementOwnershipInfoList.Add(ElementOwner);
                            }

                            //Relations
                            List<ElementsRelationInfo> RList = new List<ElementsRelationInfo>();

                            //Anbari
                            foreach (DocumentEstateSeparationPiece theOneAnbar in theOnePiece.InverseAnbari)
                            {
                                ElementsRelationInfo Anbar = new ElementsRelationInfo();

                                Anbar.ChildElement = SetRelationInfo(theOneAnbar, 1);
                                Anbar.ParentElement = SetRelationInfo(theOnePiece, 2);
                                SetElementMainObject.TheElementsRelationInfoList.Add(Anbar);
                            }

                            //Parking
                            foreach (DocumentEstateSeparationPiece theOneParking in theOnePiece.InverseParking)
                            {
                                ElementsRelationInfo Parking = new ElementsRelationInfo();

                                Parking.ChildElement = SetRelationInfo(theOneParking, 1);
                                Parking.ParentElement = SetRelationInfo(theOnePiece, 2);
                                SetElementMainObject.TheElementsRelationInfoList.Add(Parking);
                            }

                            string[] inqID = theOneRegCase.EstateInquiryId.Split(',');
                            SetElementMainObject.OtherRelatedInquiryList = new List<string>();
                            foreach (string oneID in inqID)
                                SetElementMainObject.OtherRelatedInquiryList.Add(oneID);
                            SetElementMainObject.InquiryId = inqID[0];
                        }
                    }
                }
            }

            return SetElementMainObject;
        }

        private static SeparationElement SetElementInfo(DocumentEstateSeparationPiece thePiece)
        {
            SeparationElement Element = new SeparationElement();

            Element.Area = thePiece.Area == null ? null : Decimal.Parse(thePiece.Area);
            Element.AreaUnitId = thePiece.MeasurementUnitTypeId;
            Element.Class = thePiece.Floor;
            Element.EastLimit = thePiece.EasternLimits;
            Element.WestLimit = thePiece.WesternLimits;
            Element.NorthLimit = thePiece.NorthLimits;
            Element.SouthLimit = thePiece.SouthLimits;
            Element.EKindPieceCode = PieceKindRapper(thePiece.DocumentEstateSeparationPieceKindId);
            Element.EstateHightLaw = thePiece.Rights;
            Element.HasOwnership = thePiece.HasOwner == YesNo.Yes.GetString() ? true : false;
            Element.PlaqueOriginal = thePiece.BasicPlaque;
            Element.OtherDescription = thePiece.OtherAttachments + Environment.NewLine + thePiece.Description;
            Element.SectionId = thePiece.EstateSectionId;
            Element.Sector = thePiece.PieceNo;
            Element.Separate = thePiece.DividedFromBasicPlaque; //thePiece.DivFromSecondaryPlaque;
            Element.Side = thePiece.Direction;
            Element.SidewayPlaque = thePiece.SecondaryPlaque;
            Element.SubSectionId = thePiece.EstateSubsectionId;
            Element.UnitId = thePiece.UnitId;
            Element.EPieceTypeId = thePiece.EstatePieceTypeId;

            return Element;
        }

        private static SeparationElement SetRelationInfo(DocumentEstateSeparationPiece thePiece, int RelationType)
        {
            SeparationElement Relation = new SeparationElement();
                Relation.Area = thePiece.Area == null ? null : Decimal.Parse(thePiece.Area);
                Relation.AreaUnitId = thePiece.MeasurementUnitTypeId;
                Relation.Class = thePiece.Floor;
                Relation.EastLimit = thePiece.EasternLimits;
                Relation.WestLimit = thePiece.WesternLimits;
                Relation.NorthLimit = thePiece.NorthLimits;
                Relation.SouthLimit = thePiece.SouthLimits;
                Relation.EKindPieceCode = PieceKindRapper(thePiece.DocumentEstateSeparationPieceKindId);
                Relation.EstateHightLaw = thePiece.Rights;
                Relation.HasOwnership = thePiece.HasOwner == YesNo.Yes.GetString() ? true : false;
                Relation.PlaqueOriginal = thePiece.BasicPlaque;
                Relation.OtherDescription = thePiece.OtherAttachments + Environment.NewLine + thePiece.Description;
                Relation.SectionId = thePiece.EstateSectionId;
                Relation.Sector = thePiece.PieceNo;
                Relation.Separate = thePiece.DividedFromSecondaryPlaque;
                Relation.Side = thePiece.Direction;
                Relation.SidewayPlaque = thePiece.SecondaryPlaque;
                Relation.SubSectionId = thePiece.EstateSubsectionId;
                Relation.UnitId = thePiece.UnitId;
                Relation.EPieceTypeId = thePiece.EstatePieceTypeId;


            return Relation;
        }

        private static string PieceKindRapper(string Code)
        {
            switch (Code)
            {
                case "1":
                case "2":
                    return "3";
                case "3":
                    return "5";
                case "4":
                    return "4";
                case "5":
                    return "3.";
                case "6":
                    return "2";
                default:
                    return "3";
            }
        }

        internal static string GenerateXML(SetEstateSeparationElementsOwnershipQuery inputObject)
        {
            string generatedString = string.Empty;

            System.Xml.Serialization.XmlSerializer serializer =
                new System.Xml.Serialization.XmlSerializer(typeof(SetEstateSeparationElementsOwnershipQuery));
            using (System.IO.MemoryStream memStream = new System.IO.MemoryStream())
            {
                serializer.Serialize(memStream, inputObject);
                memStream.Seek(0, System.IO.SeekOrigin.Begin);

                var reader = new System.IO.StreamReader(memStream);
                generatedString = reader.ReadToEnd();
            }

            return generatedString;
        }

        public async Task<string> InsertXMLLog(SetEstateSeparationElementsOwnershipQuery SDSPack, string CurrentReqId,
            CancellationToken cancellationToken)
        {
            if (SDSPack != null)
            {
                try
                {

                    var SDSLog =
                        await _documentEstateDealSummarySeparationRepository.GetAsync(
                            t => t.DocumentId == Guid.Parse(CurrentReqId), cancellationToken);
                    if (SDSLog != null)
                    {
                        string xml = GenerateXML(SDSPack);
                        string modifiedXML = xml.GetStandardFarsiString(false); // GetStandardFarsiString(xml);

                        SDSLog.Xml = modifiedXML;

                        SDSLog.CreateDate = _dateTimeService.CurrentPersianDate;
                        SDSLog.CreateTime = _dateTimeService.CurrentTime;
                        SDSLog.GeneratedDealSummaryCount = (short)SDSPack.TheElementOwnershipInfoList.Count;
                        SDSLog.IsSent = NotaryGeneratedDealSummary.NotSent.GetString();
                        SDSLog.DocumentId = Guid.Parse(CurrentReqId);
                        await _documentEstateDealSummarySeparationRepository.UpdateAsync(SDSLog, cancellationToken, false);
                        return SDSLog.Xml;
                    }
                    else
                    {
                        SDSLog = new DocumentEstateDealSummarySeparation();
                        SDSLog.Id = Guid.NewGuid();
                        SDSLog.Ilm = "1";
                        SDSLog.ScriptoriumId = _userService.UserApplicationContext.ScriptoriumInformation.Id;

                        string xml = GenerateXML(SDSPack);
                        string modifiedXML = xml.GetStandardFarsiString(false); // GetStandardFarsiString(xml);

                        SDSLog.Xml = modifiedXML;
                        SDSLog.CreateDate = _dateTimeService.CurrentPersianDate;
                        SDSLog.CreateTime = _dateTimeService.CurrentTime;
                        SDSLog.GeneratedDealSummaryCount = (short)SDSPack.TheElementOwnershipInfoList.Count;
                        SDSLog.IsSent = NotaryGeneratedDealSummary.NotSent.GetString();
                        SDSLog.DocumentId = Guid.Parse(CurrentReqId);
                        if (SDSLog.SendAttemptCount == null || SDSLog.SendAttemptCount == 0)
                            SDSLog.SendAttemptCount = 0;
                        await _documentEstateDealSummarySeparationRepository.AddAsync(SDSLog, cancellationToken, false);

                        return SDSLog.Xml;
                    }

                }
                catch (Exception ex)
                {
                    return null;
                }
            }

            return null;
        }

        public DSUDealSummarySignPacket ProvideSignRecord(SetEstateSeparationElementsOwnershipQuery SDSForSign,
            string ONotaryReqId)
        {
            //NotaryOfficeImplementation.CustomClassesDefinitions.SeparationDealSummarySignPack singleSignPacket = new NotaryOfficeImplementation.CustomClassesDefinitions.SeparationDealSummarySignPack() ;
            DSUDealSummarySignPacket singleSignPacket = new DSUDealSummarySignPacket();

            var serializer = new DataContractSerializer(typeof(SetEstateSeparationElementsOwnershipQuery));
            byte[] rawDataByteArray;

            using (var ms = new MemoryStream())
            {
                serializer.WriteObject(ms, SDSForSign);
                rawDataByteArray = ms.ToArray();
            }

            // Deserialization
            using (var ms = new MemoryStream(rawDataByteArray))
            {
                var deserializedObject = serializer.ReadObject(ms);
            }



            //BinaryFormatter bf = new ();
            //byte[] rawDataByteArray = null;

            //using ( MemoryStream ms = new MemoryStream () )
            //{
            //    bf.Serialize ( ms, SDSForSign );
            //    rawDataByteArray = ms.ToArray ();
            //}



            //MemoryStream memStream = new MemoryStream();
            //BinaryFormatter binForm = new BinaryFormatter();
            //memStream.Write(rawDataByteArray, 0, rawDataByteArray.Length);
            //memStream.Seek(0, SeekOrigin.Begin);
            //Object obj = (Object)binForm.Deserialize(memStream);

            //if (rawDataByteArray == null)
            //    return null;

            singleSignPacket.RawDataByteArray = rawDataByteArray;
            singleSignPacket.RawDataB64 = System.Convert.ToBase64String(singleSignPacket.RawDataByteArray);
            byte[] standardizedByte = System.Text.Encoding.Unicode.GetBytes(singleSignPacket.RawDataB64);
            singleSignPacket.RawDataB64 = System.Convert.ToBase64String(standardizedByte);
            //singleSignPacket.FailedOldDeterministicDSU = failedOldDSU;
            singleSignPacket.RegisterServiceReqObjectID = ONotaryReqId;

            return singleSignPacket;
        }

        public SetEstateSeparationElementsOwnershipQuery ExtractDealPackFromXML(string Xml)
        {
            SetEstateSeparationElementsOwnershipQuery ExtractedPack = new SetEstateSeparationElementsOwnershipQuery();

            System.Xml.Serialization.XmlSerializer serializer =
                new System.Xml.Serialization.XmlSerializer(typeof(SetEstateSeparationElementsOwnershipQuery));
            var stringReader = new System.IO.StringReader(Xml);

            ExtractedPack = serializer.Deserialize(stringReader) as SetEstateSeparationElementsOwnershipQuery;
            return ExtractedPack;
        }

        public async Task<ApiResult> SendSeparationDSPack(Document theCurrentReq,
            List<DSUDealSummarySignPacket> SignedPacket, CancellationToken cancellationToken)
        {
            try
            {

                //SeparationServiceProxy.SetElementsOwnershipInfo FinalElement = CreateSeparationDSPack(theCurrentReq.Id);

                string SavedXML = await FindXMlRecordInDB(theCurrentReq.Id.ToString(), cancellationToken);

                SetEstateSeparationElementsOwnershipQuery FinalElement = ExtractDealPackFromXML(SavedXML);

                FinalElement.DigitalSignatureOfData = SignedPacket[0].SignB64;
                FinalElement.Certificate = SignedPacket[0].CertificateB64;

                await InsertXMLLog(FinalElement, theCurrentReq.Id.ToString(), cancellationToken);



                var sendingResponse = await _mediator.Send(FinalElement, cancellationToken);



                if (sendingResponse.IsSuccess)
                {



                    var SDSLog =
                        await _documentEstateDealSummarySeparationRepository.GetAsync(
                            t => t.DocumentId == theCurrentReq.Id, cancellationToken);

                    if (SDSLog != null)
                    {
                        SDSLog.SendDate = _dateTimeService.CurrentPersianDate;
                        SDSLog.SendTime = _dateTimeService.CurrentTime;
                        SDSLog.IsSent = NotaryGeneratedDealSummary.Sent.GetString();

                    }

                }

                return sendingResponse;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private async Task<string> FindXMlRecordInDB(string RegisterServiceReqId, CancellationToken cancellationToken)
        {
            {
                var SDSLog =
                    await _documentEstateDealSummarySeparationRepository.GetAsync(
                        t => t.DocumentId == Guid.Parse(RegisterServiceReqId), cancellationToken);

                if (SDSLog != null)
                {
                    return SDSLog.Xml;
                }

                return null;
            }

        }



    }
}
