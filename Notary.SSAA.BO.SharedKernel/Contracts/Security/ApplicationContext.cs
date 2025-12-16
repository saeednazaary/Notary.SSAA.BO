using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  Notary.SSAA.BO.SharedKernel.Contracts.Security
{
    public class ApplicationUser
    {
        public ApplicationUser(string id, string userName, string name, string family)
        {
            Id = id;
            Name = name;
            Family = family;
            UserName = userName;

        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string UserName { get; set; }

    }
    public class ApplicationUserRole
    {
        public ApplicationUserRole(string roleId, string roleName)
        {
            RoleId = roleId;
            RoleName = roleName;
        }

        public string RoleId { get; set; }
        public string RoleName { get; set; }
    }
    public class ApplicationBranchAccess
    {
        public ApplicationBranchAccess(string branchAccessId, string branchId, string branchCode, string branchName, string branchAccessDisplayName,string issueDocAccess,string isBranchOwner )
        {
            BranchAccessId = branchAccessId;
            BranchId = branchId;
            BranchCode = branchCode;
            BranchName = branchName;
            BranchAccessDisplayName = branchAccessDisplayName;
            IsBranchOwner=isBranchOwner;
            IssueDocAccess= issueDocAccess;
        }

        public string BranchAccessId { get; set; }
        public string BranchId { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string BranchAccessDisplayName { get; set; }
        public string IssueDocAccess { get; set; }
        public string IsBranchOwner { get; set; }
    }

    public class UserApplicationContext
    {
        public UserApplicationContext(ApplicationUser user, ApplicationUserRole userRole, ApplicationBranchAccess branchAccess, string token, ScriptoriumInformation scriptoriumInformation)
        {
            User = user;
            UserRole = userRole;
            BranchAccess = branchAccess;
            Token = token;
            ScriptoriumInformation = scriptoriumInformation;
        }

        public ApplicationUser User { get; set; }
        public ApplicationUserRole UserRole { get; set; }
        public ApplicationBranchAccess BranchAccess { get; set; }
        public ScriptoriumInformation ScriptoriumInformation { get; set; }
        public string Token { get; set; }

    }

    public class ScriptoriumData
    {
        public ScriptoriumData()
        {
            ScriptoriumList = new();
        }
        public List<ScriptoriumInformation> ScriptoriumList { get; set; }

    }
    public class ScriptoriumInformation
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Province { get; set; }
        public string ProvinceId { get; set; }
        public string GeoLocationId { get; set; }
        public string ScriptoriumNo { get; set; }
        public string GeoLocationName { get; set; }
        public string Address { get; set; }
        public string ExordiumFullName { get; set; }
        public string Tel { get; set; }
        public UnitInformation Unit { get; set; }

        public static implicit operator Task<object>(ScriptoriumInformation v)
        {
            throw new NotImplementedException();
        }
    }
    public class UnitInformation
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Province { get; set; }
        public string No { get; set; }
        public string GeoLocationId { get; set; }
        public string LegacyId { get; set; }
        public string LevelCode { get; set; }

    }
}
