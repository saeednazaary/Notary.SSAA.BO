using Mapster;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Commands.Document;
using Notary.SSAA.BO.DataTransferObject.Commands.SignRequest;
using Notary.SSAA.BO.DataTransferObject.Mappers.Document;
using Notary.SSAA.BO.DataTransferObject.Queries.Document;
using Notary.SSAA.BO.DataTransferObject.Queries.SignRequest;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using System.Linq;
using System.Security.Principal;
using static Stimulsoft.Report.StiRecentConnections;

namespace Notary.SSAA.BO.CommandHandler.Document.Handlers
{
    public class CreateDocumentCommandHandler : BaseCommandHandler<CreateDocumentCommand, ApiResult>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IDocumentTypeRepository _documentTypeRepository;
        private readonly IDocumentTemplateRepository _documentTemplateRepository;
        private readonly IDateTimeService _dateTimeService;
        private Domain.Entities.Document masterEntity;
        private ApiResult<DocumentViewModel> apiResult;

        public CreateDocumentCommandHandler(IMediator mediator, IDateTimeService dateTimeService, IUserService userService, IDocumentRepository documentRepository, IDocumentTypeRepository documentTypeRepository, IDocumentTemplateRepository documentTemplateRepository,
            ILogger logger)
            : base(mediator, userService, logger)
        {
            masterEntity = new();
            apiResult = new();
            _dateTimeService = dateTimeService;
            _documentRepository = documentRepository;
            _documentTypeRepository = documentTypeRepository;
            _documentTemplateRepository = documentTemplateRepository;
        }

