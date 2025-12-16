using MediatR;
using Serilog;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Commands.RelatedDocument;
using Notary.SSAA.BO.DataTransferObject.Commands.SignRequest;
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


namespace Notary.SSAA.BO.CommandHandler.RelatedDocument.Handlers
{
    public class CreateRelatedDocumentCommandHandler : BaseCommandHandler<CreateRelatedDocumentCommand, ApiResult>
    {
        private Domain.Entities.Document masterEntity;
        private readonly IDateTimeService _dateTimeService;
        private readonly IDocumentRepository _documentRepository;
        private readonly IDocumentPersonRepository _documentPersonRepository;
        private readonly IRepository<DocumentEstate> _documentEstateRepository;
        private ApiResult<DocumentDetailViewModel> apiResult;
        public CreateRelatedDocumentCommandHandler(IMediator mediator, IUserService userService, ILogger logger, IDateTimeService dateTimeService, IDocumentRepository documentRepository, IDocumentPersonRepository documentPersonRepository, IRepository<DocumentEstate> documentEstateRepository) : base(mediator, userService, logger)
        {
            masterEntity = new();
            _dateTimeService = dateTimeService;
            _documentRepository = documentRepository;
            apiResult = new();
            _documentPersonRepository = documentPersonRepository;
            _documentEstateRepository = documentEstateRepository;
        }

