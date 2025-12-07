using MediatR;
using Microsoft.Extensions.Configuration;
using Serilog;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate;
using Notary.SSAA.BO.Domain.Abstractions.Base;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.ServiceHandler.Base;
using Notary.SSAA.BO.SharedKernel.Constants;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.SharedKernel.Result;


namespace Notary.SSAA.BO.ServiceHandler.ExternalService.Estate
{
    public class DealSummaryToByteArrayServiceHandler : BaseServiceHandler<DealSummaryToByteArrayInput, ApiResult<byte[]>>
    {
        private readonly IHttpEndPointCaller _httpEndPointCaller;
        private readonly IConfiguration _configuration;
        private readonly IEstateSectionRepository _estateSectionRepository;
        private readonly IEstateSubSectionRepository _estateSubSectionRepository;
        private readonly IEstateSeriDaftarRepository _estateSeriDaftarRepository;
        private readonly IEstateInquiryRepository _estateInquiryRepository;
        private readonly IRepository<EstateOwnershipType> _estateOwnershipRepository;
        private readonly IRepository<EstateTransitionType> _estateTransitionRepository;
        private readonly IRepository<DealSummaryTransferType> _dealSummaryTransferTypeRepository;
        private readonly IRepository<DealsummaryUnrestrictionType> _dealsummaryUnrestrictionTypeRepository;
        private readonly IRepository<DealsummaryPersonRelateType> _dealsummaryPersonRelateTypeRepository;
        private readonly IRepository<EstateInquiryPerson> _estateInquiryPersonRepository;
        private readonly IRepository<DealSummaryPerson> _dealSummaryPersonRepository;
        private DealSummaryObjectConvertHelper helper;
        public DealSummaryToByteArrayServiceHandler(IMediator mediator, IUserService userService, ILogger logger,
            IHttpEndPointCaller httpEndPointCaller, IConfiguration configuration,
            IEstateSectionRepository estateSectionRepository, IEstateInquiryRepository estateInquiryRepository, IEstateSeriDaftarRepository estateSeriDaftarRepository, IEstateSubSectionRepository estateSubSectionRepository
            , IRepository<EstateOwnershipType> estateOwnershipRepository, IRepository<EstateTransitionType> estateTransitionRepository
            , IRepository<DealSummaryTransferType> dealSummaryTransferTypeRepository
            , IRepository<DealsummaryUnrestrictionType> dealsummaryUnrestrictionTypeRepository
            , IRepository<DealsummaryPersonRelateType> dealsummaryPersonRelateTypeRepository,
             IRepository<EstateInquiryPerson> estateInquiryPersonRepository,
             IRepository<DealSummaryPerson> dealSummaryPersonRepository) : base(mediator, userService, logger)
        {
            _httpEndPointCaller = httpEndPointCaller;
            _configuration = configuration;
            _estateInquiryRepository = estateInquiryRepository;
            _estateSectionRepository = estateSectionRepository;
            _estateSeriDaftarRepository = estateSeriDaftarRepository;
            _estateSubSectionRepository = estateSubSectionRepository;
            _estateOwnershipRepository = estateOwnershipRepository;
            _estateTransitionRepository = estateTransitionRepository;
            _dealsummaryPersonRelateTypeRepository = dealsummaryPersonRelateTypeRepository;
            _dealSummaryTransferTypeRepository = dealSummaryTransferTypeRepository;
            _dealsummaryUnrestrictionTypeRepository = dealsummaryUnrestrictionTypeRepository;
            _estateInquiryPersonRepository = estateInquiryPersonRepository;
            _dealSummaryPersonRepository = dealSummaryPersonRepository;

            helper = new DealSummaryObjectConvertHelper(
                _mediator,
                _estateSectionRepository,
                _estateInquiryRepository,
                _estateSeriDaftarRepository,
                _estateSubSectionRepository,
                _estateOwnershipRepository,
                _estateTransitionRepository,
                _dealSummaryTransferTypeRepository,
                _dealsummaryUnrestrictionTypeRepository,
                _dealsummaryPersonRelateTypeRepository,
                _estateInquiryPersonRepository,
                _dealSummaryPersonRepository);
        }

