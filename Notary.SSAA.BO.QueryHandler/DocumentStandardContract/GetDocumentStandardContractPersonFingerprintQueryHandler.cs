using MediatR;
using Notary.ExternalServices.WebApi.Models.RequestsModel.Person;
using Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Commons;
using Notary.SSAA.BO.DataTransferObject.Commands.DocumentStandardContract;
using Notary.SSAA.BO.DataTransferObject.Mappers.DocumentStandardContract;
using Notary.SSAA.BO.DataTransferObject.Queries.DocumentStandardContract;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.Person;
using Notary.SSAA.BO.DataTransferObject.ViewModels.DocumentStandardContract;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Services.Person;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.QueryHandler.DocumentStandardContract
{
    internal class GetDocumentStandardContractPersonFingerprintQueryHandler : BaseQueryHandler<GetDocumentStandardContractPersonFingerprintQuery, ApiResult<List<DocumentStandardContractPersonFingerprintViewModel>>>
    {
        private readonly IDocumentRepository _documentRepository;
        public GetDocumentStandardContractPersonFingerprintQueryHandler(IMediator mediator, IUserService userService,
            IDocumentRepository documentRepository

            )
            : base(mediator, userService)
        {
            _documentRepository = documentRepository ?? throw new ArgumentNullException(nameof(documentRepository));
        }

        protected override bool HasAccess(GetDocumentStandardContractPersonFingerprintQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<List<DocumentStandardContractPersonFingerprintViewModel>>> RunAsync(GetDocumentStandardContractPersonFingerprintQuery request, CancellationToken cancellationToken)
        {

            List<DocumentStandardContractPersonFingerprintViewModel> result = new List<DocumentStandardContractPersonFingerprintViewModel>();
            ApiResult<List<DocumentStandardContractPersonFingerprintViewModel>> apiResult = new();

            UpdateDocumentStandardContractFingerprintStateCommand check = new();
            check.DocumentId = request.DocumentId;
            var res = await _mediator.Send(check, cancellationToken);

            Domain.Entities.Document document = await _documentRepository.GetDocument(request.DocumentId.ToGuid(), cancellationToken);
            if (document != null)
            {
                IList<DocumentPerson> documentPeople = FingerPrintForcedPeople(document.DocumentPeople);
                GetPersonPhotoListServiceInput getPersonForNationalNo = new();
                getPersonForNationalNo.NationalNos = documentPeople.Where(x => x.IsIranian == "1" && x.IsSabtahvalChecked == "1").Select(x => x.NationalNo).ToList();
                if (getPersonForNationalNo.NationalNos.Count > 0)
                {
                    ApiResult<GetPersonPhotoListViewModel> personalImages = new();
                    personalImages.IsSuccess = false;
                    GetPersonPhotoListFromSabteAhvalServiceInput getPerson = new();
                    getPerson.Persons = documentPeople.Where(x => x.IsIranian == "1" && x.IsSabtahvalChecked == "1").Select(x => new PersonPhotoListItem() { NationalNo = x.NationalNo, BirthDate = x.BirthDate }).ToList();
                    getPerson.MainObjectId = document.Id.ToString();
                    if (getPerson.Persons.Count > 0)
                        personalImages = await _mediator.Send(getPerson, cancellationToken);
                    foreach (DocumentPerson item in documentPeople)
                    {
                        if (item.PersonType == "1" && item.IsAlive == "1")
                        {
                            var addingPerson = DocumentStandardContractFingerPrintMapper.ToFingerprintViewModel(item);
                            addingPerson.IsTFARequired = false;
                            if (personalImages.IsSuccess)
                            {
                                var personalImage = personalImages.Data.PersonsData.Where(x => x.NationalNo == item.NationalNo).FirstOrDefault();
                                addingPerson.PersonalImage = personalImage == null ? null : Convert.ToBase64String(personalImage.PersonalImage);
                            }
                            var personOrginal = document.DocumentPeople.FirstOrDefault(x => x.Id == item.Id);
                            if (personOrginal != null && personOrginal.DocumentPersonType != null)
                            {
                                addingPerson.PersonPost = personOrginal.DocumentPersonType.SingularTitle;

                            }

                            var personRelated = document.DocumentPersonRelatedDocuments.Where(x => x.AgentPersonId == item.Id).OrderBy(x => x.RowNo).ToList();
                            foreach (var personRelatedItem in personRelated)
                            {
                                if (!addingPerson.PersonPost.IsNullOrWhiteSpace())
                                    addingPerson.PersonPost += " و ";
                                addingPerson.PersonPost += personRelatedItem.AgentType.Title + " " + document.DocumentPeople.
                                    Where(x => x.Id == personRelatedItem.MainPersonId).Select(x => x.Name + " " + x.Family).FirstOrDefault();
                            }
                            result.Add(addingPerson);
                        }
                    }
                }
                apiResult.Data = result;

            }
            else
            {
                apiResult.IsSuccess = true;
                apiResult.statusCode = ApiResultStatusCode.NotFound;
                apiResult.message.Add("سند مربوطه پیدا نشد");
            }
            return apiResult;
        }
        /// <summary>
        /// The IsFingerPrintForced
        /// </summary>
        /// <param name="theSelectedDocPerson">The theSelectedDocPerson<see cref="DocumentPerson"/></param>
        /// <returns>The <see cref="bool"/></returns>
        private static bool IsFingerPrintForced(DocumentPerson theSelectedDocPerson)
        {
            //return false;

            if (theSelectedDocPerson.Document.State != NotaryRegServiceReqState.FinalPrinted.GetString())
                return false;

            if (theSelectedDocPerson.PersonType == PersonType.Legal.GetString())
                return false;

            //if (theSelectedDocPerson.TheONotaryRegServicePersonType == null)
            //    return true;

            FingerprintAquisitionPermission permission = FingerprintAquisitionManager.IsFingerprintAquisitionPermitted(theSelectedDocPerson.Document, theSelectedDocPerson);

            switch (permission)
            {
                case FingerprintAquisitionPermission.Forbidden:
                    return false;
                case FingerprintAquisitionPermission.Mandatory:
                    return true;
                case FingerprintAquisitionPermission.Optional:

                    if (theSelectedDocPerson.WouldSignedDocument == YesNo.No.GetString())
                        return false;
                    else
                        return true;
            }

            return true;
        }



        public static IList<DocumentPerson> FingerPrintForcedPeople(ICollection<DocumentPerson> documentPeople)
        {
            List<DocumentPerson> fingerPrintForcedPeople = new List<DocumentPerson>();
            foreach (DocumentPerson theOneDocPerson in documentPeople)
            {

                bool isForced = IsFingerPrintForced(theOneDocPerson);
                if (!isForced)
                    continue;
                fingerPrintForcedPeople.Add(theOneDocPerson);
            }
            return fingerPrintForcedPeople;
        }
    }
}
