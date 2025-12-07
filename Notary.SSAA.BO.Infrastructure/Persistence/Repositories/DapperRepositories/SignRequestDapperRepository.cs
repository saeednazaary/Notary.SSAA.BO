using Dapper;
using Microsoft.AspNetCore.Components;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;
using Notary.SSAA.BO.Domain.RepositoryObjects.Grids;
using Notary.SSAA.BO.Infrastructure.Persistence.Repositories.DapperRepositories.Base;
using Notary.SSAA.BO.Infrastructure.Persistence.SQLQueries;
using Notary.SSAA.BO.Utilities.Extensions;
using System;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Stimulsoft.Report.StiRecentConnections;

namespace Notary.SSAA.BO.Infrastructure.Persistence.Repositories.DapperRepositories
{
    internal class SignRequestDapperRepository : DRepository, ISignRequestDapperRepository
    {
        private IDbConnection _connection;
        public SignRequestDapperRepository(IDbConnection connection) : base(connection)
        {
            _connection = connection;
        }
        public async Task<SignRequestGrid> GetSignRequestGridItems(int pageIndex, int pageSize, ICollection<SearchData> GridSearchInput, string GlobalSearch, SortData gridSortInput, IList<string> selectedItemsIds, List<string> FieldsNotInFilterSearch, string scriptoriumId, SignRequestSearchExtraParams extraParams, CancellationToken cancellationToken, bool isOrderBy = false)
        {
            SignRequestGrid result=new SignRequestGrid();
            Dictionary<string,object> allSignRequestParameters = new();  
            Dictionary<string,object> allSignRequestPersonParameters = new();  
            string personFilter = @" AND";
            string signRequestFilter= @" AND";
            string order = @"";

            if (extraParams is not null)
            {
                if (!extraParams.SignRequestReqNo.IsNullOrWhiteSpace())
                {
                    signRequestFilter += " sr.REQ_NO = :SignRequestReqNo AND";
                    allSignRequestParameters.Add(":SignRequestReqNo", extraParams.SignRequestReqNo);
                }

                if (!extraParams.SignRequestNationalNo.IsNullOrWhiteSpace())
                {
                    signRequestFilter += " sr.NATIONAL_NO = :SignRequestNationalNo AND";
                    allSignRequestParameters.Add(":SignRequestNationalNo", extraParams.SignRequestNationalNo);
                }

                if (!extraParams.SignRequestStateId.IsNullOrWhiteSpace())
                {
                    signRequestFilter += " sr.STATE = :SignRequestStateId AND";
                    allSignRequestParameters.Add(":SignRequestStateId", extraParams.SignRequestStateId);
                }

                if (!extraParams.SignRequestReqDateFrom.IsNullOrWhiteSpace())
                {
                    signRequestFilter += " sr.REQ_DATE > :SignRequestReqDateFrom AND";
                    allSignRequestParameters.Add(":SignRequestReqDateFrom", extraParams.SignRequestReqDateFrom);
                }

                if (!extraParams.SignRequestReqDateTo.IsNullOrWhiteSpace())
                {
                    signRequestFilter += " sr.REQ_DATE < :SignRequestReqDateTo AND";
                    allSignRequestParameters.Add(":SignRequestReqDateTo", extraParams.SignRequestReqDateTo);
                }

                if (!extraParams.SignRequestSignDateFrom.IsNullOrWhiteSpace())
                {
                    signRequestFilter += " sr.SIGN_DATE > :SignRequestSignDateFrom AND";
                    allSignRequestParameters.Add(":SignRequestSignDateFrom", extraParams.SignRequestSignDateFrom);
                }

                if (!extraParams.SignRequestSignDateTo.IsNullOrWhiteSpace())
                {
                    signRequestFilter += " sr.SIGN_DATE < :SignRequestSignDateTo AND";
                    allSignRequestParameters.Add(":SignRequestSignDateTo", extraParams.SignRequestSignDateTo);
                }

                // SignRequestPerson parameters (for SQL)
                if (!extraParams.PersonNationalNo.IsNullOrWhiteSpace())
                {
                    personFilter += " srp.NATIONAL_NO = :PersonNationalNo AND";
                    allSignRequestParameters.Add(":PersonNationalNo", extraParams.PersonNationalNo);
                }
                if (!extraParams.PersonName.IsNullOrWhiteSpace())
                {
                    personFilter += " srp.NAME = :PersonName AND";
                    allSignRequestParameters.Add(":PersonName", extraParams.PersonName.PersianToArabic());
                }
                if (!extraParams.PersonFamily.IsNullOrWhiteSpace())
                {
                    personFilter += " srp.FAMILY = :PersonFamily AND";
                    allSignRequestParameters.Add(":PersonFamily", extraParams.PersonFamily.PersianToArabic());
                }
                if (!extraParams.PersonFatherName.IsNullOrWhiteSpace())
                {
                    personFilter += " srp.FATHER_NAME = :PersonFatherName AND";
                    allSignRequestParameters.Add(":PersonFatherName", extraParams.PersonFatherName.PersianToArabic());
                }
                if (!extraParams.PersonIdentityNo.IsNullOrWhiteSpace())
                {
                    personFilter += " srp.IDENTITY_NO = :PersonIdentityNo AND";
                    allSignRequestParameters.Add(":PersonIdentityNo", extraParams.PersonIdentityNo);
                }
                if (!extraParams.PersonSignClassifyNo.IsNullOrWhiteSpace())
                {
                    personFilter += " srp.SIGN_CLASSIFY_NO = :SignClassifyNo AND";
                    allSignRequestParameters.Add(":SignClassifyNo", int.Parse(extraParams.PersonSignClassifyNo));

                }
                if (!extraParams.PersonSeri.IsNullOrWhiteSpace())
                {
                    personFilter += " srp.SERI = :PersonSeri AND";
                    allSignRequestParameters.Add(":PersonSeri", int.Parse(extraParams.PersonSeri));
                }
                if (!extraParams.PersonSerial.IsNullOrWhiteSpace())
                {
                    personFilter += " srp.SERIAL = :PersonSerial AND";
                    allSignRequestParameters.Add(":PersonSerial", extraParams.PersonSerial);
                }
                if (!extraParams.PersonPostalCode.IsNullOrWhiteSpace())
                {
                    personFilter += " srp.POSTAL_CODE = :PersonPostalCode AND";
                    allSignRequestParameters.Add(":PersonPostalCode", extraParams.PersonPostalCode);
                }
                if (!extraParams.PersonMobileNo.IsNullOrWhiteSpace())
                {
                    personFilter += " srp.POSTAL_CODE = :PersonMobileNo AND";
                    allSignRequestParameters.Add(":PersonMobileNo", extraParams.PersonMobileNo);
                }
                if (!extraParams.PersonAddress.IsNullOrWhiteSpace())
                {
                    personFilter += " srp.ADDRESS = :PersonAddress AND";
                    allSignRequestParameters.Add(":PersonAddress", extraParams.PersonAddress);

                }
                if (!extraParams.PersonTel.IsNullOrWhiteSpace())
                {
                    personFilter += " srp.TEL = :PersonTel AND";
                    allSignRequestParameters.Add(":PersonTel", extraParams.PersonTel);
                }
                if (!extraParams.PersonBirthDate.IsNullOrWhiteSpace())
                {
                    personFilter += " srp.BIRTH_DATE = :PersonBirthDate AND";
                    allSignRequestParameters.Add(":PersonBirthDate", extraParams.PersonBirthDate);
                }
                if (!extraParams.PersonSexType.IsNullOrWhiteSpace())
                {
                    personFilter += " srp.SEX_TYPE = :PersonSexType AND";
                    allSignRequestParameters.Add(":PersonSexType", extraParams.PersonSexType);
                }

                if (extraParams.IsPersonOriginal.HasValue)
                {
                    personFilter += " srp.IS_ORIGINAL = :IsPersonOriginal AND";
                    allSignRequestParameters.Add(":IsPersonOriginal", (extraParams.IsPersonOriginal.Value ? "1" : "0"));
                }

                if (extraParams.IsPersonRelated.HasValue)
                {
                    personFilter += " srp.IS_RELATED = :IsPersonRelated AND";
                    allSignRequestParameters.Add(":IsPersonRelated", (extraParams.IsPersonRelated.Value ? "1" : "0"));
                }
                if (extraParams.IsPersonIranian.HasValue)
                {
                    personFilter += " srp.IS_IRANIAN = :IsPersonIranian AND";
                    allSignRequestParameters.Add(":IsPersonIranian", (extraParams.IsPersonIranian.Value ? "1" : "0"));
                }
                if (!extraParams.PersonAlphabetSeri.IsNullOrWhiteSpace())
                {
                    personFilter += " srp.SERI_ALPHA = :PersonAlphabetSeri AND";
                    allSignRequestParameters.Add(":PersonAlphabetSeri", extraParams.PersonAlphabetSeri);
                }
            }

            foreach (SearchData searchData in GridSearchInput)
            {
                switch (searchData.Filter.ToLower())
                {
                    case "reqno":
                        signRequestFilter += " sr.REQ_NO" + "= :" + searchData.Filter + " AND";
                        allSignRequestParameters.Add(':' + searchData.Filter, searchData.Value);
                        break;
                    case "nationalno":
                        signRequestFilter += " sr.NATIONAL_NO" + "= :" + searchData.Filter + " AND";
                        allSignRequestParameters.Add(':' + searchData.Filter, searchData.Value);
                        break;
                    case "reqdate":
                        signRequestFilter += " sr.sr.REQ_DATE" + "= :" + searchData.Filter + " AND";
                        allSignRequestParameters.Add(':' + searchData.Filter, searchData.Value);
                        break;
                    case "signdate":
                        signRequestFilter += " sr.SIGN_DATE" + "= :" + searchData.Filter + " AND";
                        allSignRequestParameters.Add(':' + searchData.Filter, searchData.Value);
                        break;
                    case "signrequestsubjecttitle":
                        signRequestFilter += " sr_SUBJECT.TITLE" + "= :" + searchData.Filter + " AND";
                        allSignRequestParameters.Add(':' + searchData.Filter, searchData.Value);
                        break;
                    case "signrequestgettertitle":
                        signRequestFilter += " sr_GETTER.TITLE" + "= :" + searchData.Filter + " AND";
                        allSignRequestParameters.Add(':' + searchData.Filter, searchData.Value);
                        break;
                    case "stateid":
                        signRequestFilter += " sr.STATE" + "= :" + searchData.Filter + " AND";
                        allSignRequestParameters.Add(':' + searchData.Filter, searchData.Value);
                        break;
                    case "persons":
                        personFilter += " (srp.NAME LIKE :PersonNameOrFamily OR srp.FAMILY LIKE :PersonNameOrFamily)";
                        allSignRequestParameters.Add(":PersonNameOrFamily", searchData.Value.PersianToArabic());
                        break;
                    default:
                        break;
                }
            }
            // Remove trailing "AND" if exists
            if (signRequestFilter.EndsWith("AND"))
            {
                signRequestFilter = signRequestFilter.Substring(0, signRequestFilter.Length - 4);
            }
            if (personFilter.EndsWith("AND"))
            {
                personFilter = personFilter.Substring(0, personFilter.Length - 4);
            }
            allSignRequestParameters.Add( "scriptoriumId", scriptoriumId);

            string countQuery = string.Format(SignRequestQuery.SignRequestCount, personFilter, signRequestFilter);
            result.TotalCount = (await _connection.QueryAsync<int>(countQuery, allSignRequestParameters)).FirstOrDefault();


            allSignRequestParameters.Add("Offset", (pageIndex - 1) * pageSize);
            allSignRequestParameters.Add("Fetch", pageSize);

            string searchquery = string.Format(SignRequestQuery.SignRequestSearch, personFilter, signRequestFilter,order);
            this.EnsureConnectionOpen();
            result.GridItems = (await _connection.QueryAsync<SignRequestGridItem>(searchquery, allSignRequestParameters)).ToList();

            var newIds = new List<byte[]>();
            foreach (var item in selectedItemsIds)
            {
                newIds.Add(item.ToGuid().ToOracleRaw16());
            }
            string selectedquery = string.Format(SignRequestQuery.SignRequestSelected,string.Join(",", newIds));
            var parameters = new DynamicParameters();
            parameters.Add(":Ids", newIds);
            if (pageIndex == 1 && selectedItemsIds.Count > 0)
                result.SelectedItems = (await _connection.QueryAsync<SignRequestGridItem>(SignRequestQuery.SignRequestSelected, parameters)).ToList();

            if (isOrderBy)
            {
                switch (gridSortInput.Sort.ToLower())
                {
                    case "reqno":
                        order += " ,sr.REQ_NO" + gridSortInput.SortType;
                        break;
                    case "nationalno":
                        order += " ,sr.NATIONAL_NO" + gridSortInput.SortType;
                        break;
                    case "reqdate":
                        order += " ,sr.REQ_DATE" + gridSortInput.SortType;
                        break;
                    case "signdate":
                        order += " ,sr.SIGN_DATE" + gridSortInput.SortType;

                        break;
                    case "signrequestsubjecttitle":
                        order += " ,sr.sr_SUBJECT.TITLE" + gridSortInput.SortType;

                        break;
                    case "signrequestgettertitle":
                        order += " ,sr_GETTER.TITLE" + gridSortInput.SortType;

                        break;
                    case "stateid":
                        order += " ,sr.STATE" + gridSortInput.SortType;
                        break;
                    default:
                        break;
                }
            }
            //foreach (var item in result.GridItems)
            //{
            //    item.Id = new Guid(BitConverter.ToString(item.byteId).Replace("-", "")).ToString();
            //}
            //foreach (var item in result.SelectedItems)
            //{
            //    item.Id = new Guid(BitConverter.ToString(item.byteId).Replace("-", "")).ToString();
            //}
            return result;

        }
    }
}
