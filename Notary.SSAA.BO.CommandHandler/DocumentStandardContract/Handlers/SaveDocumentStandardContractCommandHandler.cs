namespace Notary.SSAA.BO.CommandHandler.DocumentStandardContract.Handlers
{
    using MediatR;
    using Serilog;
    using Notary.SSAA.BO.CommandHandler.Base;
    using Notary.SSAA.BO.DataTransferObject.Commands.Document;
    using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;
    using Notary.SSAA.BO.Domain.Entities;
    using Notary.SSAA.BO.Domain.RichDomain.Document;
    using Notary.SSAA.BO.SharedKernel.Constants;
    using Notary.SSAA.BO.SharedKernel.Enumerations;
    using Notary.SSAA.BO.SharedKernel.Interfaces;
    using Notary.SSAA.BO.SharedKernel.Result;
    using Notary.SSAA.BO.Coordinator.NotaryDocument;
    using System.Threading;
    using Notary.SSAA.BO.DataTransferObject.Commands.DocumentStandardContract;
    using Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract;

    /// <summary>
    /// Defines the <see cref="SaveDocumentStandardContractCommandHandler" />
    /// </summary>
    public class SaveDocumentStandardContractCommandHandler : SaveCommandHandler<SaveDocumentStandardContractCommand, ApiResult, DocumentStandardContractViewModel, Document>
    {
        /// <summary>
        /// Defines the _apiResult
        /// </summary>
        private ApiResult<DocumentStandardContractViewModel> _apiResult;

        /// <summary>
        /// Defines the _saveDocumentCoordinator
        /// </summary>
        private readonly SaveDocumentStandardContractCoordinator _saveDocumentCoordinator;

        /// <summary>
        /// Defines the _loadDocumentCoordinator
        /// </summary>
        private readonly LoadDocumentStandardContractCoordinator _loadDocumentCoordinator;

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveDocumentStandardContractCommandHandler"/> class.
        /// </summary>
        /// <param name="mediator">The mediator<see cref="IMediator"/></param>
        /// <param name="userService">The userService<see cref="IUserService"/></param>
        /// <param name="logger">The logger<see cref="ILogger"/></param>
        /// <param name="saveDocumentCoordinator">The saveDocumentCoordinator<see cref="SaveDocumentStandardContractCoordinator"/></param>
        /// <param name="loadDocumentCoordinator">The loadDocumentCoordinator<see cref="LoadDocumentStandardContractCoordinator"/></param>
        public SaveDocumentStandardContractCommandHandler(IMediator mediator,
            IUserService userService,
            ILogger logger,
            SaveDocumentStandardContractCoordinator saveDocumentCoordinator,
            LoadDocumentStandardContractCoordinator loadDocumentCoordinator)
            : base(mediator, userService, logger)
        {
            _saveDocumentCoordinator = saveDocumentCoordinator;
            _loadDocumentCoordinator = loadDocumentCoordinator;
            _apiResult = new();
        }

        /// <summary>
        /// The HasAccess
        /// </summary>
        /// <param name="request">The request<see cref="SaveDocumentStandardContractCommand"/></param>
        /// <param name="userRoles">The userRoles<see cref="IList{string}"/></param>
        /// <returns>The <see cref="bool"/></returns>
        protected override bool HasAccess(SaveDocumentStandardContractCommand request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis) || userRoles.Contains(RoleConstants.Admin);
        }

        /// <summary>
        /// The ExecuteAsync
        /// </summary>
        /// <param name="request">The request<see cref="SaveDocumentStandardContractCommand"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{ApiResult}"/></returns>
        protected override async Task<ApiResult> ExecuteAsync(SaveDocumentStandardContractCommand request, CancellationToken cancellationToken)
        {
            bool isValid;
            List<string> messages = new List<string>() { };
            await LoadEntity(request, cancellationToken);

            if (_saveDocumentCoordinator.document != null && (_saveDocumentCoordinator.document.IsEditable() || request.PrepareDaftaryarConfirm == true))
            {

                (isValid, messages) = ValidateViewModel(request);
                if (isValid)
                {
                    await MapToEntity(request, cancellationToken);

                    (isValid, messages) = await ValidateBeforeSave();
                    if (!isValid)
                    {
                        _apiResult.message = messages;
                        _apiResult.IsSuccess = false;
                        _apiResult.statusCode = ApiResultStatusCode.Success;
                        return _apiResult;
                    }
                    else
                    {
                        var saveResult = await SaveEntity(request, cancellationToken);
                        if (!saveResult.IsSuccess) return saveResult;
                        if (saveResult.HasAllarmMessage)
                        {
                            _apiResult.message = saveResult.message;
                            _apiResult.HasAllarmMessage = true;
                            _apiResult.IsSuccess = true;
                            _apiResult.statusCode = ApiResultStatusCode.Success;
                            return _apiResult;
                        }
                        var result = await AfterSave(request, cancellationToken);

                        result.message = new List<string> { "اطلاعات با موفقیت ذخیره شد" };


                        return result;
                    }
                }
                else
                {
                    _apiResult.message = messages;
                    _apiResult.IsSuccess = false;
                    _apiResult.statusCode = ApiResultStatusCode.Success;
                }


            }
            else
            {
                if (_saveDocumentCoordinator!=null && !_saveDocumentCoordinator.document.IsEditable())
                {
                    _apiResult.message = new List<string>() { "سند مربوطه غیرقابل ویرایش است" };
                    _apiResult.IsSuccess = false;
                    _apiResult.statusCode = ApiResultStatusCode.Success;
                }

            }

            return _apiResult;
        }

        /// <summary>
        /// The ValidateViewModel
        /// </summary>
        /// <param name="request">The request<see cref="SaveDocumentStandardContractCommand"/></param>
        /// <returns>The <see cref="(bool, List{string})"/></returns>
        protected override (bool, List<string>) ValidateViewModel(SaveDocumentStandardContractCommand request)
        {

            List<string> messages = new List<string>();
            bool isValid = false;

            if (_saveDocumentCoordinator != null &&  _saveDocumentCoordinator.document != null)
            {
                (isValid, messages) = _saveDocumentCoordinator.ValidateDocumentViewModel(request);

            }
            else
            {
                messages.Add("سند مربوطه یافت نشد ");
            }
            if (messages.Count > 0)
            {
                isValid = false;
            }
            else isValid = true;

            return (isValid, messages);
        }

        /// <summary>
        /// The MapToEntity
        /// </summary>
        /// <param name="request">The request<see cref="SaveDocumentStandardContractCommand"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task"/></returns>
        protected override async Task MapToEntity(SaveDocumentStandardContractCommand request, CancellationToken cancellationToken)
        {
            await _saveDocumentCoordinator.MapToDocument(request, cancellationToken);
        }

        /// <summary>
        /// The ValidateBeforeSave
        /// </summary>
        /// <returns>The <see cref="Task{(bool, List{string})}"/></returns>
        protected override async Task<(bool, List<string>)> ValidateBeforeSave()
        {
            bool isValid = true;
            List<string> errorMessages = new List<string>();
            (isValid, errorMessages) = await _saveDocumentCoordinator.ValidateDocumentBeforeSave();
            return (isValid, errorMessages);
        }

        /// <summary>
        /// The SaveEntity
        /// </summary>
        /// <param name="request">The request<see cref="SaveDocumentStandardContractCommand"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task"/></returns>
        protected override async Task<ApiResult> SaveEntity(SaveDocumentStandardContractCommand request, CancellationToken cancellationToken)
        {
            return await _saveDocumentCoordinator.SaveDocument(cancellationToken, request);
        }

        /// <summary>
        /// The AfterSave
        /// </summary>
        /// <param name="request">The request<see cref="SaveDocumentStandardContractCommand"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task{ApiResult}"/></returns>
        protected override async Task<ApiResult> AfterSave(SaveDocumentStandardContractCommand request, CancellationToken cancellationToken)
        {

            CaseType caseType = CaseType.None;
            if (_saveDocumentCoordinator != null && _saveDocumentCoordinator.document.HasCases()) caseType = CaseType.DocumentCase;
            else if (_saveDocumentCoordinator != null &&  _saveDocumentCoordinator.document.HasEstates()) caseType = CaseType.DocumentEstate;
            else if (_saveDocumentCoordinator != null &&  _saveDocumentCoordinator.document.HasVehicles()) caseType = CaseType.DocumentVehicle;

            if (request.IsNew && _saveDocumentCoordinator != null && _saveDocumentCoordinator.document != null)
            {

                return await _loadDocumentCoordinator.LoadDocumentDetail(_saveDocumentCoordinator.document.Id.ToString(), DocumentDatailName.DocumentPeople.GetString(), caseType, cancellationToken);

            }
            else
            {
                return await _loadDocumentCoordinator.LoadDocumentDetail(request.RequestId, request.DetailName.GetString(), caseType, cancellationToken);

            }
        }

        /// <summary>
        /// The LoadEntity
        /// </summary>
        /// <param name="request">The request<see cref="SaveDocumentStandardContractCommand"/></param>
        /// <param name="cancellationToken">The cancellationToken<see cref="CancellationToken"/></param>
        /// <returns>The <see cref="Task"/></returns>
        protected override async Task LoadEntity(SaveDocumentStandardContractCommand request, CancellationToken cancellationToken)
        {
            if (request.IsNew)
            {
                _saveDocumentCoordinator.NewDocument(request);
            }
            else if (request.IsRequestInSetNationalDocumentNo())
            {
                await _saveDocumentCoordinator.LoadDocument(request.RequestId, new List<string> { "DocumentCosts", "DocumentInfoConfirm", "DocumentPeople" }, cancellationToken);

            }
            else
            if (!request.IsRequestInFinalState())
            {

                var detailList = _loadDocumentCoordinator.GetDetails(request);
                await _saveDocumentCoordinator.LoadDocument(request.RequestId, detailList, cancellationToken);
            }
            else
            if (request.IsRequestInFinalState())
            {
                await _saveDocumentCoordinator.LoadDocumentForWorkflowProcess(request.RequestId, cancellationToken);

            }

        }
    }
}
