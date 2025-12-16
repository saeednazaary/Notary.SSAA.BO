using MediatR;
using Serilog;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Commands.RelatedDocument;
using Notary.SSAA.BO.DataTransferObject.Mappers.Document;
using Notary.SSAA.BO.DataTransferObject.Mappers.RelatedDocument;
using Notary.SSAA.BO.DataTransferObject.Queries.RelatedDocument;
using Notary.SSAA.BO.DataTransferObject.ViewModels.RelatedDocument;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using System;

namespace Notary.SSAA.BO.CommandHandler.RelatedDocument.Handlers
{
    public class UpdateRelatedDocumentCommandHandler : BaseCommandHandler<UpdateRelatedDocumentCommand, ApiResult>
    {
        private readonly IDocumentRepository _documentRepository;
        private Domain.Entities.Document masterEntity;
        private ApiResult<DocumentDetailViewModel> apiResult;
        private readonly IDocumentPersonRepository _documentPersonRepository;
        private readonly IRepository<DocumentEstate> _documentEstateRepository;
        private readonly IDateTimeService _dateTimeService;

        public UpdateRelatedDocumentCommandHandler(IMediator mediator, IUserService userService, ILogger logger, IDocumentRepository documentRepository, IDocumentPersonRepository documentPersonRepository, IDateTimeService dateTimeService,
           IRepository<DocumentEstate> documentEstateRepository) : base(mediator, userService, logger)
        {
            apiResult = new();
            masterEntity = new();
            _documentRepository = documentRepository;
            _documentPersonRepository = documentPersonRepository;
            _documentEstateRepository = documentEstateRepository;
            _dateTimeService = dateTimeService;
        }

