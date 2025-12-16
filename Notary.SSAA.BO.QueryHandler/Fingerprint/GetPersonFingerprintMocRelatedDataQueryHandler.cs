using MediatR;
using Notary.SSAA.BO.DataTransferObject.Queries.Fingerprint;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Fingerprint;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.QueryHandler.Fingerprint
{
    public class GetPersonFingerprintMocRelatedDataQueryHandler : BaseQueryHandler<GetPersonFingerprintMocRelatedDataQuery, ApiResult<GetPersonFingerprintMocRelatedDataViewModel>>
    {
        private readonly IPersonFingerprintRepository _personFingerprintRepository;

        public GetPersonFingerprintMocRelatedDataQueryHandler(IMediator mediator, IUserService userService,
           IPersonFingerprintRepository personFingerprintRepository) : base(mediator, userService)
        {
            _personFingerprintRepository = personFingerprintRepository;

        }

        protected override async Task<ApiResult<GetPersonFingerprintMocRelatedDataViewModel>> RunAsync(GetPersonFingerprintMocRelatedDataQuery request, CancellationToken cancellationToken)
        {
            ApiResult<GetPersonFingerprintMocRelatedDataViewModel> apiResult = new() { IsSuccess = true, statusCode = ApiResultStatusCode.Success };

            var personFingerPrint = await _personFingerprintRepository.GetByIdAsync(cancellationToken, request.FingerPrintId.ToGuid());
            if (personFingerPrint != null)
            {
                var FingerPrintRawImage = personFingerPrint.FingerprintImageFile;
                if (FingerPrintRawImage != null)
                {
                    byte[] newArray = new byte[FingerPrintRawImage.Length - 6];
                    Array.Copy(FingerPrintRawImage, 6, newArray, 0, newArray.Length);
                    string zero_width = "00000000";
                    string zero_height = "00000000";
                    string zero_resulation = "00000000";
                    int intWidth = FingerPrintRawImage[1];
                    string widthStr = Convert.ToString(intWidth, 2);
                    string Width = string.Concat(FingerPrintRawImage[0].ToString(), zero_width.AsSpan(0, 8 - widthStr.Length), widthStr);
                    int intHeight = FingerPrintRawImage[3];
                    string heightStr = Convert.ToString(intHeight, 2);
                    string Height = string.Concat(FingerPrintRawImage[2].ToString(), zero_height.AsSpan(0, 8 - heightStr.Length), heightStr);
                    int intResolution = FingerPrintRawImage[5];
                    string resolutiontStr = Convert.ToString(intResolution, 2);
                    string Resolution = string.Concat(Convert.ToString(FingerPrintRawImage[4], 2), zero_resulation.AsSpan(0, 8 - resolutiontStr.Length), resolutiontStr);


                    apiResult.Data = new GetPersonFingerprintMocRelatedDataViewModel()
                    {
                        FingerIndex = Convert.ToInt32(personFingerPrint.PersonFingerTypeId),
                        FingerPrint = FingerPrintRawImage,
                        ImageHeight = personFingerPrint.FingerprintImageHeight.Value,
                        ImageWidth = personFingerPrint.FingerprintImageWidth.Value,
                        Resolution = 1
                    };
                }
                else
                {
                    apiResult.IsSuccess = false;
                    apiResult.message.Add("رکورد اثر انگشت مربوطه ، اثر انگشت  ندارد");

                }
            }
            else
            {
                apiResult.IsSuccess = false;
                apiResult.message.Add("رکورد اثر انگشت مربوطه ،یافت نشد");

            }

            return apiResult;
        }

        protected override bool HasAccess(GetPersonFingerprintMocRelatedDataQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis) || userRoles.Contains(RoleConstants.Admin);
        }
    }
}