        protected async override Task<ApiResult<byte[]>> ExecuteAsync(DealSummaryToByteArrayInput request, CancellationToken cancellationToken)
        {
            ApiResult<byte[]> result = new ApiResult<byte[]>();
            if (request == null || request.DSUDealSummaryObject == null)
            {
                result.IsSuccess = false;
                result.message.Add("خلاصه معامله ورودی خالی می باشد");
                return result;
            }
            var correctedData = await helper.CorrectInput(new List<DSUDealSummaryObject> { request.DSUDealSummaryObject }, cancellationToken);
            
            result.Data = SignVerifyHelper.ToByteArray(correctedData[0]);
            return result;
        }

        protected override bool HasAccess(DealSummaryToByteArrayInput request, IList<string> userRoles)
        {
            return userRoles.Contains(RoleConstants.Sardaftar) || userRoles.Contains(RoleConstants.Daftaryar) || userRoles.Contains(RoleConstants.SanadNevis) || userRoles.Contains(RoleConstants.Admin);
        }
    }

    public class DealSummaryPersonSignObject
    {
        public string Name { get; set; }
        public string Family { get; set; }
        public string MelliCode { get; set; }
        public string Id { get; set; }
        public string FatherName { get; set; }
        public string Seri { get; set; }
        public int Serial { get; set; }
        public string BirthDate { get; set; }
        public string BirthIssueId { get; set; }
        public string IdentificationNo { get; set; }

        public int? Sex { get; set; }

        public short ExecuteTransfer { get; set; }

        public string NationalityId { get; set; }
        public string CityId { get; set; }
        public string Address { get; set; }
        public string postalcode { get; set; }
        public string Sharepart { get; set; }
        public string ShareTotal { get; set; }
        public string ShareContext { get; set; }
        public string OctantQuarter { get; set; }
        public string OctantQuarterPart { get; set; }
        public string OctandQuarterTotal { get; set; }
        public string RelationKind { get; set; }

    }
    public class DealSummarySignObject
    {
        public DealSummarySignObject()
        {
            Parties = new List<DealSummaryPersonSignObject>();
        }
        public string Id { get; set; }
        public string DealNo { get; set; }
        public string DealDate { get; set; }
        public string DealMainDate { get; set; }
        public string TransfertypeId { get; set; }
        public string EstateInquiryId { get; set; }
        public string SystemDealNo { get; set; }
        public string IssuerId { get; set; }
        public string IssueeId { get; set; }
        public string SendDate { get; set; }
        public string SaleInstance { get; set; }
        public string OnershipTypeId { get; set; }
        public string basic { get; set; }
        public string BasicAppendant { get; set; }
        public string Secondary { get; set; }
        public string SecondaryAppendant { get; set; }
        public string GeolocationId { get; set; }
        public string SectionId { get; set; }
        public string SubsectionId { get; set; }
        public string postalcode { get; set; }
        public string Area { get; set; }
        public string RegisterNo { get; set; }
        public string PrintedNumberDoc { get; set; }
        public string OfficeNo { get; set; }
        public string PageNo { get; set; }
        public string NotebookSeri { get; set; }
        public string NotebookSupplement { get; set; }
        public string Amount { get; set; }
        public string AmmountUnit { get; set; }
        public string Attached { get; set; }
        // public string Response { get; set; }
        //public string ResponseDate { get; set; }


        //در تاریخ 93/03/07 اضافه شدند برای وریفای کردن خلاصه معامله های قبل باید این فیلد ها را در نظر نگرفت
        public string RemoveRestrictionNo { get; set; }
        public string RemoveRestrictionDate { get; set; }
        public string RemoveRestrictionOrganizationId { get; set; }
        public string RemoveRestriction { get; set; }