        protected override bool HasAccess(CreateRelatedDocumentCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }
        protected async override Task<ApiResult> ExecuteAsync(CreateRelatedDocumentCommand request, CancellationToken cancellationToken)
        {
            //BusinessValidation(request);
            if (apiResult.IsSuccess)
            {
                await UpdateDatabase(request, cancellationToken);
                await _documentRepository.AddAsync(masterEntity, cancellationToken);

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
        private async Task UpdateDatabase(CreateRelatedDocumentCommand request, CancellationToken cancellationToken)
        {
            masterEntity = new()
            {
                Id = Guid.NewGuid(),
                RequestDate = _dateTimeService.CurrentPersianDate,
                RecordDate = _dateTimeService.CurrentDateTime,
                ScriptoriumId = _userService.UserApplicationContext.BranchAccess.BranchCode,
                RequestNo = await CreateDocumentNo(_userService.UserApplicationContext.BranchAccess.BranchCode, cancellationToken),
                Ilm = DocumentConstants.CreateIlm,
                RequestTime = _dateTimeService.CurrentTime,
                State = "1",
            };
            RelatedDocumentMapper.ToEntity(ref masterEntity, request);
            if (request.DocumentInfoJudgment != null)
            {
                if (request.DocumentInfoJudgment.IsNew)
                {
                    DocumentInfoJudgement documentInfoJudgment = new();
                    RelatedDocumentMapper.ToEntity(ref documentInfoJudgment, request.DocumentInfoJudgment);
                    documentInfoJudgment.DocumentId = masterEntity.Id;
                    documentInfoJudgment.ScriptoriumId = masterEntity.ScriptoriumId;
                    documentInfoJudgment.Ilm = DocumentConstants.CreateIlm;
                    documentInfoJudgment.RecordDate = _dateTimeService.CurrentDateTime;
                    masterEntity.DocumentInfoJudgement = documentInfoJudgment;
                }

            }
            if (request.DocumentInfoText != null)
            {
                if (request.DocumentInfoText.IsNew)
                {
                    DocumentInfoText documentInfoText = new();
                    RelatedDocumentMapper.ToEntity(ref documentInfoText, request.DocumentInfoText);
                    documentInfoText.DocumentId = masterEntity.Id;
                    documentInfoText.ScriptoriumId = masterEntity.ScriptoriumId;
                    documentInfoText.RecordDate = _dateTimeService.CurrentDateTime;
                    documentInfoText.Ilm = DocumentConstants.CreateIlm;

                    masterEntity.DocumentInfoText = documentInfoText;
                }

            }
            if (request.RegisterCount != null)
            {
                DocumentInfoOther documentInfoOther = new();
                documentInfoOther.Id = Guid.NewGuid();
                documentInfoOther.RegisterCount = request.RegisterCount.ToNullableInt();
                documentInfoOther.DocumentId = masterEntity.Id;
                documentInfoOther.ScriptoriumId = masterEntity.ScriptoriumId;
                documentInfoOther.RecordDate = _dateTimeService.CurrentDateTime;
                documentInfoOther.Ilm = DocumentConstants.CreateIlm;

                masterEntity.DocumentInfoOther = documentInfoOther;
            }

            if (request.DocumentPerson != null)
            {

                var RelatedpersoId = request.DocumentPerson?.PersonId.FirstOrDefault();
                var Person = RelatedpersoId != null ? _documentPersonRepository.GetById(RelatedpersoId.ToGuid()) : null;
                if (Person != null && request.DocumentPerson.IsNew)
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

            }

            if (request.DocumentEstateId?.Count > 0)
            {

                var RelatedEstateId = request.DocumentEstateId.FirstOrDefault();
                var Estate = RelatedEstateId != null ? _documentEstateRepository.GetById(RelatedEstateId.ToGuid()) : null;
                if (Estate != null)
                {
                    DocumentEstate documentEstate = new();
                    documentEstate.Id = Guid.NewGuid();
                    documentEstate.IsAttachment = Estate.IsAttachment;
                    documentEstate.ScriptoriumId = Estate.ScriptoriumId;
                    documentEstate.AttachmentType = Estate.AttachmentType;
                    documentEstate.AttachmentTypeOthers = Estate.AttachmentTypeOthers;
                    documentEstate.DivFromBasicPlaque = Estate.DivFromBasicPlaque;
                    documentEstate.DivFromSecondaryPlaque = Estate.DivFromSecondaryPlaque;
                    documentEstate.BasicPlaque = Estate.BasicPlaque;
                    documentEstate.SecondaryPlaque = Estate.SecondaryPlaque;
                    documentEstate.EvacuatedDate = request.EvacuatedDate;
                    documentEstate.Ilm = DocumentConstants.CreateIlm;
                    masterEntity.DocumentEstates.Add(documentEstate);
                }
            }
        }
        private void BusinessValidation(CreateRelatedDocumentCommand request)
        {

            if (request.IsNew)
            {
                if (request.DocumentTypeId.First() == "0023" ||
                    request.DocumentTypeId.First() == "0024" ||
                     request.DocumentTypeId.First() == "0035")
                {
                    if (request.DocumentInfoJudgment != null &&
                        (request.DocumentInfoJudgment.IsDirty))
                    {
                        if (request.DocumentInfoJudgment.IssueNo.IsNullOrEmpty())
                            apiResult.message.Add("مقدار شماره دادنامه اجباری میباشد");
                        if (request.DocumentInfoJudgment.IssueDate.IsNullOrEmpty())
                            apiResult.message.Add("مقدار تاریخ دادنامه اجباری میباشد");
                        if (request.DocumentInfoJudgment.CaseClassifyNo.IsNullOrEmpty())
                            apiResult.message.Add("مقدار کلاسه پرونده اجباری میباشد");
                        if (request.DocumentInfoJudgment.IssuerName.IsNullOrEmpty())
                            apiResult.message.Add("مقدار مرجع صدور اجباری میباشد");

                        if (request.DocumentInfoText != null &&
                            (request.DocumentInfoText.IsDirty))
                        {
                            if (request.DocumentInfoText.DocumentDescription.IsNullOrEmpty())
                                apiResult.message.Add("مقدار توضیحات در مورد حکم دادگاه اجباری میباشد");
                        }

                        if (request.BookVolumeNo.IsNullOrEmpty())
                            apiResult.message.Add("مقدار شماره جلد دفتر اندیکاتور اجباری میباشد");
                        if (request.BookPapersNo.IsNullOrEmpty())
                            apiResult.message.Add("مقدار شماره صفحات دفتر اندیکاتور اجباری میباشد");
                        if (request.ClassifyNo.IsNullOrEmpty())
                            apiResult.message.Add("مقدار شماره ثبت دفتر اندیکاتور اجباری میباشد");
                        if (request.WriteInBookDate.IsNullOrEmpty())
                            apiResult.message.Add("مقدار تاریخ ورود به دفتر اندیکاتور اجباری میباشد");
                    }
                }
                else if (request.DocumentTypeId.First() == "0025")
                {
                    if (request.IsRequestInSsar == true)
                    {
                        if (request.DocumentInfoJudgment != null &&
                            (request.DocumentInfoJudgment.IsDirty))
                        {
                            if (request.DocumentInfoJudgment.IssueNo.IsNullOrEmpty())
                                apiResult.message.Add("مقدار شماره دادنامه اجباری میباشد");
                            if (request.DocumentInfoJudgment.IssueDate.IsNullOrEmpty())
                                apiResult.message.Add("مقدار تاریخ دادنامه اجباری میباشد");
                            if (request.DocumentInfoJudgment.IssuerName.IsNullOrEmpty())
                                apiResult.message.Add("مقدار مرجع صدور اجباری میباشد");
                        }
                    }
                }
                else if (request.DocumentTypeId.First() == "0026")
                {
                    if (request.DocumentInfoJudgment != null &&
                        (request.DocumentInfoJudgment.IsDirty))
                    {
                        if (request.DocumentInfoJudgment.IssueNo.IsNullOrEmpty())
                            apiResult.message.Add("مقدار شماره دادنامه اجباری میباشد");
                        if (request.DocumentInfoJudgment.IssueDate.IsNullOrEmpty())
                            apiResult.message.Add("مقدار تاریخ دادنامه اجباری میباشد");
                        if (request.DocumentInfoJudgment.IssuerName.IsNullOrEmpty())
                            apiResult.message.Add("مقدار مرجع صدور اجباری میباشد");
                    }
                }
                else if (request.DocumentTypeId.First() == "0027")
                {
                    if (request.IsRequestInSsar == true)
                    {
                        if (request.DocumentPerson != null &&
                            (request.DocumentPerson.IsDirty))
                        {
                            if (request.DocumentPerson.PostalCode.IsNullOrEmpty())
                                apiResult.message.Add("مقدار کد پستی اجباری میباشد");
                            if (request.DocumentPerson.Address.IsNullOrEmpty())
                                apiResult.message.Add("مقدار نشانی اجباری میباشد");
                        }
                    }
                }
                else if (request.DocumentTypeId.First() == "0028")
                {
                    if (request.DocumentInfoJudgment != null &&
                        (request.DocumentInfoJudgment.IsDirty))
                    {
                        if (request.DocumentInfoJudgment.IssueNo.IsNullOrEmpty())
                            apiResult.message.Add("مقدار شماره دادنامه اجباری میباشد");
                        if (request.DocumentInfoJudgment.IssueDate.IsNullOrEmpty())
                            apiResult.message.Add("مقدار تاریخ دادنامه اجباری میباشد");
                        if (request.DocumentInfoJudgment.CaseClassifyNo.IsNullOrEmpty())
                            apiResult.message.Add("مقدار کلاسه پرونده اجباری میباشد");
                        if (request.DocumentInfoJudgment.IssuerName.IsNullOrEmpty())
                            apiResult.message.Add("مقدار مرجع صدور اجباری میباشد");

                        if (request.DocumentInfoText != null &&
                            (request.DocumentInfoText.IsDirty))
                        {
                            if (request.DocumentInfoText.DocumentDescription.IsNullOrEmpty())
                                apiResult.message.Add("مقدار توضیحات در مورد حکم دادگاه اجباری میباشد");
                        }

                        if (request.BookVolumeNo.IsNullOrEmpty())
                            apiResult.message.Add("مقدار شماره جلد دفتر اندیکاتور اجباری میباشد");
                        if (request.BookPapersNo.IsNullOrEmpty())
                            apiResult.message.Add("مقدار شماره صفحات دفتر اندیکاتور اجباری میباشد");
                        if (request.ClassifyNo.IsNullOrEmpty())
                            apiResult.message.Add("مقدار شماره ثبت دفتر اندیکاتور اجباری میباشد");
                        if (request.WriteInBookDate.IsNullOrEmpty())
                            apiResult.message.Add("مقدار تاریخ ورود به دفتر اندیکاتور اجباری میباشد");
                    }
                }
                else if (request.DocumentTypeId.First() == "0030" ||
                         request.DocumentTypeId.First() == "0031" ||
                         request.DocumentTypeId.First() == "0032")
                {
                    if (request.BookVolumeNo.IsNullOrEmpty())
                        apiResult.message.Add("مقدار شماره جلد دفتر اندیکاتور اجباری میباشد");
                    if (request.BookPapersNo.IsNullOrEmpty())
                        apiResult.message.Add("مقدار شماره صفحات دفتر اندیکاتور اجباری میباشد");
                    if (request.ClassifyNo.IsNullOrEmpty())
                        apiResult.message.Add("مقدار شماره ثبت دفتر اندیکاتور اجباری میباشد");
                    if (request.WriteInBookDate.IsNullOrEmpty())
                        apiResult.message.Add("مقدار تاریخ ورود به دفتر اندیکاتور اجباری میباشد");
                }
                else if (request.DocumentTypeId.First() == "0033")
                {
                    if (request.DocumentInfoJudgment != null &&
                        (request.DocumentInfoJudgment.IsDirty))
                    {
                        if (request.DocumentInfoJudgment.CaseClassifyNo.IsNullOrEmpty())
                            apiResult.message.Add("مقدار شماره قبوض اجباری میباشد");
                    }

                    if (request.DocumentInfoText != null &&
                        (request.DocumentInfoText.IsDirty))
                    {
                        if (request.DocumentInfoText.Description.IsNullOrEmpty())
                            apiResult.message.Add("مقدار مشخصات دیون/داین/ضامن/توضیحات اجباری میباشد");
                    }

                    if (request.Price.IsNullOrEmpty())
                        apiResult.message.Add("مقدار مبلغ اجباری میباشد");
                    if (request.RegisterCount.IsNullOrEmpty())
                        apiResult.message.Add("مقدار تعداد برگ اجباری میباشد");
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