        protected override bool HasAccess(CreateDocumentCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
        protected override async Task<ApiResult> ExecuteAsync(CreateDocumentCommand request, CancellationToken cancellationToken)
        {
            BusinessValidation(request);

            if (apiResult.IsSuccess)
            {
                await UpdateDatabase(request, cancellationToken);
                // BusinessValidation(masterEntity,request);
                await _documentRepository.AddAsync(masterEntity, cancellationToken);
                ApiResult<DocumentViewModel> getResponse = await _mediator.Send(new GetDocumentByIdQuery(masterEntity.Id.ToString()), cancellationToken);
                if (getResponse.IsSuccess)
                {
                    apiResult = getResponse;
                }
                else
                {
                    apiResult.IsSuccess = false;
                    apiResult.statusCode = getResponse.statusCode;
                    apiResult.message.Add("مشکلی در دریافت اطلاعات از پایگاه داده رخ داده است ");
                    apiResult.message = getResponse.message;
                }
            }





            return apiResult;
        }
        private async Task<string> CreateDocumentNo(string Scriptoriumid, CancellationToken cancellationToken)
        {
            var beginReqNo = _dateTimeService.CurrentPersianDate[..4];
            beginReqNo += "401";
            beginReqNo += Scriptoriumid;
            string docNo = await _documentRepository.GetMaxDocNo(beginReqNo, cancellationToken);
            if (string.IsNullOrWhiteSpace(docNo))
            {
                docNo = _dateTimeService.CurrentPersianDate[..4];
                docNo += "401";
                docNo += Scriptoriumid;
                docNo += "000001";
            }
            else
            {
                decimal numberdocNo = decimal.Parse(docNo);
                numberdocNo++;
                docNo = numberdocNo.ToString();
            }
            return docNo;
        }

        private async Task UpdateDatabase(CreateDocumentCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Document masterCopyEntity;
            Domain.Entities.DocumentTemplate DocumentTemplateEntity;
            int createEnumDocumentState = (int)DocumentState.Created;
            #region masterEntity
            masterEntity = new()
            {
                Id = Guid.NewGuid(),
                RecordDate = _dateTimeService.CurrentDateTime,
                RequestDate = _dateTimeService.CurrentPersianDate,
                RequestTime = _dateTimeService.CurrentTime,
                ScriptoriumId = _userService.UserApplicationContext.BranchAccess.BranchCode,
                Ilm = DocumentConstants.CreateIlm,
                RequestNo = await CreateDocumentNo(_userService.UserApplicationContext.BranchAccess.BranchCode, cancellationToken),
                State = createEnumDocumentState.ToString(),
            };
            DocumentMapper.MapToDocument(ref masterEntity, request);
            var documentType = _documentTypeRepository.GetById(masterEntity.DocumentTypeId);
            masterEntity.DocumentTypeTitle = documentType?.Title;
            #endregion
            #region DocumentPeople
            for (int i = 0; i < request.DocumentPeople?.Count; i++)
            {
                if (request.DocumentPeople[i].IsNew)
                {
                    DocumentPerson documentPerson = new();
                    DocumentPersonMapper.MapToDocumentPerson(ref documentPerson, request.DocumentPeople[i]);
                    documentPerson.DocumentId = masterEntity.Id;
                    documentPerson.RowNo = (short)(request.DocumentPeople.Count - i);
                    documentPerson.ScriptoriumId = masterEntity.ScriptoriumId;
                    documentPerson.Ilm = DocumentConstants.CreateIlm;
                    documentPerson.IsFingerprintGotten = DocumentTemporaryConstants.IsFingerprintGotten;
                    if (request.DocumentPeople[i].HasGrowthJudgment == true)
                    {
                        documentPerson.HasGrowthJudgment = request.DocumentPeople[i].HasGrowthJudgment.ToYesNo();
                        documentPerson.GrowthDescription = request.DocumentPeople[i].GrowthDescription;
                        documentPerson.GrowthJudgmentDate = request.DocumentPeople[i].GrowthJudgmentDate;
                        documentPerson.GrowthJudgmentNo = request.DocumentPeople[i].GrowthJudgmentNo;
                        documentPerson.GrowthLetterDate = request.DocumentPeople[i].GrowthLetterDate;
                        documentPerson.GrowthLetterNo = request.DocumentPeople[i].GrowthLetterNo;
                    }

                    masterEntity.DocumentPeople.Add(documentPerson);
                }
                else
                {
                    continue;
                }
            }
            #endregion
            #region DocumentRelatedPeople
            foreach (DocumentRelatedPersonViewModel documentRelatedPersonViewModel in request.DocumentRelatedPeople)
            {
                if (documentRelatedPersonViewModel.IsNew)
                {
                    DocumentPersonRelated newRelatedPerson = new();
                    DocumentRelatedPersonMapper.MapToDocumentRelatedPerson(ref newRelatedPerson, documentRelatedPersonViewModel);
                    newRelatedPerson.DocumentId = masterEntity.Id;
                    newRelatedPerson.DocumentScriptoriumId = masterEntity.ScriptoriumId;
                    newRelatedPerson.Ilm = DocumentConstants.CreateIlm;
                    masterEntity.DocumentPersonRelatedDocuments.Add(newRelatedPerson);
                }
                else
                {
                    continue;
                }
            }
            #endregion
            #region Estates
            var estateMap = request.DocumentEstates
                .Where(e => e.IsNew)
                .Select((e, i) =>
                {
                    var docEstate = new DocumentEstate();
                    DocumentEstateMapper.MapToDocumentEstate(ref docEstate, e);
                    docEstate.DocumentId = masterEntity.Id;
                    docEstate.RowNo = (byte)(request.DocumentEstates.Count - i);
                    docEstate.ScriptoriumId = masterEntity.ScriptoriumId;
                    docEstate.Ilm = DocumentConstants.CreateIlm;
                    return new { Dto = e, Entity = docEstate };
                })
                .ToDictionary(x => x.Dto, x => x.Entity);

            var allOwnerShips = estateMap.Keys
                .SelectMany(e => e.DocumentEstateOwnerShips.Select((o, l) => new { Estate = e, Owner = o, Index = l }))
                .ToList();

            foreach (var item in allOwnerShips)
            {
                var documentEstate = estateMap[item.Estate];

                var ownershipDoc = new DocumentEstateOwnershipDocument();
                DocumentEstateMapper.MapToDocumentEstate(ref ownershipDoc, item.Owner);

                ownershipDoc.DocumentEstateId = documentEstate.Id;
                ownershipDoc.RowNo = (byte)(item.Estate.DocumentEstateOwnerShips.Count - item.Index);
                ownershipDoc.ScriptoriumId = masterEntity.ScriptoriumId;
                ownershipDoc.Ilm = DocumentConstants.CreateIlm;

                documentEstate.DocumentEstateOwnershipDocuments.Add(ownershipDoc);
            }

            foreach (var estate in estateMap.Values)
            {
                masterEntity.DocumentEstates.Add(estate);
            }
            #endregion
            #region Vehicles
            for (int i = 0; i < request.DocumentVehicles.Count; i++)
            {
                if (request.DocumentVehicles[i].IsNew)
                {
                    DocumentVehicle documentVehicle = new();
                    DocumentVehicleMapper.MapToDocumentVehicle(ref documentVehicle, request.DocumentVehicles[i]);
                    documentVehicle.DocumentId = masterEntity.Id;
                    documentVehicle.RowNo = (Byte)(request.DocumentVehicles.Count - i);
                    documentVehicle.ScriptoriumId = masterEntity.ScriptoriumId;
                    documentVehicle.Ilm = DocumentConstants.CreateIlm;
                    foreach (var item in documentVehicle.DocumentVehicleQuota)
                    {
                        item.DocumentVehicleId = documentVehicle.Id;
                        item.ScriptoriumId = masterEntity.ScriptoriumId;
                    }
                    foreach (var item in documentVehicle.DocumentVehicleQuotaDetails)
                    {
                        item.DocumentVehicleId = documentVehicle.Id;
                        item.ScriptoriumId = masterEntity.ScriptoriumId;
                    }

                    masterEntity.DocumentVehicles.Add(documentVehicle);
                }
                else
                {
                    continue;
                }
            }
            #endregion
            #region Cases
            for (int i = 0; i < request.DocumentCases.Count; i++)
            {
                if (request.DocumentCases[i].IsNew)
                {
                    DocumentCase documentCase = new();
                    DocumentCaseMapper.MapToDocumentCase(ref documentCase, request.DocumentCases[i]);
                    documentCase.DocumentId = masterEntity.Id;
                    documentCase.RowNo = (Byte)(request.DocumentCases.Count - i);
                    documentCase.ScriptoriumId = masterEntity.ScriptoriumId;
                    documentCase.Ilm = DocumentConstants.CreateIlm;

                    masterEntity.DocumentCases.Add(documentCase);
                }
                else
                {
                    continue;
                }
            }
            #endregion
            #region DocumentInfoText
            if (request.DocumentInfoText != null)
            {
                if (request.DocumentInfoText.IsNew)
                {
                    DocumentInfoText documentInfoText = new();
                    DocumentInfoTextMapper.MapToDocumentInfoText(ref documentInfoText, request.DocumentInfoText);
                    documentInfoText.DocumentId = masterEntity.Id;
                    documentInfoText.ScriptoriumId = masterEntity.ScriptoriumId;
                    documentInfoText.Ilm = DocumentConstants.CreateIlm;
                    documentInfoText.RecordDate = _dateTimeService.CurrentDateTime;

                    masterEntity.DocumentInfoText = documentInfoText;
                }

            }
            //درهر صورت باید  متن سند  جدید ایجاد شود
            else if (!(request.IsCopyDocumentInfoText == true && !string.IsNullOrEmpty(request.DocumentCopyId)))
            {
                DocumentInfoText documentInfoText = new();
                documentInfoText.Id = Guid.NewGuid();
                documentInfoText.DocumentId = masterEntity.Id;
                documentInfoText.ScriptoriumId = masterEntity.ScriptoriumId;
                documentInfoText.Ilm = DocumentConstants.CreateIlm;
                documentInfoText.RecordDate = _dateTimeService.CurrentDateTime;
                masterEntity.DocumentInfoText = documentInfoText;
            }
            #endregion
            #region DocumentInfoOther
            if (request.DocumentInfoOther != null)
            {
                if (request.DocumentInfoOther.IsNew)
                {
                    DocumentInfoOther documentInfoOther = new();
                    DocumentInfoOtherMapper.MapToDocumentInfoOther(ref documentInfoOther, request.DocumentInfoOther);
                    documentInfoOther.DocumentId = masterEntity.Id;
                    documentInfoOther.ScriptoriumId = masterEntity.ScriptoriumId;
                    documentInfoOther.DocumentTypeSubjectId = request.DocumentTypeSubjectId != null ? request.DocumentTypeSubjectId.FirstOrDefault() : null;
                    documentInfoOther.Ilm = DocumentConstants.CreateIlm;
                    documentInfoOther.RecordDate = _dateTimeService.CurrentDateTime;
                    masterEntity.DocumentInfoOther = documentInfoOther;
                }

            }
            //درهر صورت باید  سایر اطلاعات سند  جدید ایجاد شود
            else
            {
                DocumentInfoOther documentInfoOther = new();
                documentInfoOther.Id = Guid.NewGuid();
                documentInfoOther.DocumentId = masterEntity.Id;
                documentInfoOther.ScriptoriumId = masterEntity.ScriptoriumId;
                documentInfoOther.Ilm = DocumentConstants.CreateIlm;
                documentInfoOther.RecordDate = _dateTimeService.CurrentDateTime;
                documentInfoOther.DocumentTypeSubjectId = request.DocumentTypeSubjectId != null ? request.DocumentTypeSubjectId.FirstOrDefault() : null;
                documentInfoOther.DocumentAssetTypeId = request.DocumentAssetTypeId != null ? request.DocumentAssetTypeId.FirstOrDefault() : null;
                masterEntity.DocumentInfoOther = documentInfoOther;
            }

            #endregion
            #region DocumentRelations
            if ( request.DocumentMainRelation != null )
            {
                
                    DocumentRelationMapper.MapToDocumentMainRelation ( ref masterEntity, request.DocumentMainRelation );

                
            }

            for (int i = 0; i < request.DocumentRelations.Count; i++)
            {
                if (request.DocumentRelations[i].IsNew)
                {
                    DocumentRelation documentRelation = new();
                    DocumentRelationMapper.MapToDocumentRelation(ref documentRelation, request.DocumentRelations[i]);
                    documentRelation.DocumentId = masterEntity.Id;
                    documentRelation.ScriptoriumId = masterEntity.ScriptoriumId;
                    documentRelation.Ilm = DocumentConstants.CreateIlm;
                    documentRelation.RecordDate = _dateTimeService.CurrentDateTime;

                    masterEntity.DocumentRelationDocuments.Add(documentRelation);
                }
                else
                {
                    continue;
                }
            }
            #endregion
            #region DocumentCosts

            for (int i = 0; i < request.DocumentCosts.Count; i++)
            {
                if (request.DocumentCosts[i].IsNew)
                {
                    DocumentCost documentCost = new();
                    DocuemntCostMapper.MapToDocumentCost(ref documentCost, request.DocumentCosts[i]);
                    documentCost.DocumentId = masterEntity.Id;
                    documentCost.ScriptoriumId = masterEntity.ScriptoriumId;
                    documentCost.Ilm = DocumentConstants.CreateIlm;
                    documentCost.RecordDate = _dateTimeService.CurrentDateTime;
                    masterEntity.DocumentCosts.Add(documentCost);
                    //ایجاد رکوردی در جدول هزینه محاسبه شده توسط سامانه
                    if (!string.IsNullOrEmpty(request.DocumentCosts[i].RequestPriceUnchanged))
                    {
                        DocumentCostUnchanged documentCostUnchanged = new();
                        DocuemntCostMapper.MapToDocumentCostUnchanged(ref documentCostUnchanged, request.DocumentCosts[i]);
                        documentCostUnchanged.DocumentId = masterEntity.Id;
                        documentCostUnchanged.ScriptoriumId = masterEntity.ScriptoriumId;
                        documentCostUnchanged.Ilm = DocumentConstants.CreateIlm;
                        documentCostUnchanged.RecordDate = DateTime.Now;

                        masterEntity.DocumentCostUnchangeds.Add(documentCostUnchanged);
                    }
                }
                else
                {
                    continue;
                }
            }
            #endregion
            #region DocumentPayments
            if (request.DocumentPayments?.Count > 0)
            {
                request.DocumentPayments.ToList<DocumentPaymentViewModel>().ForEach(x =>
                {
                    DocumentPayment documentPayment = new();
                    DocumentPaymentMapper.MapToDocumentPayment(ref documentPayment, x, masterEntity.ScriptoriumId);
                    masterEntity.DocumentPayments.Add(documentPayment);
                });
            }
            #endregion
            #region DocumentInfoJudgment
            if (request.DocumentInfoJudgment != null)
            {
                if (request.DocumentInfoJudgment.IsNew && request.IsRequestBasedJudgment == true)
                {
                    DocumentInfoJudgement documentInfoJudgment = new();
                    DocumentInfoJudgmentMapper.MapToDocumentInfoJudgment(ref documentInfoJudgment, request.DocumentInfoJudgment);
                    documentInfoJudgment.DocumentId = masterEntity.Id;
                    documentInfoJudgment.ScriptoriumId = masterEntity.ScriptoriumId;
                    documentInfoJudgment.Ilm = DocumentConstants.CreateIlm;
                    documentInfoJudgment.RecordDate = _dateTimeService.CurrentDateTime;
                    masterEntity.DocumentInfoJudgement = documentInfoJudgment;
                }

            }
            //درهر صورت باید  حکم دادگاه   جدید ایجاد شود


            #endregion
            #region DocumentInfoConfirm
            DocumentInfoConfirm documentInfoConfirm = new();
            documentInfoConfirm.Id = Guid.NewGuid();
            documentInfoConfirm.DocumentId = masterEntity.Id;
            documentInfoConfirm.ScriptoriumId = masterEntity.ScriptoriumId;
            documentInfoConfirm.Ilm = DocumentConstants.CreateIlm;
            documentInfoConfirm.RecordDate = _dateTimeService.CurrentDateTime;
            documentInfoConfirm.CreateDate = _dateTimeService.CurrentPersianDate;
            documentInfoConfirm.CreateTime = _dateTimeService.CurrentTime;
            documentInfoConfirm.CreatorNameFamily = _userService.UserApplicationContext.User.Name + " " + _userService.UserApplicationContext.User.Family;
            masterEntity.DocumentInfoConfirm = documentInfoConfirm;
            #endregion
            #region MasterCopy
            if (!string.IsNullOrEmpty(request.DocumentCopyId))
            {
                masterCopyEntity = await _documentRepository.GetDocumentById(request.DocumentCopyId.ToGuid(), GetCopyDetails ( request), cancellationToken);
                if (request.IsCopyDocumentPeople == true)
                {
                    IList<DocumentPerson> DocumentCopyPeople = masterCopyEntity.DocumentPeople.ToList();
                    for (int i = 0; i < DocumentCopyPeople.Count; i++)
                    {
                        DocumentPerson documentPerson = new();
                        documentPerson.MobileNo = DocumentCopyPeople[i].MobileNo;
                        documentPerson.IsAlive = DocumentCopyPeople[i].IsAlive;
                        documentPerson.IsSabtahvalCorrect = DocumentCopyPeople[i].IsSabtahvalCorrect;
                        documentPerson.IsSabtahvalChecked = DocumentCopyPeople[i].IsSabtahvalChecked;
                        documentPerson.MobileNoState = DocumentCopyPeople[i].MobileNoState;
                        documentPerson.IsOriginal = DocumentCopyPeople[i].IsOriginal;
                        documentPerson.SanaState = DocumentCopyPeople[i].SanaState;
                        documentPerson.Serial = DocumentCopyPeople[i].Serial;
                        documentPerson.Seri = DocumentCopyPeople[i].Seri;
                        documentPerson.SeriAlpha = DocumentCopyPeople[i].SeriAlpha;
                        documentPerson.IdentityNo = DocumentCopyPeople[i].IdentityNo;
                        documentPerson.IdentityIssueLocation = DocumentCopyPeople[i].IdentityIssueLocation;
                        documentPerson.FatherName = DocumentCopyPeople[i].FatherName;
                        documentPerson.BirthDate = DocumentCopyPeople[i].BirthDate;
                        documentPerson.Family = DocumentCopyPeople[i].Family;
                        documentPerson.Name = DocumentCopyPeople[i].Name;
                        documentPerson.NationalNo = DocumentCopyPeople[i].NationalNo;
                        documentPerson.NationalityId = DocumentCopyPeople[i].NationalityId;
                        documentPerson.IsIranian = DocumentCopyPeople[i].IsIranian;
                        documentPerson.IsRelated = DocumentCopyPeople[i].IsRelated;
                        documentPerson.IsFingerprintGotten = DocumentCopyPeople[i].IsFingerprintGotten;
                        documentPerson.PassportNo = DocumentCopyPeople[i].PassportNo;
                        documentPerson.Description = DocumentCopyPeople[i].Description;
                        documentPerson.Tel = DocumentCopyPeople[i].Tel;
                        documentPerson.AddressType = DocumentCopyPeople[i].AddressType;
                        documentPerson.Address = DocumentCopyPeople[i].Address;
                        documentPerson.PostalCode = DocumentCopyPeople[i].PostalCode;
                        documentPerson.Email = DocumentCopyPeople[i].Email;
                        documentPerson.SexType = DocumentCopyPeople[i].SexType;
                        documentPerson.DocumentPersonTypeId = masterCopyEntity.DocumentTypeId == masterEntity.DocumentTypeId ? DocumentCopyPeople[i].DocumentPersonTypeId : null;
                        documentPerson.LegalpersonTypeId = DocumentCopyPeople[i].LegalpersonTypeId;
                        documentPerson.LegalpersonNatureId = DocumentCopyPeople[i].LegalpersonNatureId;
                        documentPerson.CompanyTypeId = DocumentCopyPeople[i].CompanyTypeId;
                        documentPerson.PersonType = DocumentCopyPeople[i].PersonType;
                        documentPerson.Id = Guid.NewGuid();
                        documentPerson.DocumentId = masterEntity.Id;
                        documentPerson.RowNo = (Byte)(DocumentCopyPeople.Count - i);
                        documentPerson.ScriptoriumId = masterEntity.ScriptoriumId;
                        documentPerson.FaxNo = DocumentCopyPeople[i].FaxNo;
                        documentPerson.Ilm = DocumentConstants.CreateIlm;
                        documentPerson.HasGrowthJudgment = DocumentCopyPeople[i].HasGrowthJudgment;
                        documentPerson.GrowthDescription = DocumentCopyPeople[i].GrowthDescription;
                        documentPerson.GrowthJudgmentDate = DocumentCopyPeople[i].GrowthJudgmentDate;
                        documentPerson.GrowthJudgmentNo = DocumentCopyPeople[i].GrowthJudgmentNo;
                        documentPerson.GrowthLetterDate = DocumentCopyPeople[i].GrowthLetterDate;
                        documentPerson.GrowthLetterNo = DocumentCopyPeople[i].GrowthLetterNo;
                        masterEntity.DocumentPeople.Add(documentPerson);
                    }
                }
                if (request.IsCopyDocumentCases == true)
                {
                    IList<DocumentCase> DocumentCopyCase = masterCopyEntity.DocumentCases.ToList();
                    for (int i = 0; i < DocumentCopyCase.Count; i++)
                    {
                        DocumentCase documentCase = new();
                        documentCase.Description = DocumentCopyCase[i].Description;
                        documentCase.Title = DocumentCopyCase[i].Title;
                        documentCase.Id = Guid.NewGuid();
                        documentCase.DocumentId = masterEntity.Id;
                        documentCase.RowNo = (Byte)(DocumentCopyCase.Count - i);
                        documentCase.ScriptoriumId = masterEntity.ScriptoriumId;
                        documentCase.Ilm = DocumentConstants.CreateIlm;
                        masterEntity.DocumentCases.Add(documentCase);
                    }
                    IList<DocumentVehicle> DocumentCopyVehicles = masterCopyEntity.DocumentVehicles.ToList();
                    foreach (var copyVehicle in DocumentCopyVehicles)

                    {
                        DocumentVehicle documentVehicle = new();
                        documentVehicle.Description = copyVehicle.Description;
                        documentVehicle.Id = Guid.NewGuid();
                        documentVehicle.DocumentId = masterEntity.Id;
                        documentVehicle.ScriptoriumId = masterEntity.ScriptoriumId;
                        documentVehicle.Ilm = DocumentConstants.CreateIlm;
                        documentVehicle.OldDocumentDate = copyVehicle.OldDocumentDate;
                        documentVehicle.OldDocumentIssuer = copyVehicle.OldDocumentIssuer;
                        documentVehicle.OldDocumentNo = copyVehicle.OldDocumentNo;
                        documentVehicle.OwnershipPrintedDocumentNo = copyVehicle.OwnershipPrintedDocumentNo;
                        documentVehicle.InssuranceNo = copyVehicle.InssuranceNo;
                        documentVehicle.InssuranceCo = copyVehicle.InssuranceCo;
                        documentVehicle.OtherInfo = copyVehicle.OtherInfo;
                        documentVehicle.FuelCardNo = copyVehicle.FuelCardNo;
                        documentVehicle.DutyFicheNo = copyVehicle.DutyFicheNo;
                        documentVehicle.CardNo = copyVehicle.CardNo;
                        documentVehicle.CylinderCount = copyVehicle.CylinderCount;
                        documentVehicle.Color = copyVehicle.Color;
                        documentVehicle.EngineCapacity = copyVehicle.EngineCapacity;
                        documentVehicle.ChassisNo = copyVehicle.ChassisNo;
                        documentVehicle.EngineNo = copyVehicle.EngineNo;
                        documentVehicle.Model = copyVehicle.Model;
                        documentVehicle.Tip = copyVehicle.Tip;
                        documentVehicle.System = copyVehicle.System;
                        documentVehicle.Type = copyVehicle.Type;
                        documentVehicle.MadeInIran = copyVehicle.MadeInIran;
                        documentVehicle.IsInTaxList = copyVehicle.IsInTaxList;
                        documentVehicle.RowNo = copyVehicle.RowNo;
                        documentVehicle.Price = copyVehicle.Price;
                        documentVehicle.PlaqueBuyer = copyVehicle.PlaqueBuyer;
                        documentVehicle.PlaqueNoAlphaBuyer = copyVehicle.PlaqueNoAlphaBuyer;
                        documentVehicle.PlaqueSeriBuyer = copyVehicle.PlaqueSeriBuyer;
                        documentVehicle.PlaqueNo2Buyer = copyVehicle.PlaqueNo2Buyer;
                        documentVehicle.PlaqueNo1Buyer = copyVehicle.PlaqueNo1Buyer;
                        documentVehicle.PlaqueSeller = copyVehicle.PlaqueSeller;
                        documentVehicle.PlaqueNoAlphaSeller = copyVehicle.PlaqueNoAlphaSeller;
                        documentVehicle.PlaqueSeriSeller = copyVehicle.PlaqueSeriSeller;
                        documentVehicle.PlaqueNo2Seller = copyVehicle.PlaqueNo2Seller;
                        documentVehicle.PlaqueNo1Seller = copyVehicle.PlaqueNo1Seller;
                        documentVehicle.NumberingLocation = copyVehicle.NumberingLocation;
                        documentVehicle.IsVehicleNumbered = copyVehicle.IsVehicleNumbered;
                        documentVehicle.QuotaText = copyVehicle.QuotaText;
                        documentVehicle.Description = copyVehicle.Description;
                        documentVehicle.SellTotalQuota = copyVehicle.SellTotalQuota;
                        documentVehicle.SellDetailQuota = copyVehicle.SellDetailQuota;
                        documentVehicle.OwnershipTotalQuota = copyVehicle.OwnershipTotalQuota;
                        documentVehicle.OwnershipDetailQuota = copyVehicle.OwnershipDetailQuota;
                        documentVehicle.OwnershipType = copyVehicle.OwnershipType;
                        documentVehicle.DocumentVehicleTipId = copyVehicle.DocumentVehicleTipId;
                        documentVehicle.DocumentVehicleTypeId = copyVehicle.DocumentVehicleTypeId;
                        documentVehicle.DocumentVehicleSystemId = copyVehicle.DocumentVehicleSystemId;
                        foreach (var item in copyVehicle.DocumentVehicleQuotaDetails)
                        {
                            DocumentVehicleQuotaDetail newDocumentVehicleQuotaDetail = new DocumentVehicleQuotaDetail();
                            newDocumentVehicleQuotaDetail = item;
                            newDocumentVehicleQuotaDetail.DocumentVehicleId = documentVehicle.Id;
                            newDocumentVehicleQuotaDetail.Id = item.Id;
                            newDocumentVehicleQuotaDetail.QuotaText = item.QuotaText;
                            newDocumentVehicleQuotaDetail.DocumentPersonBuyerId = item.DocumentPersonBuyerId;
                            newDocumentVehicleQuotaDetail.DocumentPersonSellerId = item.DocumentPersonSellerId;
                            newDocumentVehicleQuotaDetail.OwnershipDetailQuota = item.OwnershipDetailQuota;
                            newDocumentVehicleQuotaDetail.OwnershipTotalQuota = item.OwnershipTotalQuota;
                            newDocumentVehicleQuotaDetail.ScriptoriumId = masterEntity.ScriptoriumId;
                            newDocumentVehicleQuotaDetail.Ilm = item.Ilm;
                            documentVehicle.DocumentVehicleQuotaDetails.Add(newDocumentVehicleQuotaDetail);
                        }
                        foreach (var item in copyVehicle.DocumentVehicleQuota)
                        {
                            DocumentVehicleQuotum NewDocumentVehicleQuotum = new DocumentVehicleQuotum();
                            NewDocumentVehicleQuotum.DocumentVehicleId = documentVehicle.Id ;
                            NewDocumentVehicleQuotum.QuotaText = item.QuotaText;
                            NewDocumentVehicleQuotum.DocumentPersonId = item.DocumentPersonId ;
                            NewDocumentVehicleQuotum.DetailQuota = item.DetailQuota;
                            NewDocumentVehicleQuotum.TotalQuota = item.TotalQuota;
                            NewDocumentVehicleQuotum.ScriptoriumId = masterEntity.ScriptoriumId;
                            NewDocumentVehicleQuotum.Ilm = item.Ilm ;
                            documentVehicle.DocumentVehicleQuota.Add(NewDocumentVehicleQuotum);
                        }
                        masterEntity.DocumentVehicles.Add(documentVehicle);
                    }
                }
                if (request.IsCopyDocumentInfoText == true)
                {
                    DocumentInfoText DocumentCopyInfoText = masterCopyEntity.DocumentInfoText;
                    DocumentInfoText documentInfoText = new();
                    documentInfoText.DocumentText = DocumentCopyInfoText?.DocumentText;
                    documentInfoText.LegalText = DocumentCopyInfoText?.LegalText;
                    documentInfoText.DocumentDescription = DocumentCopyInfoText?.DocumentDescription;
                    documentInfoText.Description = DocumentCopyInfoText?.Description;
                    documentInfoText.DocumentId = masterEntity.Id;
                    documentInfoText.Id = Guid.NewGuid();
                    documentInfoText.ScriptoriumId = masterEntity.ScriptoriumId;
                    documentInfoText.Ilm = DocumentConstants.CreateIlm;
                    documentInfoText.RecordDate = _dateTimeService.CurrentDateTime;
                    masterEntity.DocumentInfoText = documentInfoText;
                }
            }
            else if (!string.IsNullOrEmpty(request.DocumentTemplateId))
            {
                DocumentTemplateEntity = await _documentTemplateRepository.GetByIdAsync(cancellationToken, request.DocumentTemplateId.ToGuid());
                if (DocumentTemplateEntity != null)
                {
                    DocumentInfoText documentInfoText = new();
                    documentInfoText.DocumentId = masterEntity.Id;
                    documentInfoText.Id = Guid.NewGuid();
                    documentInfoText.ScriptoriumId = masterEntity.ScriptoriumId;
                    documentInfoText.Ilm = DocumentConstants.CreateIlm;
                    documentInfoText.LegalText = DocumentTemplateEntity.Text;
                    documentInfoText.RecordDate = _dateTimeService.CurrentDateTime;
                    masterEntity.DocumentInfoText = documentInfoText;

                }
            }

            #endregion
        }
        private void BusinessValidation(CreateDocumentCommand request)
        {
            if (string.IsNullOrEmpty(request.RequestId))
            {
                if (request.IsRequestBasedJudgment == true && request.DocumentInfoJudgment == null)
                {
                    apiResult.message.Add(SystemMessagesConstant.Judgment_Create_Required);
                }
                else if (request.IsRequestBasedJudgment == true && request.DocumentInfoJudgment != null)
                {
                    if (!request.DocumentInfoJudgment.IsNew)
                    {
                        apiResult.message.Add(SystemMessagesConstant.Judgment_Create_Required);
                    }

                    if (request.DocumentInfoJudgment.DocumentJudgmentTypeId.Count < 1)
                        apiResult.message.Add(SystemMessagesConstant.Judgment_Type_Required);

                    if (string.IsNullOrEmpty(request.DocumentInfoJudgment.IssueDate))
                        apiResult.message.Add(SystemMessagesConstant.Judgment_IssueDate_Required);

                    if (string.IsNullOrEmpty(request.DocumentInfoJudgment.IssueNo))
                        apiResult.message.Add(SystemMessagesConstant.Judgment_IssueNo_Required);

                    if (string.IsNullOrEmpty(request.DocumentInfoJudgment.IssuerName))
                        apiResult.message.Add(SystemMessagesConstant.Judgment_IssuerName_Required);

                    if (string.IsNullOrEmpty(request.DocumentInfoJudgment.CaseClassifyNo))
                        apiResult.message.Add(SystemMessagesConstant.Judgment_CaseClassifyNo_Required);
                }

                foreach (var item in request.DocumentRelatedPeople)
                {
                    if (!item.IsNew)
                        continue;

                    var errors = item.validateRelatedAgentDocumentIssuer();
                    if (errors != null)
                        apiResult.message.AddRange(errors);

                    if (item.RelatedAgentTypeId.First() != "10" &&
                        item.RelatedReliablePersonReasonId.Count < 1)
                    {
                        apiResult.message.Add(SystemMessagesConstant.Related_ReliableReason_Required);
                    }

                    if (item.RelatedAgentTypeId.First() == "1")
                    {
                        if (!ValidatorHelper.BeValidPersianDate(item.RelatedAgentDocumentDate))
                            apiResult.message.Add(SystemMessagesConstant.Related_DocumentDate_Invalid);

                        if (string.IsNullOrWhiteSpace(item.RelatedAgentDocumentNo))
                            apiResult.message.Add(SystemMessagesConstant.Related_DocumentNo_Required);
                        else if (item.RelatedAgentDocumentNo.Length > 50)
                            apiResult.message.Add(SystemMessagesConstant.Related_DocumentNo_MaxLength);

                        if (item.IsRelatedAgentDocumentAbroad)
                        {
                            if (item.RelatedAgentDocumentCountryId.Count < 1)
                                apiResult.message.Add(SystemMessagesConstant.Related_DocumentCountry_Required);
                        }
                        else
                        {
                            if (item.RelatedAgentDocumentScriptoriumId.Count < 1)
                                apiResult.message.Add(SystemMessagesConstant.Related_Scriptorium_Required);

                            if (item.IsRelatedDocumentInSSAR &&
                                item.RelatedAgentDocumentDate.GreaterThanEqual("1392/06/26"))
                                apiResult.message.Add(SystemMessagesConstant.Related_DocumentDate_SSAR_Invalid);

                            if (!item.IsRelatedDocumentInSSAR &&
                                item.RelatedAgentDocumentDate.LessThanEqual("1392/06/26"))
                                apiResult.message.Add(SystemMessagesConstant.Related_DocumentDate_SSAR_Invalid);
                        }
                    }

                    if (item.RelatedAgentTypeId.First() != "03" &&
                        item.RelatedAgentTypeId.First() != "01")
                    {
                        if (!ValidatorHelper.BeValidPersianDate(item.RelatedAgentDocumentDate))
                            apiResult.message.Add(SystemMessagesConstant.Related_DocumentDate_Invalid);

                        if (string.IsNullOrWhiteSpace(item.RelatedAgentDocumentNo))
                            apiResult.message.Add(SystemMessagesConstant.Related_DocumentNo_Required);
                        else if (item.RelatedAgentDocumentNo.Length > 50)
                            apiResult.message.Add(SystemMessagesConstant.Related_DocumentNo_MaxLength);

                        if (string.IsNullOrWhiteSpace(item.RelatedAgentDocumentIssuer))
                            apiResult.message.Add(SystemMessagesConstant.Related_DocumentIssuer_Required);
                    }

                    if (!masterEntity.DocumentPeople.Any(x =>
                            x.Id == item.MainPersonId.First().ToGuid()))
                    {
                        apiResult.message.Add(SystemMessagesConstant.Related_MainPerson_Deleted);
                    }

                    if (!masterEntity.DocumentPeople.Any(x =>
                            x.Id == item.RelatedAgentPersonId.First().ToGuid()))
                    {
                        apiResult.message.Add(SystemMessagesConstant.Related_AgentPerson_Deleted);
                    }
                }
            }
            else
            {
                apiResult.message.Add(SystemMessagesConstant.Document_NotFound);
            }

            if (apiResult.message.Count > 0)
            {
                apiResult.statusCode = ApiResultStatusCode.Success;
                apiResult.IsSuccess = false;
            }
        }

        private void BusinessMasterValidation(Domain.Entities.Document masterEntity, CreateDocumentCommand request)
        {
            if (masterEntity != null)
            {
      

          
            }
            else
            {
                apiResult.message.Add("سند مربوطه یافت نشد ");
            }

            if (apiResult.message.Count > 0)
            {
                apiResult.statusCode = ApiResultStatusCode.Success;
                apiResult.IsSuccess = false;
            }
        }

        private static List<string> GetCopyDetails(CreateDocumentCommand request)
        {
            List<string> detailsList = new List<string>();
            if (!string.IsNullOrEmpty(request.DocumentCopyId))
            {
                if (request.IsCopyDocumentPeople == true)
                {
                    detailsList.Add("DocumentPeople");
                }
                if (request.IsCopyDocumentCases == true)
                {
                    detailsList.Add("DocumentCases");
                    detailsList.Add("DocumentVehicles");
                }
                if (request.IsCopyDocumentInfoText == true)
                {
                    detailsList.Add("DocumentInfoText");
                }

            }


            return detailsList;

        }
    }
}