        protected async override Task<ApiResult> ExecuteAsync(UpdateRelatedDocumentCommand request, CancellationToken cancellationToken)
        {
            BusinessValidation(request);
            if (apiResult.IsSuccess)
            {
                masterEntity = await _documentRepository.GetByIdAsync(cancellationToken, request.RequestId.ToGuid());
                if (masterEntity == null)
                {
                    apiResult.IsSuccess = false;
                    apiResult.statusCode = ApiResultStatusCode.NotFound;
                    apiResult.message.Add("سند مربوطه پیدا نشد");
                }
                else
                {
                    if (request.DocumentInfoText != null)
                    {
                        await _documentRepository.LoadReferenceAsync(masterEntity, e => e.DocumentInfoText, cancellationToken);
                    }
                    if (request.DocumentInfoJudgment != null)
                    {
                        await _documentRepository.LoadReferenceAsync(masterEntity, e => e.DocumentInfoJudgement, cancellationToken);
                    }
                    if (request.DocumentPerson != null)
                    {
                        await _documentRepository.LoadCollectionAsync(masterEntity, e => e.DocumentPeople, cancellationToken);
                    }
                    if (request.RegisterCount != null)
                    {
                        await _documentRepository.LoadReferenceAsync(masterEntity, e => e.DocumentInfoOther, cancellationToken);
                    }
                    UpdateDatabase(request);
                    await _documentRepository.UpdateAsync(masterEntity, cancellationToken);
                    ApiResult<DocumentDetailViewModel> getResponse = await _mediator.Send(new GetDocumentDetailByIdQuery(masterEntity.Id.ToString()), cancellationToken);
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
            }
            return apiResult;
        }
        protected override bool HasAccess(UpdateRelatedDocumentCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
        private void UpdateDatabase(UpdateRelatedDocumentCommand request)
        {

            RelatedDocumentMapper.ToEntity(ref masterEntity, request);
            #region RelatedDocumentInfoJudgment

            if (request.DocumentInfoJudgment != null)
            {


                if (request.DocumentInfoJudgment.IsNew)
                {
                    DocumentInfoJudgement newDocumentInfoJudgment = new();
                    RelatedDocumentMapper.ToEntity(ref newDocumentInfoJudgment, request.DocumentInfoJudgment);
                    newDocumentInfoJudgment.ScriptoriumId = masterEntity.ScriptoriumId;
                    newDocumentInfoJudgment.Ilm = DocumentConstants.CreateIlm;
                    newDocumentInfoJudgment.RecordDate = _dateTimeService.CurrentDateTime;
                    masterEntity.DocumentInfoJudgement = newDocumentInfoJudgment;

                }
                else if (request.DocumentInfoJudgment.IsDirty)
                {
                    DocumentInfoJudgement updatingDocumentInfoJudgment = masterEntity.DocumentInfoJudgement;
                    RelatedDocumentMapper.ToEntity(ref updatingDocumentInfoJudgment, request.DocumentInfoJudgment);
                    updatingDocumentInfoJudgment.Ilm = DocumentConstants.UpdateIlm;
                }
                else
                {
                    DocumentInfoJudgement updatingDocumentInfoJudgment = masterEntity.DocumentInfoJudgement;
                }





            }
            #endregion
            #region RelatedDocumentInfoText
            if (request.DocumentInfoText != null)
            {
                if (request.DocumentInfoText.IsNew)
                {
                    DocumentInfoText newDocumentInfoText = new();
                    RelatedDocumentMapper.ToEntity(ref newDocumentInfoText, request.DocumentInfoText);
                    newDocumentInfoText.ScriptoriumId = masterEntity.ScriptoriumId;
                    newDocumentInfoText.Ilm = DocumentConstants.CreateIlm;
                    newDocumentInfoText.RecordDate = _dateTimeService.CurrentDateTime;
                    masterEntity.DocumentInfoText = newDocumentInfoText;

                }

                else if (request.DocumentInfoText.IsDirty)
                {
                    DocumentInfoText updatingDocumentInfoText = masterEntity.DocumentInfoText;
                    RelatedDocumentMapper.ToEntity(ref updatingDocumentInfoText, request.DocumentInfoText);
                    updatingDocumentInfoText.Ilm = DocumentConstants.UpdateIlm;

                }

                else
                {
                    DocumentInfoText updatingDocumentInfoText = masterEntity.DocumentInfoText;
                }
            }
            #endregion
            #region RegisterCount

            if (request.RegisterCount != null)
            {
                if (masterEntity.DocumentInfoOther != null)
                {
                    DocumentInfoOther updatingDocumentInfoOther = masterEntity.DocumentInfoOther;
                    updatingDocumentInfoOther.RegisterCount = request.RegisterCount.ToNullableInt();
                    updatingDocumentInfoOther.Ilm = DocumentConstants.UpdateIlm;

                }
                else
                {
                    DocumentInfoOther newDocumentInfoOther = new();
                    newDocumentInfoOther.Id = Guid.NewGuid();
                    newDocumentInfoOther.RecordDate = _dateTimeService.CurrentDateTime;
                    newDocumentInfoOther.RegisterCount = request.RegisterCount.ToNullableInt();
                    newDocumentInfoOther.ScriptoriumId = masterEntity.ScriptoriumId;
                    newDocumentInfoOther.Ilm = DocumentConstants.CreateIlm;
                    newDocumentInfoOther.DocumentId = masterEntity.Id;
                }
            }

            #endregion
            #region RelatedDocumentPerson

            if (request?.DocumentPerson != null)
            {
                var RelatedpersoId = request.DocumentPerson?.PersonId.FirstOrDefault();
                var Person = RelatedpersoId != null ? _documentPersonRepository.GetById(RelatedpersoId.ToGuid()) : null;
                if (Person != null)
                {
                    if (request.DocumentPerson.IsNew && Person.DocumentId != masterEntity.Id)
                    {
                        DocumentPerson documentPerson = new();
                        RelatedDocumentMapper.MapToRelatedDocumentPerson(ref documentPerson, request.DocumentPerson);
                        documentPerson.Id = Guid.NewGuid();
                        documentPerson.NationalNo = Person.NationalNo;
                        documentPerson.BirthDate = Person.BirthDate;
                        documentPerson.IsIranian = Person.IsIranian;
                        documentPerson.IsRelated = Person.IsRelated;
                        documentPerson.Name = Person.Name;
                        documentPerson.Family = Person.Family;
                        documentPerson.IsOriginal = Person.IsOriginal;
                        documentPerson.SexType = Person.SexType;
                        documentPerson.DocumentId = masterEntity.Id;
                        documentPerson.RowNo = 1;
                        documentPerson.ScriptoriumId = masterEntity.ScriptoriumId;
                        documentPerson.NationalityId = Person.NationalityId;
                        documentPerson.BirthYear = Person.BirthYear;
                        documentPerson.FatherName = Person.FatherName;
                        documentPerson.IdentityIssueLocation = Person.IdentityIssueLocation;
                        documentPerson.IdentityNo = Person.IdentityNo;
                        documentPerson.SeriAlpha = Person.SeriAlpha;
                        documentPerson.Seri = Person.Seri;
                        documentPerson.Serial = Person.Serial;
                        documentPerson.SanaState = Person.SanaState;
                        documentPerson.MobileNoState = Person.MobileNoState;
                        documentPerson.IsSabtahvalChecked = Person.IsSabtahvalChecked;
                        documentPerson.IsSabtahvalCorrect = Person.IsSabtahvalCorrect;
                        documentPerson.IsAlive = Person.IsAlive;
                        documentPerson.PersonType = Person.PersonType;
                        documentPerson.CompanyTypeId = Person.CompanyTypeId;
                        documentPerson.LegalpersonNatureId = Person.LegalpersonNatureId;
                        documentPerson.LegalpersonTypeId = Person.LegalpersonTypeId;
                        documentPerson.IdentityIssueGeoLocationId = Person.IdentityIssueGeoLocationId;
                        documentPerson.DocumentPersonTypeId = Person.DocumentPersonTypeId;
                        documentPerson.Email = Person.Email;
                        documentPerson.AddressType = Person.AddressType;
                        documentPerson.FaxNo = Person.FaxNo;
                        documentPerson.Description = Person.Description;
                        documentPerson.PassportNo = Person.PassportNo;
                        documentPerson.IsFingerprintGotten = Person.IsFingerprintGotten;
                        documentPerson.Ilm = DocumentConstants.CreateIlm;

                        masterEntity.DocumentPeople.Add(documentPerson);
                    }
                    if (request.DocumentPerson.IsDirty && Person.DocumentId == masterEntity.Id)
                    {

                        DocumentPerson updatingDocumentPerson = masterEntity.DocumentPeople.FirstOrDefault();
                        RelatedDocumentMapper.MapToRelatedDocumentPerson(ref updatingDocumentPerson, request.DocumentPerson);
                        updatingDocumentPerson.Ilm = DocumentConstants.UpdateIlm;
                    }
                }
            }
            #endregion
            #region RelatedDocumentEstate

            if (request.DocumentEstateId?.Count > 0)
            {

                var RelatedEstateId = request.DocumentEstateId.FirstOrDefault();
                var Estate = RelatedEstateId != null ? _documentEstateRepository.GetById(RelatedEstateId.ToGuid()) : null;
                if (Estate != null)
                {

                    if (Estate.DocumentId == masterEntity.Id)
                    {
                        DocumentEstate updatingDocumentEstate = masterEntity.DocumentEstates.FirstOrDefault();
                        if (updatingDocumentEstate != null)
                        {
                            updatingDocumentEstate.IsAttachment = Estate.IsAttachment;
                            updatingDocumentEstate.AttachmentType = Estate.AttachmentType;
                            updatingDocumentEstate.AttachmentTypeOthers = Estate.AttachmentTypeOthers;
                            updatingDocumentEstate.DivFromBasicPlaque = Estate.DivFromBasicPlaque;
                            updatingDocumentEstate.DivFromSecondaryPlaque = Estate.DivFromSecondaryPlaque;
                            updatingDocumentEstate.BasicPlaque = Estate.BasicPlaque;
                            updatingDocumentEstate.SecondaryPlaque = Estate.SecondaryPlaque;
                            updatingDocumentEstate.EvacuatedDate = request.EvacuatedDate;
                            updatingDocumentEstate.ScriptoriumId = Estate.ScriptoriumId;
                            updatingDocumentEstate.Ilm = DocumentConstants.UpdateIlm;
                        }
                    }
                    else
                    {
                        DocumentEstate documentEstate = new();
                        documentEstate.Id = Guid.NewGuid();
                        documentEstate.IsAttachment = Estate.IsAttachment;
                        documentEstate.DocumentId = Estate.DocumentId;
                        documentEstate.AttachmentType = Estate.AttachmentType;
                        documentEstate.AttachmentTypeOthers = Estate.AttachmentTypeOthers;
                        documentEstate.DivFromBasicPlaque = Estate.DivFromBasicPlaque;
                        documentEstate.DivFromSecondaryPlaque = Estate.DivFromSecondaryPlaque;
                        documentEstate.BasicPlaque = Estate.BasicPlaque;
                        documentEstate.SecondaryPlaque = Estate.SecondaryPlaque;
                        documentEstate.EvacuatedDate = request.EvacuatedDate;
                        documentEstate.ScriptoriumId = Estate.ScriptoriumId;
                        documentEstate.Ilm = DocumentConstants.CreateIlm;
                        masterEntity.DocumentEstates.Add(documentEstate);
                    }
                }
            }
            #endregion
        }
        private void BusinessValidation(UpdateRelatedDocumentCommand request)
        {
            if (!request.IsNew && request.IsDirty)
            {
                var docType = request.DocumentTypeId.FirstOrDefault();

                if (docType == "0023" || docType == "0024" || docType == "0035")
                {
                    if (request.DocumentInfoJudgment != null && (request.DocumentInfoJudgment.IsDirty))
                    {
                        if (request.DocumentInfoJudgment.IssueNo.IsNullOrEmpty()) apiResult.message.Add("مقدار شماره دادنامه اجباری میباشد");
                        if (request.DocumentInfoJudgment.IssueDate.IsNullOrEmpty()) apiResult.message.Add("مقدار تاریخ دادنامه اجباری میباشد");
                        if (request.DocumentInfoJudgment.CaseClassifyNo.IsNullOrEmpty()) apiResult.message.Add("مقدار کلاسه پرونده اجباری میباشد");
                        if (request.DocumentInfoJudgment.IssuerName.IsNullOrEmpty()) apiResult.message.Add("مقدار مرجع صدور اجباری میباشد");
                    }

                    if (request.DocumentInfoText != null && (request.DocumentInfoText.IsDirty))
                    {
                        if (request.DocumentInfoText.DocumentDescription.IsNullOrEmpty()) apiResult.message.Add("مقدار توضیحات در مورد حکم دادگاه اجباری میباشد");
                    }

                    if (request.BookVolumeNo.IsNullOrEmpty()) apiResult.message.Add("مقدار شماره جلد دفتر اندیکاتور اجباری میباشد");
                    if (request.BookPapersNo.IsNullOrEmpty()) apiResult.message.Add("مقدار شماره صفحات دفتر اندیکاتور اجباری میباشد");
                    if (request.ClassifyNo.IsNullOrEmpty()) apiResult.message.Add("مقدار شماره ثبت دفتر اندیکاتور اجباری میباشد");
                    if (request.WriteInBookDate.IsNullOrEmpty()) apiResult.message.Add("مقدار تاریخ ورود به دفتر اندیکاتور اجباری میباشد");
                }
                else if (docType == "0025" && request.IsRequestInSsar == true)
                {
                    if (request.DocumentInfoJudgment != null && (request.DocumentInfoJudgment.IsDirty))
                    {
                        if (request.DocumentInfoJudgment.IssueNo.IsNullOrEmpty()) apiResult.message.Add("مقدار شماره دادنامه اجباری میباشد");
                        if (request.DocumentInfoJudgment.IssueDate.IsNullOrEmpty()) apiResult.message.Add("مقدار تاریخ دادنامه اجباری میباشد");
                        if (request.DocumentInfoJudgment.IssuerName.IsNullOrEmpty()) apiResult.message.Add("مقدار مرجع صدور اجباری میباشد");
                    }
                }
                else if (docType == "0026")
                {
                    if (request.DocumentInfoJudgment != null && (request.DocumentInfoJudgment.IsDirty))
                    {
                        if (request.DocumentInfoJudgment.IssueNo.IsNullOrEmpty()) apiResult.message.Add("مقدار شماره دادنامه اجباری میباشد");
                        if (request.DocumentInfoJudgment.IssueDate.IsNullOrEmpty()) apiResult.message.Add("مقدار تاریخ دادنامه اجباری میباشد");
                        if (request.DocumentInfoJudgment.IssuerName.IsNullOrEmpty()) apiResult.message.Add("مقدار مرجع صدور اجباری میباشد");
                    }
                }
                else if (docType == "0027" && request.IsRequestInSsar==true)
                {
                    if (request.DocumentPerson != null && (request.DocumentPerson.IsDirty))
                    {
                        if (request.DocumentPerson.PostalCode.IsNullOrEmpty()) apiResult.message.Add("مقدار کد پستی اجباری میباشد");
                        if (request.DocumentPerson.Address.IsNullOrEmpty()) apiResult.message.Add("مقدار نشانی اجباری میباشد");
                    }
                }
                else if (docType == "0028")
                {
                    if (request.DocumentInfoJudgment != null && (request.DocumentInfoJudgment.IsDirty))
                    {
                        if (request.DocumentInfoJudgment.IssueNo.IsNullOrEmpty()) apiResult.message.Add("مقدار شماره دادنامه اجباری میباشد");
                        if (request.DocumentInfoJudgment.IssueDate.IsNullOrEmpty()) apiResult.message.Add("مقدار تاریخ دادنامه اجباری میباشد");
                        if (request.DocumentInfoJudgment.CaseClassifyNo.IsNullOrEmpty()) apiResult.message.Add("مقدار کلاسه پرونده اجباری میباشد");
                        if (request.DocumentInfoJudgment.IssuerName.IsNullOrEmpty()) apiResult.message.Add("مقدار مرجع صدور اجباری میباشد");
                    }

                    if (request.DocumentInfoText != null && ( request.DocumentInfoText.IsDirty))
                    {
                        if (request.DocumentInfoText.DocumentDescription.IsNullOrEmpty()) apiResult.message.Add("مقدار توضیحات در مورد حکم دادگاه اجباری میباشد");
                    }

                    if (request.BookVolumeNo.IsNullOrEmpty()) apiResult.message.Add("مقدار شماره جلد دفتر اندیکاتور اجباری میباشد");
                    if (request.BookPapersNo.IsNullOrEmpty()) apiResult.message.Add("مقدار شماره صفحات دفتر اندیکاتور اجباری میباشد");
                    if (request.ClassifyNo.IsNullOrEmpty()) apiResult.message.Add("مقدار شماره ثبت دفتر اندیکاتور اجباری میباشد");
                    if (request.WriteInBookDate.IsNullOrEmpty()) apiResult.message.Add("مقدار تاریخ ورود به دفتر اندیکاتور اجباری میباشد");
                }
                else if (docType == "0030" || docType == "0031" || docType == "0032")
                {
                    if (request.BookVolumeNo.IsNullOrEmpty()) apiResult.message.Add("مقدار شماره جلد دفتر اندیکاتور اجباری میباشد");
                    if (request.BookPapersNo.IsNullOrEmpty()) apiResult.message.Add("مقدار شماره صفحات دفتر اندیکاتور اجباری میباشد");
                    if (request.ClassifyNo.IsNullOrEmpty()) apiResult.message.Add("مقدار شماره ثبت دفتر اندیکاتور اجباری میباشد");
                    if (request.WriteInBookDate.IsNullOrEmpty()) apiResult.message.Add("مقدار تاریخ ورود به دفتر اندیکاتور اجباری میباشد");
                }
                else if (docType == "0033")
                {
                    if (request.DocumentInfoJudgment != null && (request.DocumentInfoJudgment.IsDirty))
                    {
                        if (request.DocumentInfoJudgment.CaseClassifyNo.IsNullOrEmpty()) apiResult.message.Add("مقدار شماره قبوض اجباری میباشد");
                    }
                    if (request.DocumentInfoText != null && (request.DocumentInfoText.IsDirty))
                    {
                        if (request.DocumentInfoText.Description.IsNullOrEmpty()) apiResult.message.Add("مقدار مشخصات دیون/داین/ضامن/توضیحات اجباری میباشد");
                    }
                    if (request.Price.IsNullOrEmpty()) apiResult.message.Add("مقدار مبلغ اجباری میباشد");
                    if (request.RegisterCount.IsNullOrEmpty()) apiResult.message.Add("مقدار تعداد برگ اجباری میباشد");
                }
            }

            if (apiResult.message.Count > 0)
            {
                apiResult.statusCode = ApiResultStatusCode.BadRequest;
                apiResult.IsSuccess = false;
            }
        }

    }
}
