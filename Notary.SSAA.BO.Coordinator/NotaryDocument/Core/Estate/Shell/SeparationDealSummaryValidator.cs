using Notary.SSAA.BO.SharedKernel.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notary.SSAA.BO.DataTransferObject.Coordinators.Estate;
using Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Estate.Core;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices.Estate;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Estate.Shell
{
    public class SeparationDealSummaryValidator
    {
        private readonly SeperationDealSummaryEngineCore _coreseperationDealSummaryEngineCore;
        private IRepository<DocumentEstateDealSummarySeparation> _documentEstateDealSummarySeparationRepository;

        public SeparationDealSummaryValidator(SeperationDealSummaryEngineCore coreseperationDealSummaryEngineCore, IRepository<DocumentEstateDealSummarySeparation> documentEstateDealSummarySeparationRepository)
        {
            _coreseperationDealSummaryEngineCore = coreseperationDealSummaryEngineCore;
            _documentEstateDealSummarySeparationRepository = documentEstateDealSummarySeparationRepository;
        }

        public async Task<(DSUDealSummarySignPacket, string, string)> PrepareSeparationPacksForSign(string ONotaryRegisterReqId, CancellationToken cancellationToken, string GeneratedMessage, string SeparationDSCount)
        {
            string GeneratedMessageRef = GeneratedMessage;
            string SeparationDSCountRef = SeparationDSCount;
            try
            {
                //NotaryOfficeImplementation.CustomClassesDefinitions.SeparationDealSummarySignPack SPacksForSign = new NotaryOfficeImplementation.CustomClassesDefinitions.SeparationDealSummarySignPack();
                DSUDealSummarySignPacket SPackForSign = new DSUDealSummarySignPacket();

                SetEstateSeparationElementsOwnershipQuery MainPack = await _coreseperationDealSummaryEngineCore.CreateSeparationDSPack(ONotaryRegisterReqId, cancellationToken);
                if (MainPack.TheElementOwnershipInfoList != null && MainPack.TheElementOwnershipInfoList.Any())
                {
                    SeparationDSCountRef = MainPack.TheElementOwnershipInfoList.Count.ToString();
                }
                else
                {
                    SeparationDSCountRef = "0";
                    GeneratedMessageRef = "خلاصه معامله های مورد نیاز برای ارسال تولید نشد. لطفاً مجدداً تلاش نمایید.";
                    return (null, GeneratedMessageRef, SeparationDSCountRef);
                }



                //create XML from MainPack
                //string MainPackXML = EstateInquiryManager.EngineCore.SeperationDealSummaryEngineCore.GenerateXML(MainPack);

                //Insert XML Record Into DataBase
                string InsertedXML = await _coreseperationDealSummaryEngineCore.InsertXMLLog(MainPack, ONotaryRegisterReqId, cancellationToken);

                if (!string.IsNullOrEmpty(InsertedXML))
                {
                    SetEstateSeparationElementsOwnershipQuery Pack = _coreseperationDealSummaryEngineCore.ExtractDealPackFromXML(InsertedXML);
                }

                //Create RawDataPack from MainPack
                SPackForSign = _coreseperationDealSummaryEngineCore.ProvideSignRecord(MainPack, ONotaryRegisterReqId);

                //if (SPackForSign == null)
                //{
                //    SeparationDSCountRef = "0";
                //    GeneratedMessageRef = "خلاصه معامله های مورد نیاز برای ارسال تولید نشد. لطفاً مجدداً تلاش نمایید.";
                //    return (null, GeneratedMessageRef, SeparationDSCountRef);
                //}
                //Goto Client Side and Sign Every RawData and fill SignB64 & CertificateB64 Fields of


                //Goto Server Side and Extract MainPack from XML 
                //Fill SignB64 & CertificateB64 of every ElementOwner
                //Send MainPack included Sign information 

                return (SPackForSign, GeneratedMessageRef, SeparationDSCountRef);
            }
            catch (Exception ex)
            {
                SeparationDSCountRef = "0";
                GeneratedMessageRef = "خطا در تولید بسته های خلاصه معامله. لطفا مجددا تلاش نمایید. ";
                return (null, GeneratedMessageRef, SeparationDSCountRef);

            }
        }
        public async Task<(NotaryGeneratedDealSummary, string)> SendSDS(Document theCurrentReq, List<DSUDealSummarySignPacket> SignedPacket, CancellationToken cancellationToken, string messages)
        {
            string? messagesRef = messages;
            try
            {
                //ConfigurationManager.TypeDefinitions.ClientConfiguration clientConfiguration = new ConfigurationManager.TypeDefinitions.ClientConfiguration();

                //if (clientConfiguration.IsDSUDealSummaryCreationEnabled != NotaryOfficeImplementation.CustomClassesDefinitions.Configurations.TypeDefinitions.DSUActionLevel.FullFeature)
                //{
                //    messages = "ارسال اتوماتیک خلاصه معامله غیر فعال می باشد.";
                //    return Rad.CMS.Enumerations.NotaryGeneratedDealSummary.NotSent;
                //}

                //if (SignedPacket.Count==0)
                //{
                //    messages = "خلاصه معامله ای برای ارسال اتوماتیک تعریف نشده است.";
                //    return Rad.CMS.Enumerations.NotaryGeneratedDealSummary.NotSent;
                //}


                var ResultMessage = await _coreseperationDealSummaryEngineCore.SendSeparationDSPack(theCurrentReq, SignedPacket, cancellationToken);

                if (ResultMessage!=null && ResultMessage.IsSuccess)
                {
                    messagesRef = (ResultMessage != null && ResultMessage.message.Count > 0) ? ResultMessage.message[0] : null;
                    return (NotaryGeneratedDealSummary.Sent, messagesRef);
                }
                else
                {
                   // messagesRef = (ResultMessage != null && ResultMessage.message.Count > 0) ? ResultMessage.message[0] : null;
                    return (NotaryGeneratedDealSummary.NotSent, messagesRef);
                }

            }
            catch (Exception ex)
            {
                messagesRef = "خطای سیستمی در لحظه نهایی ارسال خلاصه معامله! لطفاً مجدداً تلاش نمایید.";

                return (NotaryGeneratedDealSummary.NotSent, messagesRef);
            }

        }

        public async Task<bool> IsDSUSent4ThisRequest(Document theCurrentRegisterServiceReq, CancellationToken cancellationToken)
        {
            var generatedDSUDealSummary =
                await _documentEstateDealSummarySeparationRepository.TableNoTracking.CountAsync(t =>
                    t.DocumentId == theCurrentRegisterServiceReq.Id &&
                    t.IsSent == NotaryGeneratedDealSummary.Sent.GetString(), cancellationToken) > 0;


            if (generatedDSUDealSummary == false)
            {

                return false;
            }
            else
            {
                return true;
            }
        }

        public bool IsPermitedToGenerateSDS(Document theRegisterServiceReq, ref string ResultMessage)
        {

            int ParkingCount = 0;
            int AppartmentCount = 0;
            foreach (DocumentEstate theOneRegCase in theRegisterServiceReq.DocumentEstates)
            {
                foreach (DocumentEstateSeparationPiece theOnePiece in theOneRegCase.DocumentEstateSeparationPieces)
                {
                    if (theOnePiece.DocumentEstateSeparationPieceKindId == "3")
                    {
                        ParkingCount = ParkingCount + 1;
                    }
                    if (theOnePiece.DocumentEstateSeparationPieceKindId == "2")
                    {
                        AppartmentCount = AppartmentCount + 1;
                    }
                }
            }

            foreach (DocumentEstate theOneRegCase in theRegisterServiceReq.DocumentEstates)
            {
                foreach (DocumentEstateSeparationPiece theOnePiece in theOneRegCase.DocumentEstateSeparationPieces)
                {
                    if (theOnePiece.HasOwner == YesNo.No.GetString())
                    {
                        decimal? TotalQuota = 0;
                        bool PieceHasQuota = false;
                        decimal? MaxTotalQuota = 0;
                        if (theOnePiece.DocumentEstateSeparationPieceKindId == "1" || theOnePiece.DocumentEstateSeparationPieceKindId == "2")
                        {

                            foreach (var q in theOnePiece.DocumentEstateSeparationPiecesQuota)
                            {
                                if (q.TotalQuota > MaxTotalQuota)
                                    MaxTotalQuota = q.TotalQuota;
                            }

                            decimal? df = 0;
                            foreach (var q in theOnePiece.DocumentEstateSeparationPiecesQuota)
                            {
                                df += (MaxTotalQuota / q.TotalQuota) * q.DetailQuota;
                            }


                            if ((df == 0) || (df / MaxTotalQuota) < 1)
                            {
                                if (theOnePiece.DocumentEstateSeparationPieceKindId == "1")
                                {
                                    ResultMessage = "مجموع سهم بندی قطعه " + theOnePiece.PieceNo + " از سهم کل این قطعه کمتر است. ";
                                }
                                else
                                {
                                    ResultMessage = "مجموع سهم بندی آپارتمان " + theOnePiece.PieceNo + " از سهم کل این آپارتمان کمتر است. ";
                                }

                                return false;
                            }

                            if ((df / MaxTotalQuota) > 1)
                            {
                                if (theOnePiece.DocumentEstateSeparationPieceKindId == "1")
                                {
                                    ResultMessage = "مجموع سهم بندی قطعه " + theOnePiece.PieceNo + "،از سهم کل این قطعه بیشتر است. ";
                                }
                                else
                                {
                                    ResultMessage = "مجموع سهم بندی آپارتمان " + theOnePiece.PieceNo + "، از سهم کل این آپارتمان بیشتر است. ";
                                }

                                return false;
                            }

                            if (ParkingCount != 0)
                            {
                                if (ParkingCount >= AppartmentCount)
                                {
                                    if (theOnePiece.InverseParking == null || theOnePiece.InverseParking.Count == 0)
                                    {
                                        ResultMessage = "پارکینگی برای آپارتمان شماره " + theOnePiece.PieceNo + " اختصاص نیافته است. یک پارکینگ برای این آپارتمان تعیین کنید.";
                                        return false;
                                    }
                                }
                                else if (ParkingCount < AppartmentCount)
                                {
                                    if (theOnePiece.InverseParking.Count > 1)
                                    {
                                        ResultMessage = "برای آپارتمان شماره " + theOnePiece.PieceNo + " بیش از یک پارکینگ اختصاص یافته است.";
                                        return false;
                                    }
                                }
                            }
                        }
                        else if (theOnePiece.DocumentEstateSeparationPieceKindId == "3" || theOnePiece.DocumentEstateSeparationPieceKindId == "4")
                        {
                            if (theOnePiece.Parking != null)
                            {
                                PieceHasQuota = true;
                            }

                            if (theOnePiece.Anbari != null)
                            {
                                PieceHasQuota = true;
                            }


                            if (PieceHasQuota == false)
                            {
                                if (theOnePiece.DocumentEstateSeparationPieceKindId == "3")
                                {
                                    ResultMessage = "وضعیت مالکیت پارکینگ " + theOnePiece.PieceNo + " مشخص نشده است.";
                                }
                                else
                                {
                                    ResultMessage = "وضعیت مالکیت انباری " + theOnePiece.PieceNo + " مشخص نشده است.";
                                }

                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
    }

}
