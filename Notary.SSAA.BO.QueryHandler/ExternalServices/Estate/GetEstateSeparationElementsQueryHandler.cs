using MediatR;
using Notary.SSAA.BO.Coordinator.Estate.Helpers;
using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices.Estate;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices.Estate;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.QueryHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.QueryHandler.ExternalServices.Estate
{
    public class GetEstateSeparationElementsQueryHandler : BaseQueryHandler<GetEstateSeparationElementsQuery, ApiResult<EstateSeparationDocumentRelatedData>>
    {
       
        private readonly IRepository<EstatePieceType> _estatePieceTypeRepository;
        private readonly IDateTimeService _dateTimeService;
        private readonly BaseInfoServiceHelper _baseInfoServiceHelper;

        public GetEstateSeparationElementsQueryHandler(IMediator mediator, IUserService userService, IDateTimeService dateTimeService, IRepository<EstatePieceType> estatePieceTypeRepository)
            : base(mediator, userService)
        {

                 
            _dateTimeService = dateTimeService;
            _estatePieceTypeRepository = estatePieceTypeRepository;
            _baseInfoServiceHelper = new BaseInfoServiceHelper(_mediator);

        }
        protected override bool HasAccess(GetEstateSeparationElementsQuery request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis);
        }

        protected async override Task<ApiResult<EstateSeparationDocumentRelatedData>> RunAsync(GetEstateSeparationElementsQuery request, CancellationToken cancellationToken)
        {
            EstateSeparationDocumentRelatedData result = new();
            ApiResult<EstateSeparationDocumentRelatedData> apiResult = new();

            var input = new GetEstateSeparationElementsInput() { EstateInquiryId = request.EstateInquiryId };
            var output = await _mediator.Send(input, cancellationToken);
            if (output != null)
            {
                if (output.IsSuccess)
                {
                    if (string.IsNullOrWhiteSpace(output.Data.ErrorMessage))
                    {
                        if (output.Data.Separation != null)
                        {
                            var unit = await _baseInfoServiceHelper.GetUnitById(new string[] { output.Data.Separation.UnitId }, cancellationToken);
                            if (unit == null || unit.UnitList == null || unit.UnitList.Count == 0)
                            {
                                apiResult.IsSuccess = false;
                                apiResult.message.Add("واحد ثبتی مرتبط با تقسیم نامه در اطلاعات پایه سامانه یافت نشد");
                                return apiResult;
                            }    
                            result.SeparationNo = output.Data.Separation.NatureNum;
                            result.SeparationDate = output.Data.Separation.NatureDate.Substring(0, 10);
                            result.SeparationIssuer = unit.UnitList[0].Name;
                            result.SeparationType = output.Data.Separation.FunctionType.ToString();
                            result.SeparationText = output.Data.Separation.PrintedText;
                            result.SeparationDescription = output.Data.Separation.Description;

                            foreach (var oneSeparationPiece in output.Data.Separation.TheSeparationElements)
                            {

                                var newSeparationPieces = new EstateSeparationPiece();


                                if (oneSeparationPiece.EKindPieceCode != "3" &&    // آپارتمانی
                                    oneSeparationPiece.EKindPieceCode != "D1" &&   // قطعه
                                    oneSeparationPiece.EKindPieceCode != "3." &&   // منضماتی
                                    oneSeparationPiece.EKindPieceCode != "4" &&    // انباری
                                    oneSeparationPiece.EKindPieceCode != "5" //&&
                                    //oneSeparationPiece.EKindPieceCode != "2"
                                    )      // پارکینگ
                                    continue;

                                if (string.Compare(oneSeparationPiece.EPieceTypeId, "016401315819475d8e6c12975ce8d973") == 0 ||
                                    string.Compare(oneSeparationPiece.EPieceTypeId, "2a70fb3d502b4a72967a197654ead508") == 0 ||
                                    string.Compare(oneSeparationPiece.EPieceTypeId, "46cc5f14c7ce45018a5970d8b92aa0bb") == 0)    // پیلوت
                                    continue;



                                newSeparationPieces.UnitId = oneSeparationPiece.UnitId;
                                newSeparationPieces.EstateSectionId = oneSeparationPiece.SectionId;
                                newSeparationPieces.EstateSubsectionId = oneSeparationPiece.SubSectionId;

                                string separationPieceKind = "";
                                switch (oneSeparationPiece.EKindPieceCode)
                                {
                                    case "3":     // آپارتمانی
                                    case "D1":    // قطعه
                                        if (output.Data.Separation.FunctionType == 2)
                                            separationPieceKind = "2";
                                        else
                                            separationPieceKind = "1";
                                        break;
                                    case "4":    // انباری
                                        separationPieceKind = "4";
                                        break;
                                    case "5":    // پارکینگ
                                        separationPieceKind = "3";
                                        break;
                                    case "3.":   // منضمات
                                        separationPieceKind = "5";
                                        break;
                                    case "2":    // مشاعات
                                        separationPieceKind = "6";
                                        break;
                                }
                                newSeparationPieces.DocumentEstateSeparationPieceKindId = separationPieceKind;
                                newSeparationPieces.EstatePieceTypeId = oneSeparationPiece.EPieceTypeId;
                                if (!string.IsNullOrWhiteSpace(newSeparationPieces.EstatePieceTypeId))
                                {
                                    var epeiceType = await _estatePieceTypeRepository.GetByIdAsync(cancellationToken, newSeparationPieces.EstatePieceTypeId);
                                    if (epeiceType == null)
                                    {
                                        apiResult.IsSuccess = false;
                                        apiResult.message.Add("نوع قطعه در سامانه یافت نشد");
                                        return apiResult;
                                    }
                                    newSeparationPieces.EstatePieceTypeTitle = epeiceType.Title;
                                }
                                newSeparationPieces.BasicPlaque = oneSeparationPiece.PlaqueOriginal;
                                newSeparationPieces.SecondaryPlaque = oneSeparationPiece.SidewayPlaque;
                                newSeparationPieces.DividedFromSecondaryPlaque = oneSeparationPiece.Separate;
                                newSeparationPieces.Floor = oneSeparationPiece.Class;
                                newSeparationPieces.Block = oneSeparationPiece.Block;
                                newSeparationPieces.PieceNo = oneSeparationPiece.Sector;
                                newSeparationPieces.Direction = oneSeparationPiece.Side;
                                newSeparationPieces.Area = oneSeparationPiece.Area.ToString();
                                newSeparationPieces.MeasurementUnitTypeId = oneSeparationPiece.AreaUnitId;
                                if (!string.IsNullOrWhiteSpace(newSeparationPieces.MeasurementUnitTypeId))
                                {
                                    var unitType = await _baseInfoServiceHelper.GetMeasurementUnitTypeById(new string[] { newSeparationPieces.MeasurementUnitTypeId }, cancellationToken);
                                    if (unitType == null || unitType.MesurementUnitTypeList == null || unitType.MesurementUnitTypeList.Count == 0)
                                    {
                                        apiResult.IsSuccess = false;
                                        apiResult.message.Add("واحد اندازه گیری مساحت  در سامانه یافت نشد");
                                        return apiResult;
                                    }
                                    newSeparationPieces.MeasurementUnitTypeTitle = unitType.MesurementUnitTypeList[0].Name;
                                }
                                newSeparationPieces.NorthLimits = oneSeparationPiece.NorthLimit;
                                newSeparationPieces.EasternLimits = oneSeparationPiece.EastLimit;
                                newSeparationPieces.SouthLimits = oneSeparationPiece.SouthLimit;
                                newSeparationPieces.WesternLimits = oneSeparationPiece.WestLimit;
                                newSeparationPieces.OtherAttachments = oneSeparationPiece.OtherDescription;
                                newSeparationPieces.Rights = oneSeparationPiece.EstateHightLaw;
                                newSeparationPieces.HasOwner = oneSeparationPiece.HasOwnership ? "1" : "2";
                                newSeparationPieces.HasOwnerTitle = oneSeparationPiece.HasOwnership ? "دارد" : "ندارد";
                                newSeparationPieces.Description = oneSeparationPiece.ProfitLaw;
                                newSeparationPieces.ScriptoriumId = _userService.UserApplicationContext.BranchAccess.BranchCode;
                                newSeparationPieces.Ilm = "1";
                                result.DocumentEstateSeparationPieceList.Add(newSeparationPieces);
                            }
                            apiResult.Data = result;
                        }
                        else
                        {
                            apiResult.message.Add("خطا در دریافت قطعات تفکیکی از سامانه املاک رخ داده است");
                            apiResult.IsSuccess = false;
                        }
                    }
                    else
                    {
                        apiResult.message.Add(output.Data.ErrorMessage);
                        apiResult.IsSuccess = false;
                    }

                }
                else
                {
                    apiResult.IsSuccess = false;
                    if (output.message.Count > 0)
                        apiResult.message.AddRange(output.message);
                    else
                        apiResult.message.Add(SystemMessagesConstant.Cadastre_Parts_Fetch_Failed);
                }
            }
            else
            {
                apiResult.message.Add(SystemMessagesConstant.Cadastre_Parts_Fetch_Failed);
                apiResult.IsSuccess = false;
            }

            return apiResult;
        }
    }
}
