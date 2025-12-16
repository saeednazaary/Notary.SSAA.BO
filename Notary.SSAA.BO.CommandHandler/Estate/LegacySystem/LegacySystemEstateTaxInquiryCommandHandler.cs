using MediatR;
using Microsoft.Extensions.Configuration;
using Serilog;
using Notary.SSAA.BO.CommandHandler.Base;
using Notary.SSAA.BO.DataTransferObject.Commands.Estate.LegacySystem;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.Media;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Estate.BaseInfoService;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.Coordinator.Estate.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.CommandHandler.Estate.LegacySystem
{
    public class LegacySystemEstateTaxInquiryCommandHandler : BaseExternalCommandHandler<EstateTaxInquiryLegacySystemCommand, ExternalApiResult>
    {
        private readonly IEstateInquiryRepository _estateInquiryRepository;
        private readonly IEstateTaxInquiryRepository _estateTaxInquiryRepository;
        private readonly IEstateTaxInquiryUsingTypeRepository _estateTaxInquiryUsingTypeRepository;
        private readonly IEstateTaxCityRepository _estateTaxCityRepository;
        private readonly IEstateTaxCountyRepository _estateTaxCountyRepository;
        private readonly IEstateTaxProvinceRepository _estateTaxProvinceRepository;
        private readonly IEstateTaxInquiryBuildingTypeRepository _estateTaxInquiryBuildingTypeRepository;
        private readonly IEstateTaxInquiryBuildingStatusRepository  _estateTaxInquiryBuildingStatusRepository;
        private readonly IEstateTaxInquiryBuildingConstructionStepRepository  _estateTaxInquiryBuildingConstructionStepRepository;
        private readonly IEstateTaxInquiryDocumentRequestTypeRepository _estateTaxInquiryDocumentRequestTypeRepository;
        private readonly IEstateTaxInquiryFieldTypeRepository _estateTaxInquiryFieldTypeRepository;
        private readonly IEstateTaxInquiryLocationAssignRigthOwnershipTypeRepository _estateTaxInquiryLocationAssignRigthOwnershipTypeRepository;
        private readonly IEstateTaxInquiryLocationAssignRigthUsingTypeRepository _estateTaxInquiryLocationAssignRigthUsingTypeRepository;
        private readonly IWorkfolwStateRepository _workfolwStateRepository;
        private readonly IRepository<Domain.Entities.EstateTaxUnit> _estateTaxUnitRepository;
        private readonly IEstatePieceTypeRepository _estatePieceTypeRepository;
        private readonly IRepository<EstateTaxInquirySendedSm> _EstateTaxInquirySendedSmRepository;
        private readonly IRepository<ConfigurationParameter> _configurationParameterRepository;
        private readonly IRepository<SsrApiExternalUser> _ssrApiExternalUser;
        private readonly IDateTimeService _dateTimeService;
        private readonly IConfiguration _configuration;
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly BaseInfoServiceHelper _baseInfoServiceHelper;
        private readonly IRepository<EstateSection> _sectionRepository;
        EstateTaxInquiryLegacySystemCommand _request = null;
        string GScriptoriumId = "";
        public LegacySystemEstateTaxInquiryCommandHandler(IMediator mediator, IUserService userService, ILogger logger,
            IEstateInquiryRepository estateInquiryRepository,
            IEstateTaxInquiryRepository estateTaxInquiryRepository,
            IEstateTaxInquiryUsingTypeRepository estateTaxInquiryUsingTypeRepository,
            IEstateTaxCityRepository estateTaxCityRepository,
            IEstateTaxCountyRepository estateTaxCountyRepository,
            IEstateTaxProvinceRepository estateTaxProvinceRepository,
            IEstateTaxInquiryBuildingTypeRepository estateTaxInquiryBuildingTypeRepository,
            IEstateTaxInquiryBuildingStatusRepository estateTaxInquiryBuildingStatusRepository,
            IEstateTaxInquiryBuildingConstructionStepRepository estateTaxInquiryBuildingConstructionStepRepository,
            IEstateTaxInquiryDocumentRequestTypeRepository estateTaxInquiryDocumentRequestTypeRepository,
            IEstateTaxInquiryFieldTypeRepository estateTaxInquiryFieldTypeRepository,
            IEstateTaxInquiryLocationAssignRigthOwnershipTypeRepository estateTaxInquiryLocationAssignRigthOwnershipTypeRepository,
            IEstateTaxInquiryLocationAssignRigthUsingTypeRepository estateTaxInquiryLocationAssignRigthUsingTypeRepository,
            IWorkfolwStateRepository  workfolwStateRepository,
            IRepository<Domain.Entities.EstateTaxUnit> estateTaxUnitRepository,
            IEstatePieceTypeRepository estatePieceTypeRepository,
            IDateTimeService dateTimeService,
            IConfiguration configuration,
            IHttpEndPointCaller httpEndPointCaller,
            IRepository<EstateTaxInquirySendedSm> EstateTaxInquirySendedSmRepository,
            IRepository<ConfigurationParameter> configurationParameterRepository,
            IRepository<SsrApiExternalUser> ssrApiExternalUser,
            IRepository<EstateSection> sectionRepository
            )
            : base(mediator, userService, logger)
        {

            _estateTaxInquiryRepository = estateTaxInquiryRepository;
            
            _dateTimeService = dateTimeService;
            _configuration = configuration;
            _httpEndPointCaller = httpEndPointCaller;
            _estateInquiryRepository = estateInquiryRepository;
            _estateTaxInquiryUsingTypeRepository = estateTaxInquiryUsingTypeRepository;
            _estateTaxCityRepository = estateTaxCityRepository;
            _estateTaxCountyRepository = estateTaxCountyRepository;
            _estateTaxProvinceRepository = estateTaxProvinceRepository;
            _estateTaxInquiryBuildingTypeRepository = estateTaxInquiryBuildingTypeRepository;
            _estateTaxInquiryBuildingStatusRepository = estateTaxInquiryBuildingStatusRepository;
            _estateTaxInquiryBuildingConstructionStepRepository = estateTaxInquiryBuildingConstructionStepRepository;
            _estateTaxInquiryDocumentRequestTypeRepository = estateTaxInquiryDocumentRequestTypeRepository;
            _estateTaxInquiryFieldTypeRepository = estateTaxInquiryFieldTypeRepository;
            _estateTaxInquiryLocationAssignRigthOwnershipTypeRepository = estateTaxInquiryLocationAssignRigthOwnershipTypeRepository;
            _estateTaxInquiryLocationAssignRigthUsingTypeRepository = estateTaxInquiryLocationAssignRigthUsingTypeRepository;
            _workfolwStateRepository = workfolwStateRepository;
            _estateTaxUnitRepository = estateTaxUnitRepository;
            _estatePieceTypeRepository = estatePieceTypeRepository;
            _EstateTaxInquirySendedSmRepository = EstateTaxInquirySendedSmRepository;
            _configurationParameterRepository = configurationParameterRepository;
            _sectionRepository = sectionRepository;
            _ssrApiExternalUser = ssrApiExternalUser;
            _baseInfoServiceHelper = new BaseInfoServiceHelper(mediator);
            //_configurationParameterHelper = new ConfigurationParameterHelper(configurationParameterRepository, mediator);
        }
        protected override bool HasAccess(EstateTaxInquiryLegacySystemCommand request, IList<string> userRoles)
        {
            return true;
        }
        int nextOrder = int.MaxValue;
        protected async override Task<ExternalApiResult> ExecuteAsync(EstateTaxInquiryLegacySystemCommand request, CancellationToken cancellationToken)
        {
            this._request = request;
            ExternalApiResult apiResult = new() { ResCode = "1", ResMessage = SystemMessagesConstant.Operation_Successful };
            var user = await _ssrApiExternalUser.TableNoTracking.Where(x => x.UserName == request.UserName && x.UserPassword == request.Password).FirstOrDefaultAsync(cancellationToken);
            if (user == null)
            {
                apiResult.ResCode = "104";
                apiResult.ResMessage = "نام کاربری یا کلمه عبور استفاده کننده  اشتباه می باشد";
                return apiResult;
            }
            else if (request.UserName != "TaxUser")
            {
                apiResult.ResCode = "105";
                apiResult.ResMessage = "مجاز به استفاده از این سرویس نمی باشید";
                return apiResult;
            }
            try
            {
                if (request.Data.Count > 0)
                    request.Data = request.Data.OrderBy(x => x.OrderNo).ToList();

                var estateInquirylist = request.Data.Where(x => x.EntityName.Equals("estestateinquiry", StringComparison.OrdinalIgnoreCase)).ToList();
                if(estateInquirylist.Count > 0)
                {
                    EstateInquiryLegacySystemCommand estateInquiryLegacySystemCommand = new EstateInquiryLegacySystemCommand();
                    estateInquiryLegacySystemCommand.Data = request.Data;
                    await _mediator.Send(estateInquiryLegacySystemCommand, cancellationToken);
                }

                var list = request.Data.Where(x => x.EntityName.Equals("inquirytaxaffairs", StringComparison.OrdinalIgnoreCase)).ToList();
                var orders = list.Select(x => x.OrderNo).ToArray();
                int index = 0;
                foreach (var data in list)
                {
                    try
                    {
                        nextOrder = orders[index + 1];
                    }
                    catch(IndexOutOfRangeException)
                    {

                    }
                    bool commitChanges = false;
                    if (index == list.Count - 1)
                        commitChanges = true;
                    if (data.CommandType == 1 || data.CommandType == 2)
                    {
                        var estatetaxInquiryId = await SaveEstateTaxInquiry(data, commitChanges, cancellationToken);
                        if (estatetaxInquiryId == null)
                        {
                            apiResult.ResCode = "106";
                            apiResult.ResMessage = "ورودی نامعتبر می باشد";
                            return apiResult;
                        }
                        
                    }
                    else if (data.CommandType == 3)
                    {
                        await DeleteEstateTaxInquiry(data, commitChanges, cancellationToken);
                    }

                    index++;
                }
            }
            catch (Exception ex)
            {
                apiResult.ResCode = "2003";
                apiResult.ResMessage = "خطا در ثبت اطلاعات رخ داده است";
            }
            return apiResult;
        }

        private async Task<Domain.Entities.EstateInquiry> SetEstateInquiryRelatedProperties(EntityData data,Domain.Entities.EstateTaxInquiry estateTaxInquiry, CancellationToken cancellationToken)
        {
            var idData = data.FieldValues.Where(fv => fv.FieldName.Equals("estestateinquiryid", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (idData == null)
                return null;
            if (idData.Value == null)
                return null;

            var idValue = idData.Value.ToString();            
            var estateInquiry = await _estateInquiryRepository.GetAsync(x => x.Id == idValue.ToGuid() || x.LegacyId.Equals(idValue, StringComparison.OrdinalIgnoreCase), cancellationToken);
            await _estateInquiryRepository.LoadCollectionAsync(estateInquiry, x => x.EstateInquiryPeople,cancellationToken);
            estateTaxInquiry.EstateInquiryId = estateInquiry.Id;
            estateTaxInquiry.EstateUnitId = estateInquiry.UnitId;
            estateTaxInquiry.Estatebasic = estateInquiry.Basic;
            estateTaxInquiry.Estatesecondary = estateInquiry.Secondary;
            estateTaxInquiry.BasicRemaining= estateInquiry.BasicRemaining;
            estateTaxInquiry.SecondaryRemaining= estateInquiry.SecondaryRemaining;
            estateTaxInquiry.EstateSectionId = estateInquiry.EstateSectionId;
            estateTaxInquiry.EstateSubsectionId=estateInquiry.EstateSubsectionId;
            var estateInquiryOwner = estateInquiry.EstateInquiryPeople.First();
            var theOwner = estateTaxInquiry.EstateTaxInquiryPeople.Where(x => x.DealsummaryPersonRelateTypeId == "108").FirstOrDefault();
            if (theOwner == null)
            {
                theOwner = new EstateTaxInquiryPerson();
                theOwner.Id = Guid.NewGuid();
                theOwner.EstateTaxInquiryId = estateTaxInquiry.Id;                                
                theOwner.DealsummaryPersonRelateTypeId = "108";
                estateTaxInquiry.EstateTaxInquiryPeople.Add(theOwner);
            }
            var lst1 = estateInquiryOwner.GetType().GetProperties();
            var type = theOwner.GetType();
            foreach (var p1 in lst1)
            {
                if (p1.Name == "Id" || p1.Name == "id" || p1.Name == "ID")
                    continue;
                var p2 = type.GetProperty(p1.Name);
                if (p2 != null)
                {
                    p2.SetValue(theOwner, p1.GetValue(estateInquiryOwner, null));
                }
            }
           
            theOwner.LegacyId = estateInquiryOwner.LegacyId;
            theOwner.Timestamp = 1;
            theOwner.Serial = estateInquiryOwner.SerialNo;
            theOwner.IssuePlace = estateInquiryOwner.IssuePlaceText;
            return estateInquiry;
        }
        private async Task DeleteEstateTaxInquiry(EntityData data, bool commitChanges, CancellationToken cancellationToken)
        {
            var idValue = data.FieldValues.Where(fv => fv.FieldName.Equals("id", StringComparison.OrdinalIgnoreCase)).First().Value.ToString();
            var masterEntity = await _estateTaxInquiryRepository.GetAsync(x => x.Id == idValue.ToGuid() || x.LegacyId.Equals(idValue, StringComparison.OrdinalIgnoreCase) , cancellationToken);
            await _estateTaxInquiryRepository.LoadCollectionAsync(masterEntity, e => e.EstateTaxInquiryPeople, cancellationToken);
            await _estateTaxInquiryRepository.LoadCollectionAsync(masterEntity, e => e.EstateTaxInquiryAttaches, cancellationToken);

            masterEntity.EstateTaxInquiryPeople.Clear();
            masterEntity.EstateTaxInquiryAttaches.Clear();
            
            _estateTaxInquiryRepository.Delete(masterEntity,commitChanges);
        }

        GetGeolocationByIdViewModel geLocations = new GetGeolocationByIdViewModel();
        GetGeolocationByIdViewModel iranGeoLocation=new GetGeolocationByIdViewModel();
        private async Task<Guid?> SaveEstateTaxInquiry(EntityData data, bool commitChanges, CancellationToken cancellationToken)
        {
            SSAA.BO.Domain.Entities.EstateTaxInquiry estateTaxInquiry = null;            
            var idData = data.FieldValues.Where(fv => fv.FieldName.Equals("id", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (idData == null)
            {
                return null;
            }
            var idValue = idData.Value.ToString();
            estateTaxInquiry = await _estateTaxInquiryRepository.GetAsync(x => x.Id == idValue.ToGuid() || x.LegacyId.Equals(idValue, StringComparison.OrdinalIgnoreCase), cancellationToken);
            
            bool isNew = false;
            if (estateTaxInquiry == null)
            {                
                estateTaxInquiry = new Domain.Entities.EstateTaxInquiry();
                estateTaxInquiry.Id = Guid.NewGuid();
                estateTaxInquiry.LegacyId = idValue;
                estateTaxInquiry.CreateDate = GeneralHelper.GetDatePart(GetValue<string>(data, "CreateDateTime"));
                estateTaxInquiry.CreateTime = GeneralHelper.GetTimePart(GetValue<string>(data, "CreateDateTime"));               
                isNew = true;
            }
            else
            {
                await _estateTaxInquiryRepository.LoadCollectionAsync(estateTaxInquiry, x => x.EstateTaxInquiryPeople, cancellationToken);
                await _estateTaxInquiryRepository.LoadCollectionAsync(estateTaxInquiry, x => x.EstateTaxInquiryAttaches, cancellationToken);
                await _estateTaxInquiryRepository.LoadCollectionAsync(estateTaxInquiry, x => x.EstateTaxInquiryFiles, cancellationToken);
            }
            var estateInquiry = await SetEstateInquiryRelatedProperties(data, estateTaxInquiry, cancellationToken);
            var workflowState = await SetProperties(estateTaxInquiry, data, estateInquiry!=null ? estateInquiry.ScriptoriumId:"", cancellationToken);

            if (estateInquiry == null)
            {
                estateTaxInquiry.EstateInquiryId = null;
                estateTaxInquiry.EstateUnitId = GetValue<string>(data, "UnitId");
                estateTaxInquiry.Estatebasic = GetValue<string>(data, "Basic");
                estateTaxInquiry.Estatesecondary = GetValue<string>(data, "Secondary");
                estateTaxInquiry.BasicRemaining = "2";
                estateTaxInquiry.SecondaryRemaining = "2";
                var sectionId = GetValue<string>(data, "BSTSectionId");
                if (!string.IsNullOrWhiteSpace(sectionId))
                    estateTaxInquiry.EstateSectionId = (await _sectionRepository.GetAsync(x => x.LegacyId == sectionId, cancellationToken)).Id;
                else
                    estateTaxInquiry.EstateSectionId = null;
                estateTaxInquiry.EstateSubsectionId = null;
            }

            object nationalityId = "---";           
            var PersonList = _request.Data.Where(x => x.EntityName.Equals("inquirytaxrelatedpersons", StringComparison.OrdinalIgnoreCase) && x.OrderNo > data.OrderNo && x.OrderNo < nextOrder).ToList();
            bool hasPerson = false;
            List<string> legacyGeoIdList = new List<string>();
            foreach (EntityData person in PersonList)
            {
                hasPerson = true;
                nationalityId = person.FieldValues.Where(fv => fv.FieldName.Equals("NTIONALITYID", StringComparison.OrdinalIgnoreCase)).First().Value;                
                if (nationalityId != null)
                    legacyGeoIdList.Add(nationalityId.ToString());               
            }
            if (legacyGeoIdList.Count > 0)
            {
                geLocations = await _baseInfoServiceHelper.GetGeolocationByLegacyId(legacyGeoIdList.ToArray(), cancellationToken);
            }
            iranGeoLocation = await _baseInfoServiceHelper.GetGeoLocationOfIran(cancellationToken);
            
            bool isLegacyInquiry = false;
            if (!string.IsNullOrWhiteSpace(estateTaxInquiry.LegacyId))
                isLegacyInquiry = true;
            if(hasPerson)
            {
                foreach (var person in PersonList)
                {
                    var id = person.FieldValues.Where(x => x.FieldName.Equals("id", StringComparison.OrdinalIgnoreCase)).First();
                    var taxInquiryPerson = estateTaxInquiry.EstateTaxInquiryPeople.Where(x => x.Id == id.Value.ToString().ToGuid() || x.LegacyId.Equals(id.Value.ToString(), StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                    var p = SetProperties(taxInquiryPerson, person);
                    if (p != null)
                    {
                        p.EstateTaxInquiryId = estateTaxInquiry.Id;
                        estateTaxInquiry.EstateTaxInquiryPeople.Add(p);
                    }
                }
            }
            var attachList = _request.Data.Where(x => x.EntityName.Equals("InquiryTaxAffairsAttach", StringComparison.OrdinalIgnoreCase) && x.OrderNo > data.OrderNo && x.OrderNo < nextOrder).ToList();
            foreach(var attach in attachList)
            {
                var id = attach.FieldValues.Where(x => x.FieldName.Equals("id", StringComparison.OrdinalIgnoreCase)).First();
                var taxInquiryAttach = estateTaxInquiry.EstateTaxInquiryAttaches.Where(x => x.Id == id.Value.ToString().ToGuid() || x.LegacyId.Equals(id.Value.ToString(), StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                var a = await SetProperties(taxInquiryAttach, attach, cancellationToken);
                if (a != null)
                {
                    a.EstateTaxInquiryId = estateTaxInquiry.Id;
                    estateTaxInquiry.EstateTaxInquiryAttaches.Add(a);
                }
            }
            if (workflowState.State == "40")
            {
                if (estateTaxInquiry.CertificateFile != null)
                {
                    var pdf = estateTaxInquiry.EstateTaxInquiryFiles.Where(x => x.AttachmentNo == estateTaxInquiry.No + "_1" && x.FileExtention == "pdf").FirstOrDefault();
                    if (pdf == null)
                    {
                        pdf = new Domain.Entities.EstateTaxInquiryFile();
                        pdf.EstateTaxInquiryId = estateTaxInquiry.Id;
                        pdf.ScriptoriumId = estateTaxInquiry.ScriptoriumId;
                        pdf.Ilm = "1";
                        pdf.AttachmentNo = estateTaxInquiry.No + "_1";
                        pdf.FileContent = estateTaxInquiry.CertificateFile;
                        pdf.FileExtention = "pdf";
                        pdf.Timestamp = 1;
                        pdf.CreateDate = _dateTimeService.CurrentPersianDate;
                        pdf.CreateTime = _dateTimeService.CurrentTime;
                        estateTaxInquiry.CertificateFile = null;
                        estateTaxInquiry.EstateTaxInquiryFiles.Add(pdf);
                    }
                    else
                    {
                        pdf.FileContent= estateTaxInquiry.CertificateFile;
                        estateTaxInquiry.CertificateFile = null;                        
                    }

                }
                else
                {
                    var pdf = estateTaxInquiry.EstateTaxInquiryFiles.Where(x => x.AttachmentNo == estateTaxInquiry.No + "_1" && x.FileExtention == "pdf").FirstOrDefault();
                    if (pdf != null)
                    {
                        if (!string.IsNullOrWhiteSpace(pdf.ArchiveMediaFileId))
                            pdf.ChangeState = "3";
                        else
                            estateTaxInquiry.EstateTaxInquiryFiles.Remove(pdf);
                    }
                }
            }
            var fileList = _request.Data.Where(x => x.EntityName.Equals("InquiryTaxAffairsFile", StringComparison.OrdinalIgnoreCase) && x.OrderNo > data.OrderNo && x.OrderNo < nextOrder).ToList();
            foreach (var file in fileList)
            {
                var id = file.FieldValues.Where(x => x.FieldName.Equals("id", StringComparison.OrdinalIgnoreCase)).First();
                var taxInquiryFile = estateTaxInquiry.EstateTaxInquiryFiles.Where(x => x.Id == id.Value.ToString().ToGuid() || x.LegacyId.Equals(id.Value.ToString(), StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                var a =  SetProperties(taxInquiryFile, file);
                if (a != null)
                {                    
                    a.EstateTaxInquiryId = estateTaxInquiry.Id;
                    estateTaxInquiry.EstateTaxInquiryFiles.Add(a);                   
                }               
            }
            if (_request.TransferFilesToArchive)
            {
                
                foreach (var taxInquiryFile in estateTaxInquiry.EstateTaxInquiryFiles)
                {
                    if (taxInquiryFile.FileContent != null && taxInquiryFile.ChangeState != "3")
                    {
                        var uploadResult = await UploadFileToArchive(taxInquiryFile, cancellationToken);
                        if (uploadResult != null && uploadResult.IsSuccess)
                        {
                            taxInquiryFile.FileContent = null;
                            taxInquiryFile.ArchiveMediaFileId = uploadResult.MediaId;
                        }
                    }
                    else if(taxInquiryFile.ChangeState=="3")
                    {
                        var removeResult = await RemoveFileFromArchive(taxInquiryFile, cancellationToken);
                        if (removeResult)
                            taxInquiryFile.ChangeState = "4";
                    }
                }
                var removedFiles = estateTaxInquiry.EstateTaxInquiryFiles.Where(f => f.ChangeState == "4").ToList();
                foreach (var file in removedFiles)
                    estateTaxInquiry.EstateTaxInquiryFiles.Remove(file);
            }
            if (isNew)
            {               
                if (commitChanges)
                    await _estateTaxInquiryRepository.AddAsync(estateTaxInquiry, cancellationToken);
                else
                    await _estateTaxInquiryRepository.AddAsync(estateTaxInquiry, cancellationToken, false);
            }
            else if (commitChanges)
                await _estateTaxInquiryRepository.UpdateAsync(estateTaxInquiry, cancellationToken);
            else
                await _estateTaxInquiryRepository.UpdateAsync(estateTaxInquiry, cancellationToken, false);
            return estateTaxInquiry.Id;
        }
        private  EstateTaxInquiryFile SetProperties(EstateTaxInquiryFile estateTaxInquiryFile, EntityData file)
        {
            if (file == null) return null;
            bool isNewFileObject = false;
            if (estateTaxInquiryFile == null)
            {
                estateTaxInquiryFile = new EstateTaxInquiryFile();
                estateTaxInquiryFile.Id = Guid.NewGuid();
                estateTaxInquiryFile.LegacyId = file.FieldValues.Where(x => x.FieldName.Equals("id", StringComparison.OrdinalIgnoreCase)).First().Value.ToString();
                isNewFileObject = true;
            }
            estateTaxInquiryFile.AttachmentDate = GetValue<string>(file, "AttachmentDate");
            estateTaxInquiryFile.AttachmentDesc = GetValue<string>(file, "AttachmentDesc");
            estateTaxInquiryFile.AttachmentNo = GetValue<string>(file, "AttachmentNo");
            estateTaxInquiryFile.AttachmentTitle = GetValue<string>(file, "AttachmentTitle");
            estateTaxInquiryFile.CreateDate = GeneralHelper.GetDatePart(GetValue<string>(file, "CreateDateTime"));
            estateTaxInquiryFile.CreateTime = GeneralHelper.GetTimePart(GetValue<string>(file, "CreateDateTime"));
            estateTaxInquiryFile.FileContent = GetValue<byte[]>(file, "FileContent");
            estateTaxInquiryFile.FileExtention= GetValue<string>(file, "FileExtention");
            estateTaxInquiryFile.Ilm = "1";
            estateTaxInquiryFile.ScriptoriumId = GScriptoriumId;
            estateTaxInquiryFile.SendDate = GeneralHelper.GetDatePart(GetValue<string>(file, "SendDateTime"));
            estateTaxInquiryFile.SendTime = GeneralHelper.GetTimePart(GetValue<string>(file, "SendDateTime"));            
            estateTaxInquiryFile.Timestamp = GetValue<decimal>(file, "Timestamp");
            if (isNewFileObject)
                return estateTaxInquiryFile;
            return null;
        }
        private EstateTaxInquiryPerson SetProperties(EstateTaxInquiryPerson estateTaxInquiryPerson, EntityData Person)
        {
            if (Person == null) return null;
            bool isNewPersonObject = false;
            if (estateTaxInquiryPerson == null)
            {
                estateTaxInquiryPerson = new EstateTaxInquiryPerson();
                estateTaxInquiryPerson.Id = Guid.NewGuid();
                estateTaxInquiryPerson.LegacyId = Person.FieldValues.Where(x => x.FieldName.Equals("id", StringComparison.OrdinalIgnoreCase)).First().Value.ToString();
                isNewPersonObject= true;
            }
            estateTaxInquiryPerson.DealsummaryPersonRelateTypeId = GetValue<string>(Person, "RelationKind");
            estateTaxInquiryPerson.PersonType = GetValue<int>(Person, "PERSONTYPE").ToString();
            var nationality = GetValue<string>(Person, "NTIONALITYID");
            bool isIrani = true;
            if (!string.IsNullOrWhiteSpace(nationality))
            {
                if (!iranGeoLocation.GeolocationList[0].LegacyId.Equals(nationality, StringComparison.OrdinalIgnoreCase))
                    isIrani = false;
            }
            estateTaxInquiryPerson.Address = GetValue<string>(Person, "ADDRESS");
            estateTaxInquiryPerson.BirthDate = GetValue<string>(Person, "BIRTHDATE");
            estateTaxInquiryPerson.ScriptoriumId = GScriptoriumId;
            estateTaxInquiryPerson.Family = GetValue<string>(Person, "FAMILY");
            estateTaxInquiryPerson.FatherName = GetValue<string>(Person, "FATHERNAME");
            estateTaxInquiryPerson.IdentityNo = GetValue<string>(Person, "IdentityNo");
            estateTaxInquiryPerson.Ilm = "1";
            estateTaxInquiryPerson.IsOriginal = EstateConstant.BooleanConstant.True;
            estateTaxInquiryPerson.IsRelated = EstateConstant.BooleanConstant.False;
            if (isIrani)
                estateTaxInquiryPerson.IsIranian = EstateConstant.BooleanConstant.True;
            else
                estateTaxInquiryPerson.IsIranian = EstateConstant.BooleanConstant.False;
            estateTaxInquiryPerson.IssuePlace = GetValue<string>(Person, "IssuePlace");
            estateTaxInquiryPerson.LastLegalPaperDate = "";
            estateTaxInquiryPerson.LastLegalPaperNo = "";
            estateTaxInquiryPerson.Name = GetValue<string>(Person, "NAME");
            estateTaxInquiryPerson.NationalityCode = GetValue<string>(Person, "NationalityCode");
            if (!string.IsNullOrWhiteSpace(nationality))
            {
                var na = geLocations.GeolocationList.Where(x => x.LegacyId.Equals(nationality, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                if (na != null)
                {
                    estateTaxInquiryPerson.NationalityId = Convert.ToInt32(na.Id);
                }
                else
                    estateTaxInquiryPerson.NationalityId = Convert.ToInt32(iranGeoLocation.GeolocationList[0].Id);
            }
            else
                estateTaxInquiryPerson.NationalityId = Convert.ToInt32(iranGeoLocation.GeolocationList[0].Id);            
            estateTaxInquiryPerson.PostalCode = GetValue<string>(Person, "POSTALCODE");            
            estateTaxInquiryPerson.SharePart = GetValue<decimal>(Person, "SHAREPART");
            estateTaxInquiryPerson.ShareTotal = GetValue<decimal>(Person, "SHARETOTAL");            
            estateTaxInquiryPerson.Timestamp = GetValue<decimal>(Person, "Timestamp");
            var rt = GetValue<decimal?>(Person, "RoleType");
            if (rt != null)
            {
                if (rt.Value == 1)
                    estateTaxInquiryPerson.RoleType = true;
                else
                    estateTaxInquiryPerson.RoleType = false;
            }
            else
                estateTaxInquiryPerson.RoleType = null;
            if (estateTaxInquiryPerson.RoleType == true)
            {
                estateTaxInquiryPerson.DealsummaryPersonRelateTypeId = "108";
            }
            if (isNewPersonObject)
                return estateTaxInquiryPerson;
            return null;
        }
        private async Task<EstateTaxInquiryAttach> SetProperties(EstateTaxInquiryAttach estateTaxInquiryAttach, EntityData attachData,CancellationToken cancellationToken)
        {            
            if (attachData == null) return null;
            bool isNewAttachObject = false;
            if (estateTaxInquiryAttach == null)
            {
                estateTaxInquiryAttach = new EstateTaxInquiryAttach();
                estateTaxInquiryAttach.Id = Guid.NewGuid();
                estateTaxInquiryAttach.LegacyId = attachData.FieldValues.Where(x => x.FieldName.Equals("id", StringComparison.OrdinalIgnoreCase)).First().Value.ToString();
                isNewAttachObject = true;
            }
            estateTaxInquiryAttach.Area = GetValue<decimal>(attachData,"Area");
            estateTaxInquiryAttach.Block = GetValue<string>(attachData,"Block");
            estateTaxInquiryAttach.EstatePieceTypeId = (await _estatePieceTypeRepository.GetAsync(x => x.LegacyId.Equals(GetValue<string>(attachData, "EPieceTypeId"), StringComparison.OrdinalIgnoreCase), cancellationToken)).Id;
            estateTaxInquiryAttach.EstateSideTypeId = GetSideTypeId(GetValue<string>(attachData, "Side"));
            estateTaxInquiryAttach.Floor = GetValue<string>(attachData, "Floor");
            estateTaxInquiryAttach.Ilm = "1";
            estateTaxInquiryAttach.Piece = GetValue<string>(attachData, "Piece");
            estateTaxInquiryAttach.ScriptoriumId = GScriptoriumId;
            estateTaxInquiryAttach.Timestamp = GetValue<decimal>(attachData, "Timestamp");
            if (isNewAttachObject)
                return estateTaxInquiryAttach;
            return null;
        }
        private static string GetSideTypeId(string v)
        {
            var keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("جنوب غربي","01");
            keyValuePairs.Add("جنوبي", "02");
            keyValuePairs.Add("جنوب شرقي", "03");
            keyValuePairs.Add("غربي", "04");
            keyValuePairs.Add("مركزي", "05");
            keyValuePairs.Add("شرقي", "06");
            keyValuePairs.Add("شمال غربي", "07");
            keyValuePairs.Add("شمالي", "08");
            keyValuePairs.Add("شمال شرقي", "09");
            return keyValuePairs[v.NormalizeTextChars()];            
        }
        private async Task<WorkflowState> SetProperties(Domain.Entities.EstateTaxInquiry estateTaxInquiry, EntityData data,  string scriptoriumId, CancellationToken cancellationToken)
        {            
            var (workflowStatesId, workflowStates) = await GetEstateTaxInquiryWorkflowState(GetValue<int>(data, "TaxAffairsState").ToString(), cancellationToken);
            if (string.IsNullOrWhiteSpace(scriptoriumId))
            {
                var sid = data.FieldValues.Where(x => x.FieldName.Equals("ScriptoriumId", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                if (sid != null && sid.Value != null)
                {
                    scriptoriumId = sid.Value.ToString();
                    var scriptoriums = await this._baseInfoServiceHelper.GetScriptoriumByLegacyId(new string[] { scriptoriumId }, cancellationToken);
                    GScriptoriumId = scriptoriums.ScriptoriumList[0].Id;
                }
                else
                {
                    var nd = data.FieldValues.Where(x => x.FieldName.Equals("No", StringComparison.OrdinalIgnoreCase)).First();
                    var organization = await this._baseInfoServiceHelper.GetOrganizationById(new string[] { nd.Value.ToString().Substring(7, 5) }, cancellationToken);
                    GScriptoriumId = organization.OrganizationList[0].ScriptoriumId;
                }
            }
            else
                GScriptoriumId = scriptoriumId;
            estateTaxInquiry.ApartmentArea = GetValue<decimal?>(data, "ApartmentArea");
            estateTaxInquiry.ArsehArea = GetValue<decimal?>(data, "ArsehArea");
            estateTaxInquiry.Avenue = GetValue<string>(data, "Avenue");
            estateTaxInquiry.BillNo = GetValue<string>(data, "BillNo");
            estateTaxInquiry.BuildingOld = GetValue<decimal?>(data, "BuildingOld");
            estateTaxInquiry.BuildingType = GetValue<string>(data, "BuildingType");
            estateTaxInquiry.BuildingUsingTypeId = (await _estateTaxInquiryUsingTypeRepository.GetAsync(x => x.LegacyId == GetValue<string>(data, "ApartmentUsingTypeId"), cancellationToken)).Id;
            estateTaxInquiry.CertificateFile = GetValue<byte[]>(data, "TaxAffairsFile");
            estateTaxInquiry.CertificateHtml = GetValue<string>(data, "LicenseHTML");
            estateTaxInquiry.CertificateNo = GetValue<string>(data, "LicenceNumber");
            estateTaxInquiry.CessionDate = GetValue<string>(data, "CessionDate");
            estateTaxInquiry.CessionPrice = GetValue<decimal?>(data, "CessionPrice");
            estateTaxInquiry.EstateAddress = GetValue<string>(data, "EstateAddress");
            estateTaxInquiry.EstatePostCode = GetValue<string>(data, "EstatePostCode");
            estateTaxInquiry.EstateSector = GetValue<string>(data, "EstateSector");
            estateTaxInquiry.EstateTaxCityId = (await _estateTaxCityRepository.GetAsync(x => x.LegacyId == GetValue<string>(data, "DistrictOfEstateId"), cancellationToken)).Id;
            estateTaxInquiry.EstateTaxCountyId = (await _estateTaxCountyRepository.GetAsync(x => x.LegacyId == GetValue<string>(data, "CityOfEstateId"), cancellationToken)).Id;
            var InquiryEstateBuildStepId = GetValue<string>(data, "InquiryEstateBuildStepId");
            if (!string.IsNullOrWhiteSpace(InquiryEstateBuildStepId))
                estateTaxInquiry.EstateTaxInquiryBuildingConstructionStepId = (await _estateTaxInquiryBuildingConstructionStepRepository.GetAsync(x => x.LegacyId == InquiryEstateBuildStepId, cancellationToken)).Id;
            else
                estateTaxInquiry.EstateTaxInquiryBuildingConstructionStepId = null;
            estateTaxInquiry.EstateTaxInquiryBuildingStatusId = (await _estateTaxInquiryBuildingStatusRepository.GetAsync(x => x.LegacyId == GetValue<string>(data, "InquiryEstateBuildingStatusId"), cancellationToken)).Id;
            estateTaxInquiry.EstateTaxInquiryBuildingTypeId = (await _estateTaxInquiryBuildingTypeRepository.GetAsync(x => x.LegacyId == GetValue<string>(data, "InquiryEstateBuildTypeId"), cancellationToken)).Id;
            estateTaxInquiry.EstateTaxInquiryDocumentRequestTypeId = (await _estateTaxInquiryDocumentRequestTypeRepository.GetAsync(x => x.LegacyId == GetValue<string>(data, "InquiryDocRequestTypeId"), cancellationToken)).Id;
            estateTaxInquiry.EstateTaxInquiryFieldTypeId = (await _estateTaxInquiryFieldTypeRepository.GetAsync(x => x.LegacyId == GetValue<string>(data, "InquiryGrandTypeId"), cancellationToken)).Id;
            estateTaxInquiry.EstateTaxInquiryTransferTypeId = GetValue<string>(data, "CessionSubject");
            var taxOfficeId = GetValue<string>(data, "TaxOfficeId");
            if (!string.IsNullOrWhiteSpace(taxOfficeId))
                estateTaxInquiry.EstateTaxUnitId = (await _estateTaxUnitRepository.GetAsync(x => x.LegacyId == taxOfficeId, cancellationToken)).Id;
            else
                estateTaxInquiry.EstateTaxUnitId = null;
            estateTaxInquiry.FieldUsingTypeId = (await _estateTaxInquiryUsingTypeRepository.GetAsync(x => x.LegacyId == GetValue<string>(data, "GrandUsingTypeId"), cancellationToken)).Id;
            estateTaxInquiry.FkEstateTaxProvinceId = (await _estateTaxProvinceRepository.GetAsync(x => x.LegacyId == GetValue<string>(data, "ProvinceOfEstateId"), cancellationToken)).Id;
            estateTaxInquiry.FloorNo = GetValue<decimal?>(data, "FloorNo");
            estateTaxInquiry.HasSpecialTrance = ConvertToStringBoolean(GetValue<int?>(data, "HasSpecialTrance"));
            estateTaxInquiry.HasSpecifiedTradingValue = ConvertToStringBoolean(GetValue<int?>(data, "HasSpecifiedTradingValue"));
            estateTaxInquiry.HowToPay = GetValue<string>(data, "HowToPay");
            estateTaxInquiry.Ilm = "1";
            var isActive = GetValue<int?>(data, "IsActive");
            estateTaxInquiry.IsActive = isActive == null || isActive == 1 ? EstateConstant.BooleanConstant.True : EstateConstant.BooleanConstant.False;
            estateTaxInquiry.IsCostPaid = ConvertToStringBoolean(GetValue<int?>(data, "ConfirmPayCost"));
            estateTaxInquiry.IsFirstCession = ConvertToStringBoolean(GetValue<int?>(data, "IsFirstCession")); 
            estateTaxInquiry.IsFirstDeal = ConvertToStringBoolean(GetValue<int?>(data, "IsFirstDeal")); 
            estateTaxInquiry.IsGroundLevel = ConvertToStringBoolean(GetValue<int?>(data, "IsGroundLevel"));
            estateTaxInquiry.IsLicenceReady = ConvertToStringBoolean(GetValue<int?>(data, "IsLicenceReady"));
            estateTaxInquiry.IsMissingSeparateDocument = ConvertToStringBoolean(GetValue<int?>(data, "IsMissingSeparateDoc"));
            estateTaxInquiry.IsWornTexture = ConvertToStringBoolean(GetValue<int?>(data, "EnfeebledRestrictr"));
            estateTaxInquiry.LastReceiveStatusDate = GeneralHelper.GetDatePart(GetValue<string>(data, "LastReceiveStatusTime"));
            estateTaxInquiry.LastReceiveStatusTime = GeneralHelper.GetTimePart(GetValue<string>(data, "LastReceiveStatusTime"));
            estateTaxInquiry.LastSendDate = GeneralHelper.GetDatePart(GetValue<string>(data, "LastSendTime"));
            estateTaxInquiry.LastSendTime = GeneralHelper.GetTimePart(GetValue<string>(data, "LastSendTime"));            
            estateTaxInquiry.LicenseDate = GetValue<string>(data, "LicenseDate");
            estateTaxInquiry.LocationAssignRightDealCurrentValue = GetValue<decimal?>(data, "EstateCurrentValue");
            var InquiryOwnershipTypeId = GetValue<string>(data, "InquiryOwnershipTypeId");
            if (!string.IsNullOrWhiteSpace(InquiryOwnershipTypeId))
                estateTaxInquiry.LocationAssignRigthOwnershipTypeId = (await _estateTaxInquiryLocationAssignRigthOwnershipTypeRepository.GetAsync(x => x.LegacyId == InquiryOwnershipTypeId, cancellationToken)).Id;
            else
                estateTaxInquiry.LocationAssignRigthOwnershipTypeId = null;
            var InquiryLRAssignTypeId = GetValue<string>(data, "InquiryLRAssignTypeId");
            if (!string.IsNullOrWhiteSpace(InquiryLRAssignTypeId))
                estateTaxInquiry.LocationAssignRigthUsingTypeId = (await _estateTaxInquiryLocationAssignRigthUsingTypeRepository.GetAsync(x => x.LegacyId == InquiryLRAssignTypeId, cancellationToken)).Id;
            else
                estateTaxInquiry.LocationAssignRigthUsingTypeId = null;
            estateTaxInquiry.No = GetValue<string>(data, "No");
            estateTaxInquiry.No2 = GetValue<string>(data, "UniqueNo");
            estateTaxInquiry.PayCostDate = GeneralHelper.GetDatePart(GetValue<string>(data, "PayCostDateTime"));
            estateTaxInquiry.PayCostTime = GeneralHelper.GetTimePart(GetValue<string>(data, "PayCostDateTime"));
            estateTaxInquiry.PaymentType = GetValue<string>(data, "PaymentType");
            estateTaxInquiry.PlateNo = GetValue<string>(data, "PlateNo");
            estateTaxInquiry.PrevTransactionsAccordingToFacilitateRule = ConvertToStringBoolean(GetValue<int?>(data, "PCIsAccordingToFacilitateRule"));
            estateTaxInquiry.ReceiptNo = GetValue<string>(data, "ReceiptNo");
            estateTaxInquiry.RenovationRelatedBlockNo = GetValue<string>(data, "RenovationRelatedBlockNo");
            estateTaxInquiry.RenovationRelatedEstateNo = GetValue<string>(data, "RenovationRelatedEstateNo");
            estateTaxInquiry.RenovationRelatedRowNo = GetValue<string>(data, "RenovationRelatedRowNo");
            estateTaxInquiry.ScriptoriumId = GScriptoriumId;
            estateTaxInquiry.SeparationProcessNo = GetValue<string>(data, "SeparationProcessNo");
            estateTaxInquiry.ShareOfOwnership = GetValue<decimal?>(data, "ShareOfOwnership");
            estateTaxInquiry.ShebaNo = GetValue<string>(data, "ShebaNo");
            estateTaxInquiry.StatusDescription = GetValue<string>(data, "StatusDescription");
            estateTaxInquiry.SumPrices = GetValue<int?>(data, "SumPrices");
            estateTaxInquiry.TaxAmount = GetValue<string>(data, "TaxAmount");
            estateTaxInquiry.TaxBillHtml = GetValue<string>(data, "TaxBillHtml");
            estateTaxInquiry.TaxBillIdentity = GetValue<string>(data, "TaxBillIdentity");
            estateTaxInquiry.TaxPaymentIdentity = GetValue<string>(data, "TaxPaymentIdentity");
            estateTaxInquiry.Timestamp = GetValue<decimal>(data, "Timestamp");// isNew ? 1 : estateTaxInquiry.Timestamp + 1;
            estateTaxInquiry.TotalArea = GetValue<decimal?>(data, "TotalArea");
            estateTaxInquiry.TotalOwnershipShare = GetValue<decimal?>(data, "TotalOwnershipShare");
            estateTaxInquiry.TrackingCode = GetValue<string>(data, "TrackingCode");
            estateTaxInquiry.TranceWidth = GetValue<decimal?>(data, "TranceWidth");
            estateTaxInquiry.TransitionShare = GetValue<decimal?>(data, "TransitionShare");
            estateTaxInquiry.ValuebookletBlockNo = GetValue<decimal?>(data, "BNValueBooklet");
            estateTaxInquiry.ValuebookletRowNo = GetValue<decimal?>(data, "RNValueBooklet");
            estateTaxInquiry.WorkCompletionCertificateDate = GetValue<string>(data, "WorkCompletionCertificateDate");
            estateTaxInquiry.WorkflowStatesId = workflowStatesId;



            estateTaxInquiry.BuildingValue= GetValue<string>(data, "BuildingValue");
            estateTaxInquiry.FacilityLawNumber= GetValue<string>(data, "FacilityLawNumber");
            estateTaxInquiry.FacilityLawYear= GetValue<long?>(data, "FacilityLawYear");
            estateTaxInquiry.GoodWillValue= GetValue<string>(data, "GoodWillValue");
            //*public abstract string LandAndBuildingSumValue { get; }
            estateTaxInquiry.LandValue = GetValue<string>(data, "LandValue");
            estateTaxInquiry.ShebaNo2 = GetValue<string>(data, "ShebaNo2");
            estateTaxInquiry.ShebaNo3 = GetValue<string>(data, "ShebaNo3");
            estateTaxInquiry.TaxAmount2 = GetValue<string>(data, "TaxAmount2");
            estateTaxInquiry.TaxAmount3 = GetValue<string>(data, "TaxAmount3");
            estateTaxInquiry.TaxBillIdentity2 = GetValue<string>(data, "TaxBillIdentity2");
            estateTaxInquiry.TaxBillIdentity3 = GetValue<string>(data, "TaxBillIdentity3");
            estateTaxInquiry.TaxGoodWillValue = GetValue<string>(data, "TaxGoodWillValue");
            estateTaxInquiry.TaxPaymentIdentity2 = GetValue<string>(data, "TaxPaymentIdentity2");
            estateTaxInquiry.TaxPaymentIdentity3 = GetValue<string>(data, "TaxPaymentIdentity3");

            return workflowStates;
        }
        private  async Task<(string,WorkflowState)> GetEstateTaxInquiryWorkflowState(string status,CancellationToken cancellationToken)
        {
            var ws = await _workfolwStateRepository.GetAsync(x => x.TableName == "ESTATE_TAX_INQUIRY" && x.State == status, cancellationToken);
            return (ws.Id,ws);
        }
        static string ConvertToStringBoolean(int? value)
        {            
            if (value.HasValue && value.Value == 1) return EstateConstant.BooleanConstant.True;
            else return EstateConstant.BooleanConstant.False;
        }       
        private static T GetValue<T>(EntityData data,string fieldName)
        {
            return GeneralHelper.GetValue<T>(data, fieldName); 
        }               
        private async Task<MediaUpload> UploadFileToArchive(Domain.Entities.EstateTaxInquiryFile taxInquiryFile, CancellationToken cancellationToken)
        {
            if (!this._request.TransferFilesToArchive) return null;
            var baseInfoUrl = _configuration.GetValue(typeof(string), "InternalGatewayUrl");
            if (string.IsNullOrWhiteSpace(taxInquiryFile.ArchiveMediaFileId))
            {
                string docId = null;
                using (MultipartFormDataContent formData = new MultipartFormDataContent())
                {
                    formData.Add(new StringContent(""), "Media.MediaId");
                    formData.Add(new StringContent(""), "Media.RowNo");
                    formData.Add(new StringContent(""), "DocId");
                    formData.Add(new StringContent(""), "DocDescription");
                    formData.Add(new StringContent(""), "DocCreateDate");
                    formData.Add(new StringContent(""), "DocOtherData");
                    formData.Add(new StringContent(""), "Media.MediaFile");
                    formData.Add(new StringContent(""), "Media.MediaThumbNail");
                    formData.Add(new StringContent(""), "Media.MediaExtension");
                    formData.Add(new StringContent(""), "Media.FileName");
                    formData.Add(new StringContent(""), "Media.FileType");
                    formData.Add(new StringContent(!string.IsNullOrWhiteSpace(taxInquiryFile.AttachmentTitle) ? taxInquiryFile.AttachmentTitle : "-"), "DocTitle");
                    formData.Add(new StringContent(taxInquiryFile.EstateTaxInquiryId.ToString()), "RelatedRecordIds");
                    formData.Add(new StringContent(!string.IsNullOrWhiteSpace(taxInquiryFile.AttachmentNo) ? taxInquiryFile.AttachmentNo : "-"), "DocNo");
                    formData.Add(new StringContent("0910"), "DocType");
                    formData.Add(new StringContent("9007"), "ClientId");
                    var apiResult = await _httpEndPointCaller.CallExternalPostApiAsync<MediaUpload>(new SharedKernel.Contracts.HttpEndPointCaller.HttpEndpointRequest(baseInfoUrl + "Media/Upload", _userService.UserApplicationContext.Token, formData), cancellationToken);
                    if (apiResult.IsSuccess && !string.IsNullOrWhiteSpace(apiResult.AttachmentId))
                    {
                        docId = apiResult.AttachmentId;
                    }
                }
                if (!string.IsNullOrWhiteSpace(docId))
                {
                    using (MultipartFormDataContent formData = new MultipartFormDataContent())
                    {
                        var mediaFileContent = new ByteArrayContent(taxInquiryFile.FileContent);
                        formData.Add(mediaFileContent, "Media.MediaFile", "TaxInquiryFile");
                        formData.Add(new StringContent("null"), "Media.MediaThumbNail");
                        formData.Add(new StringContent("null"), "Media.MediaId");                        
                        formData.Add(new StringContent(taxInquiryFile.FileExtention), "Media.MediaExtension");
                        formData.Add(new StringContent("null"), "Media.RowNo");
                        formData.Add(new StringContent("TaxInquiryFile"), "Media.FileName");
                        formData.Add(new StringContent("image/png"), "Media.FileType");
                        formData.Add(new StringContent(docId), "DocId");
                        formData.Add(new StringContent("0910"), "DocType");
                        formData.Add(new StringContent(!string.IsNullOrWhiteSpace(taxInquiryFile.AttachmentNo) ? taxInquiryFile.AttachmentNo : "-"), "DocNo");
                        formData.Add(new StringContent(!string.IsNullOrWhiteSpace(taxInquiryFile.AttachmentTitle) ? taxInquiryFile.AttachmentTitle : "-"), "DocTitle");
                        formData.Add(new StringContent(!string.IsNullOrWhiteSpace(taxInquiryFile.AttachmentDate) ? taxInquiryFile.AttachmentDate : "-"), "DocCreateDate");
                        formData.Add(new StringContent(!string.IsNullOrWhiteSpace(taxInquiryFile.AttachmentDesc) ? taxInquiryFile.AttachmentDesc : "-"), "DocDescription");
                        formData.Add(new StringContent("-"), "DocOtherData");
                        formData.Add(new StringContent("9007"), "ClientId");
                        formData.Add(new StringContent(taxInquiryFile.EstateTaxInquiryId.ToString()), "RelatedRecordIds");
                        var apiResult = await _httpEndPointCaller.CallExternalPostApiAsync<MediaUpload>(new SharedKernel.Contracts.HttpEndPointCaller.HttpEndpointRequest(baseInfoUrl + "Media/Upload", _userService.UserApplicationContext.Token, formData), cancellationToken);
                        if (apiResult.IsSuccess && !string.IsNullOrWhiteSpace(apiResult.AttachmentId))
                        {
                            return apiResult;
                        }
                    }
                }
            }
            else
            {
                var archiveFile = await DownloadFileFromArchive(taxInquiryFile, cancellationToken);
                if (archiveFile != null)
                {
                    using (MultipartFormDataContent formData = new MultipartFormDataContent())
                    {
                        var mediaFileContent = new ByteArrayContent(taxInquiryFile.FileContent);
                        formData.Add(mediaFileContent, "Media.MediaFile", "TaxInquiryFile");
                        formData.Add(new StringContent("null"), "Media.MediaThumbNail");
                        formData.Add(new StringContent(taxInquiryFile.ArchiveMediaFileId), "Media.MediaId");
                        formData.Add(new StringContent(taxInquiryFile.FileExtention), "Media.MediaExtension");
                        formData.Add(new StringContent("null"), "Media.RowNo");
                        formData.Add(new StringContent("TaxInquiryFile"), "Media.FileName");
                        formData.Add(new StringContent("image/png"), "Media.FileType");
                        formData.Add(new StringContent(archiveFile.AttachmentID), "DocId");
                        formData.Add(new StringContent("0910"), "DocType");
                        formData.Add(new StringContent(!string.IsNullOrWhiteSpace(taxInquiryFile.AttachmentNo) ? taxInquiryFile.AttachmentNo : "-"), "DocNo");
                        formData.Add(new StringContent(!string.IsNullOrWhiteSpace(taxInquiryFile.AttachmentTitle) ? taxInquiryFile.AttachmentTitle : "-"), "DocTitle");
                        formData.Add(new StringContent(!string.IsNullOrWhiteSpace(taxInquiryFile.AttachmentDate) ? taxInquiryFile.AttachmentDate : "-"), "DocCreateDate");
                        formData.Add(new StringContent(!string.IsNullOrWhiteSpace(taxInquiryFile.AttachmentDesc) ? taxInquiryFile.AttachmentDesc : "-"), "DocDescription");
                        formData.Add(new StringContent("-"), "DocOtherData");
                        formData.Add(new StringContent("9007"), "ClientId");
                        formData.Add(new StringContent(taxInquiryFile.EstateTaxInquiryId.ToString()), "RelatedRecordIds");
                        var apiResult = await _httpEndPointCaller.CallExternalPostApiAsync<MediaUpload>(new SharedKernel.Contracts.HttpEndPointCaller.HttpEndpointRequest(baseInfoUrl + "Media/Upload", _userService.UserApplicationContext.Token, formData), cancellationToken);
                        if (apiResult.IsSuccess && !string.IsNullOrWhiteSpace(apiResult.AttachmentId))
                        {
                            return apiResult;
                        }
                    }
                }
            }
            return null;
        }
        private async Task<DownloadAttachmentViewModel> DownloadFileFromArchive(Domain.Entities.EstateTaxInquiryFile taxInquiryFile, CancellationToken cancellationToken)
        {
            var downloadAttachmentInput = new DownloadMediaServiceInput
            {
                AttachmentFileId = taxInquiryFile.ArchiveMediaFileId,
                AttachmentClientId = "9007",
                AttachmentRelatedRecordId = taxInquiryFile.EstateTaxInquiryId.ToString(),
                AttachmentTypeId = "0910"
            };
            var r = await _mediator.Send(downloadAttachmentInput, cancellationToken);
            if (r.IsSuccess && r.Data!=null)
                return r.Data;
            return null;
        }
        private async Task<bool> RemoveFileFromArchive(Domain.Entities.EstateTaxInquiryFile taxInquiryFile, CancellationToken cancellationToken)
        {
            var archiveFile = await DownloadFileFromArchive(taxInquiryFile, cancellationToken);
            if (archiveFile != null)
            {
                var removeAttachmentInput = new MediaRemoveServiceInput
                {
                    MediaId = taxInquiryFile.ArchiveMediaFileId,
                    ClientId = "9007",
                    DocId = archiveFile.AttachmentID
                };
                var r = await _mediator.Send(removeAttachmentInput, cancellationToken);
                return r.IsSuccess;
            }
            return false;
        }
       
    }
}