        public List<DealSummaryPersonSignObject> Parties { get; set; }


    }
    public class SignVerifyHelper
    {
       
        public static DealSummarySignObject ToDealSummary(DSUDealSummaryObject deal)
        {
            var d = new DealSummarySignObject();
            d.Id = deal.Id;
            d.AmmountUnit = deal.AmountUnitId;
            d.Amount = (deal.Amount.HasValue) ? deal.Amount.Value.ToString() : null;
            d.Area = (deal.Area.HasValue) ? deal.Area.Value.ToString() : null;
            d.Attached = deal.Attached;
            d.basic = deal.Basic;
            d.BasicAppendant = deal.BasicAppendant;
            d.DealDate = deal.DealDate;
            d.DealMainDate = deal.DealMainDate;
            d.DealNo = deal.DealNo;
            d.EstateInquiryId = deal.ESTEstateInquiryId;
            d.GeolocationId = deal.GeoLocationId;
            d.IssueeId = deal.DealSummeryIssueeId;
            d.IssuerId = deal.DealSummeryIssuerId;
            d.NotebookSeri = deal.NotebookSeri;
            d.NotebookSupplement = deal.NotebookSupplement;
            d.OfficeNo = deal.OfficeNo;
            d.OnershipTypeId = deal.DSUOwnerShipTypeId;
            d.PageNo = deal.PageNo;
            d.postalcode = deal.PostalCode;
            d.PrintedNumberDoc = deal.PrintNumberDoc;
            d.RegisterNo = deal.RegisterNo;
            // d.Response = deal.RESPONSE;
            // d.ResponseDate = deal.RESPONSEDATE;
            d.SaleInstance = deal.saleInstatnce;
            d.Secondary = deal.Secondary;
            d.SecondaryAppendant = deal.SecondaryAppendant;
            d.SectionId = deal.SectionId;
            //d.SendDate = deal.SENDDATE;
            d.SubsectionId = deal.SubsectionId;
            d.SystemDealNo = deal.SystemDealNo;
            d.TransfertypeId = deal.DSUTransferTypeId;

            //در تاریخ 93/03/07 اضافه شدند برای وریفای کردن خلاصه معامله های قبل باید این فیلد ها را در نظر نگرفت
            d.RemoveRestrictionNo = deal.RemoveRestrictionNo;
            d.RemoveRestrictionDate = deal.RemoveRestrictionDate;
            d.RemoveRestriction = deal.DSURemoveRestirctionTypeId;
            d.RemoveRestrictionOrganizationId = deal.unrestrictedOrganizationId;



            foreach (var rlp in deal.TheDSURealLegalPersonList)
            {
                var drlp = new  DealSummaryPersonSignObject();
                drlp.Address = rlp.Address;
                drlp.BirthDate = rlp.BirthDate;
                drlp.BirthIssueId = rlp.IssuePlaceId;
                drlp.CityId = rlp.CityId;
                drlp.ExecuteTransfer = (rlp.ExecutiveTransfer.HasValue) ? ((rlp.ExecutiveTransfer.Value) ? Convert.ToInt16(1) : Convert.ToInt16(0)) : Convert.ToInt16(-1);
                drlp.Family = rlp.Family;
                drlp.FatherName = rlp.FatherName;
                drlp.Id = rlp.Id;
                drlp.IdentificationNo = (!string.IsNullOrWhiteSpace(rlp.IdentificationNo)) ? rlp.IdentificationNo : "-1";
                drlp.MelliCode = rlp.NationalCode;
                drlp.Name = rlp.Name;
                drlp.NationalityId = rlp.NationalityId;
                drlp.OctandQuarterTotal = rlp.OctantQuarterTotal;
                drlp.OctantQuarter = rlp.OctantQuarter;
                drlp.OctantQuarterPart = rlp.OctantQuarterPart;
                drlp.postalcode = (rlp.PostalCode != null) ? rlp.PostalCode : null;
                drlp.Seri = rlp.Seri;
                drlp.Serial = (rlp.Serial.HasValue) ? Convert.ToInt32(rlp.Serial.Value) : -1;
                drlp.Sex = rlp.sex;
                drlp.ShareContext = rlp.ShareContext;
                drlp.Sharepart = (rlp.SharePart.HasValue) ? rlp.SharePart.Value.ToString() : null;
                drlp.ShareTotal = (rlp.ShareTotal.HasValue) ? rlp.ShareTotal.Value.ToString() : null;
                drlp.RelationKind = rlp.DSURelationKindId;
                d.Parties.Add(drlp);
            }

            return d;
        }

