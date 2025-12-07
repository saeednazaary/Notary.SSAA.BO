using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices
{
    public class ILENCServiceViewModel
    {
        public ILENCServiceViewModel()
        {
            Result = new();
            TheCCompany = new();
        }
        public Result Result { get; set; }
        public TheCCompany TheCCompany { get; set; }
    }
    public class Result
    {
        public bool Successful { get; set; }
        public string Message { get; set; }
        public string No { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
    }
    public class TheCCompany
    {
        public TheCCompany()
        {
            TheCCompanyActivitiesList = new();
            TheCCompanyChangeHistoryList = new();
            TheCCompanyPersonList = new();
            TheCCompanyStocksList = new();
            TheCICompanyType = new();
            TheNAJAUnit = new();
            TheObjectState = new();
            TheUnit = new();

        }
        public string AddressDesc { get; set; }
        public string FixPhoneNumber { get; set; }
        public string IssuanceDate { get; set; }
        public string Name { get; set; }
        public string NationalCode { get; set; }
        public string PostCode { get; set; }
        public string RegisterDate { get; set; }
        public string RegisterNumber { get; set; }
        public List<TheCCompanyActivitiesList> TheCCompanyActivitiesList { get; set; }
        public List<object> TheCCompanyBranchList { get; set; }
        public List<TheCCompanyChangeHistoryList> TheCCompanyChangeHistoryList { get; set; }
        public List<object> TheCCompanyClusterGroupList { get; set; }
        public List<object> TheCCompanyFieldList { get; set; }
        public List<TheCCompanyPersonList> TheCCompanyPersonList { get; set; }
        public List<object> TheCCompanyStatuteList { get; set; }
        public List<TheCCompanyStocksList> TheCCompanyStocksList { get; set; }
        public TheCICompanyType TheCICompanyType { get; set; }
        public TheNAJAUnit TheNAJAUnit { get; set; }
        public TheObjectState TheObjectState { get; set; }
        public TheUnit TheUnit { get; set; }
    }

    public class TheCCompanyActivitiesList
    {
        public string ActivityDesc { get; set; }
        public decimal ActivityTimeState { get; set; }
        public decimal LicenseState { get; set; }
        public decimal State { get; set; }
    }

    public class TheCCompanyChangeHistoryList
    {
        public TheCCompanyChangeHistoryList()
        {
            TheCIDecision = new();
        }
        public string NextVal { get; set; }
        public string PreviousVal { get; set; }
        public string CreateDateTime { get; set; }
        public string Description { get; set; }
        public TheCIDecision TheCIDecision { get; set; }
    }

    public class TheCCompanyPersonList
    {
        public TheCCompanyPersonList()
        {
            TheCCompanyPersonPostList = new();
        }
        public string Address { get; set; }
        public string BirthDateSH { get; set; }
        public decimal CapitalStatus { get; set; }
        public decimal ClearanceConfessionStatus { get; set; }
        public decimal ExternalNationalStatus { get; set; }
        public string FatherNameFA { get; set; }
        public string FirstNameFA { get; set; }
        public string LastNameFA { get; set; }
        public decimal ManagingConfessionStatus { get; set; }
        public string MobileNumber4SMS { get; set; }
        public string NationalityCode { get; set; }
        public decimal PersonNumber { get; set; }
        public decimal PersonType { get; set; }
        public string PostCode { get; set; }
        public decimal Sex { get; set; }
        public decimal State { get; set; }
        public List<object> TheAgentsList { get; set; }
        public List<TheCCompanyPersonPostList> TheCCompanyPersonPostList { get; set; }
    }

    public class TheCCompanyPersonPostList
    {
        public TheCCompanyPersonPostList()
        {
            TheCIPostType = new();
        }
        public string EndDateValidity { get; set; }
        public decimal IsNonDirectMember { get; set; }
        public decimal IsNonPartnership { get; set; }
        public decimal ManagingConfessionStatus { get; set; }
        public decimal PeriodTime { get; set; }
        public decimal SignatureState { get; set; }
        public string StartDate { get; set; }
        public decimal State { get; set; }
        public TheCIPostType TheCIPostType { get; set; }
    }

    public class TheCCompanyStocksList
    {
        public TheCCompanyStocksList()
        {
            TheCISharesType = new();
        }
        public string FromDate { get; set; }
        public decimal NumberOfStock { get; set; }
        public decimal Price { get; set; }
        public decimal State { get; set; }
        public TheCISharesType TheCISharesType { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class TheCICompanyType
    {
        public string Code { get; set; }
        public string Title { get; set; }
    }

    public class TheCIDecision
    {
        public string Code { get; set; }
        public decimal State { get; set; }
        public string Title { get; set; }
    }

    public class TheCIPostType
    {
        public string Code { get; set; }
        public string Title { get; set; }
    }

    public class TheCISharesType
    {
        public string Code { get; set; }
        public decimal State { get; set; }
        public string Title { get; set; }
    }

    public class TheNAJAUnit
    {
        public string Code { get; set; }
        public decimal State { get; set; }
        public string UnitName { get; set; }
    }

    public class TheObjectState
    {
        public string Code { get; set; }
        public decimal StateType { get; set; }
        public string Title { get; set; }
    }

    public class TheUnit
    {
        public string Code { get; set; }
        public string UnitName { get; set; }
    }


}