        public static byte[] ToByteArray(DSUDealSummaryObject data)
        {
            List<byte> lst = new List<byte>();
            DealSummarySignObject obj = ToDealSummary(data);

            Type type = obj.GetType();
            System.Reflection.PropertyInfo[] props = type.GetProperties();
            List<System.Reflection.PropertyInfo> mpl = props.OrderBy(tp => tp.Name).ToList();
            foreach (System.Reflection.PropertyInfo p in mpl)
            {
                object pvalue = p.GetValue(obj, null);
                if (pvalue != null)
                {
                    string propertyName = p.PropertyType.Name.ToLower();
                    if (propertyName.Contains("string"))
                    {
                        string str = pvalue.ToString();
                        lst.AddRange(System.Text.Encoding.Unicode.GetBytes(str));
                    }
                    if (propertyName.Contains("int32"))
                    {
                        lst.AddRange(BitConverter.GetBytes(Convert.ToInt32(pvalue)));
                    }
                    if (propertyName.Contains("int64"))
                    {
                        lst.AddRange(BitConverter.GetBytes(Convert.ToInt64(pvalue)));
                    }
                    if (propertyName.Contains("int16"))
                    {
                        lst.AddRange(BitConverter.GetBytes(Convert.ToInt16(pvalue)));
                    }
                    if (propertyName.Contains("double"))
                    {
                        lst.AddRange(BitConverter.GetBytes(Convert.ToDouble(pvalue)));
                    }

                    if (propertyName.Contains("dsureallegalperson"))
                    {
                        var rp = (List<DealSummaryPersonSignObject>)pvalue;

                        foreach (var d in rp)
                        {
                            Type t = d.GetType();
                            System.Reflection.PropertyInfo[] parr = t.GetProperties();
                            List<System.Reflection.PropertyInfo> pl = parr.OrderBy(p1 => p1.Name).ToList();

                            foreach (System.Reflection.PropertyInfo p1 in pl)
                            {
                                object pvalue1 = p1.GetValue(d, null);
                                if (pvalue1 != null)
                                {
                                    propertyName = p1.PropertyType.Name.ToLower();
                                    if (propertyName.Contains("string"))
                                    {
                                        string str = pvalue1.ToString();
                                        lst.AddRange(System.Text.Encoding.Unicode.GetBytes(str));
                                    }
                                    if (propertyName.Contains("int32"))
                                    {
                                        lst.AddRange(BitConverter.GetBytes(Convert.ToInt32(pvalue1)));
                                    }
                                    if (propertyName.Contains("int64"))
                                    {
                                        lst.AddRange(BitConverter.GetBytes(Convert.ToInt64(pvalue1)));
                                    }
                                    if (propertyName.Contains("int16"))
                                    {
                                        lst.AddRange(BitConverter.GetBytes(Convert.ToInt16(pvalue1)));
                                    }
                                    if (propertyName.Contains("double"))
                                    {
                                        lst.AddRange(BitConverter.GetBytes(Convert.ToDouble(pvalue1)));
                                    }
                                }
                            }
                        }

                    }

                }
            }
            return lst.ToArray();
        }        
    }
}
