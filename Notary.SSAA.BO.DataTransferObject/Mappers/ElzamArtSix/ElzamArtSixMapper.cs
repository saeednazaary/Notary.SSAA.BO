using Mapster;
using Notary.SSAA.BO.DataTransferObject.Commands.ElzamArtSix;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ElzamArtSix;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ElzamArtSixPerson;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ElzamArtSixResponse;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Utilities.Extensions;
using static Notary.SSAA.BO.SharedKernel.Constants.EstateConstant;

namespace Notary.SSAA.BO.DataTransferObject.Mappers.ElzamArtSix
{
    public class ElzamArtSixMapper
    {
        public static ElzamArtSixViewModel ToViewModelElzamArtSix(Domain.Entities.SsrArticle6Inq ElzamArtSix)
        {
            TypeAdapterConfig<Domain.Entities.SsrArticle6Inq, ElzamArtSixViewModel>
            .NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.No, src => src.No)
            .Map(dest => dest.CreateDate, src => src.CreateDate)
            .Map(dest => dest.CreateTime, src => src.CreateTime)
            .Map(dest => dest.Type, src => src.Type == true ? "1" : "2")
            .Map(dest => dest.ProvinceId, src => new List<string> { { src.ProvinceId.ToString().Trim() } })
            .Map(dest => dest.CountyId, src => new List<string> { { src.CountyId.ToString().Trim() } })
            .Map(dest => dest.EstateMap, src => src.EstateMap)
            .Map(dest => dest.ElzamArtSixOrganId, src => src.SsrArticle6InqReceivers.Select(x => x.SsrArticle6OrganId))
            .Map(dest => dest.EstateUnitId, src => new List<string> { { src.EstateUnitId.ToString().Trim() } })
            .Map(dest => dest.EstateSectionId, src => new List<string> { { src.EstateSectionId.ToString().Trim() } })
            .Map(dest => dest.EstateSubsectionId, src => new List<string> { { src.EstateSubsectionId.ToString().Trim() } })
            .Map(dest => dest.EstateMainPlaque, src => src.EstateMainPlaque)
            .Map(dest => dest.EstateSecondaryPlaque, src => src.EstateSecondaryPlaque)
            .Map(dest => dest.EstRelatedInfoLoadBySvc, src => src.EstRelatedInfoLoadBySvc.ToBoolean())
            .Map(dest => dest.EstateArea, src => src.EstateArea)
            .Map(dest => dest.EstatePostCode, src => src.EstatePostCode)
            .Map(dest => dest.Attachments, src => src.Attachments)
            .Map(dest => dest.SendDate, src => src.SendDate)
            .Map(dest => dest.SendTime, src => src.SendTime)
            .Map(dest => dest.ScriptoriumId, src => src.ScriptoriumId)
            .Map(dest => dest.WorkflowStatesId, src => src.WorkflowStatesId)
            .Map(dest => dest.WorkflowStatesTitle, src => src.WorkflowStates != null ? src.WorkflowStates.Title : "")
            .Map(dest => dest.Ilm, src => src.Ilm)
            .Map(dest => dest.TrackingCode, src => src.TrackingCode)
            .Map(dest => dest.EstateDocElectronicNoteNo, src => src.EstateDocElectronicNoteNo)
            .Map(dest => dest.Address, src => src.Address)
            .Map(dest => dest.EstateUsingId, src => new List<string> { { src.EstateUsingId.ToString().Trim() } })
            .Map(dest => dest.EstateTypeId, src => new List<string> { { src.EstateTypeId.ToString().Trim() } })
            .Map(dest => dest.ElzamArtSixSellerPerson, src => ToViewModelElzamArtSixPerson(src.SsrArticle6InqPeople.Where(x=>x.RelationType == EstateElzamSixRelationType.Seller).FirstOrDefault()))
            .Map(dest => dest.ElzamArtSixBuyerPerson, src => ToViewModelElzamArtSixPerson(src.SsrArticle6InqPeople.Where(x => x.RelationType == EstateElzamSixRelationType.Buyer).FirstOrDefault()))
            .Map(dest => dest.ElzamArtSixResponses, src => ToViewModelElzamArtSixResponseList(src.SsrArticle6InqResponses.ToList()))
            .IgnoreNonMapped(true);

            var returnObject = ElzamArtSix.Adapt<ElzamArtSixViewModel>();
            return returnObject;
        }
        public static List<ElzamArtSixResponseViewModel> ToViewModelElzamArtSixResponseList(List<SsrArticle6InqResponse> ElzamArtSixResponses)
        {
            List<ElzamArtSixResponseViewModel> ResponseList = new List<ElzamArtSixResponseViewModel>();

            foreach (SsrArticle6InqResponse Response in ElzamArtSixResponses)
            {
                TypeAdapterConfig<Domain.Entities.SsrArticle6InqResponse, ElzamArtSixResponseViewModel>
                            .NewConfig()
                            .Map(dest => dest.Id, src => src.Id)
                            .Map(dest => dest.ResponseDate, src => src.ResponseDate)
                            .Map(dest => dest.ResponseTime, src => src.ResponseTime)
                            .Map(dest => dest.ResponseNo, src => src.ResponseNo)
                            .Map(dest => dest.ResponseType, src => src.ResponseType ? "موافقت" : "عدم موافقت")
                            .Map(dest => dest.ElzamArtSixId, src => src.SsrArticle6InqId)
                            .Map(dest => dest.ElzamArtSixOrganId, src => src.SenderOrgId)
                            .Map(dest => dest.ElzamArtSixOrganCode, src => src.SenderOrg.Code)
                            .Map(dest => dest.ElzamArtSixOrganTitle, src => src.SenderOrg.Title)
                            .Map(dest => dest.OppositionId, src => src.OppositionId)
                            .Map(dest => dest.OppositionTitle, src => src.Opposition.Title)
                            .Map(dest => dest.State, src => src.State)
                            .IgnoreNonMapped(true);
                ResponseList.Add(Response.Adapt<ElzamArtSixResponseViewModel>());
            }
            return ResponseList;
        }
        public static ElzamArtSixPersonViewModel ToViewModelElzamArtSixPerson(Domain.Entities.SsrArticle6InqPerson ElzamArtSixPerson)
        {
            TypeAdapterConfig<Domain.Entities.SsrArticle6InqPerson, ElzamArtSixPersonViewModel>
            .NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Family, src => src.Family)
            .Map(dest => dest.FatherName, src => src.FatherName)
            .Map(dest => dest.IdentityNo, src => src.IdentityNo)
            .Map(dest => dest.BirthDate, src => src.BirthDate)
            .Map(dest => dest.NationalityCode, src => src.NationalityCode)
            .Map(dest => dest.Seri, src => src.Seri)
            .Map(dest => dest.AlphabetSeri, src => src.SeriAlpha)
            .Map(dest => dest.SerialNo, src => src.SerialNo)
            .Map(dest => dest.SharePart, src => src.SharePart)
            .Map(dest => dest.ShareText, src => src.ShareText)
            .Map(dest => dest.ShareTotal, src => src.ShareTotal)
            .Map(dest => dest.IssuePlaceText, src => src.IssuePlaceText)
            .Map(dest => dest.ForiegnBirthPlace, src => src.ForiegnBirthPlace)
            .Map(dest => dest.ForiegnIssuePlace, src => src.ForiegnIssuePlace)
            .Map(dest => dest.SexType, src => src.SexType)
            .Map(dest => dest.PersonType, src => src.PersonType)
            .Map(dest => dest.RelationType, src => src.RelationType)
            .Map(dest => dest.PostalCode, src => src.PostalCode)
            .Map(dest => dest.Address, src => src.Address)
            .Map(dest => dest.ExecutiveTransfer, src => src.ExecutiveTransfer)
            .Map(dest => dest.IssuePlaceId, src => new List<string> { { src.IssuePlaceId != null ? src.IssuePlaceId.ToString().Trim() : null } })
            .Map(dest => dest.NationalityId, src => new List<string> { { src.NationalityId != null ? src.NationalityId.ToString().Trim() : null } })
            .Map(dest => dest.ScriptoriumId, src => src.ScriptoriumId)
            .Map(dest => dest.BirthPlaceId, src => new List<string> { { src.BirthPlaceId != null ? src.BirthPlaceId.ToString().Trim() : "" } })
            .Map(dest => dest.CityId, src => new List<string> { { src.CityId != null ? src.CityId.ToString().Trim() : "" } })
            .Map(dest => dest.SsrArticleInqId, src => new List<string> { { src.SsrArticle6InqId != null ? src.SsrArticle6InqId.ToString().Trim() : "" } })
            .Map(dest => dest.Timestamp, src => src.Timestamp)
            .Map(dest => dest.HasSmartCard, src => src.HasSmartCard)
            .Map(dest => dest.MocState, src => src.MocState)
            .Map(dest => dest.IsSanaChecked, src => src.SanaState.ToBoolean())
            .Map(dest => dest.TfaRequired, src => src.TfaRequired)
            .Map(dest => dest.TfaState, src => src.TfaState)
            .Map(dest => dest.IsIranian, src => src.IsIranian.ToBoolean())
            .Map(dest => dest.IsSabteAhvalChecked, src => src.IsSabtahvalChecked.ToBoolean())
            .Map(dest => dest.IsSabtahvalCorrect, src => src.IsSabtahvalCorrect.ToBoolean())
            .Map(dest => dest.IsIlencChecked, src => src.IsIlencChecked.ToBoolean())
            .Map(dest => dest.IsIlencCorrect, src => src.IsIlencCorrect)
            .Map(dest => dest.IsForeignerssysChecked, src => src.IsForeignerssysChecked)
            .Map(dest => dest.IsForeignerssysCorrect, src => src.IsForeignerssysCorrect)
            .Map(dest => dest.Ilm, src => src.Ilm)
            .Map(dest => dest.MobileNo, src => src.MobileNo)
            .Map(dest => dest.MobileNoState, src => src.MobileNoState.ToBoolean())
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.Tel, src => src.Tel)
            .Map(dest => dest.Fax, src => src.Fax)
            .Map(dest => dest.IsAlive, src => src.IsAlive.ToBoolean())
            .Map(dest => dest.IsOriginal, src => src.IsOriginal.ToBoolean())
            .Map(dest => dest.IsRelated, src => src.IsRelated.ToBoolean())
            .Map(dest => dest.SanaHasOrganizationChart, src => src.SanaHasOrganizationChart)
            .Map(dest => dest.SanaInquiryDate, src => src.SanaInquiryDate)
            .Map(dest => dest.SanaInquiryTime, src => src.SanaInquiryTime)
            .Map(dest => dest.SanaMobileNo, src => src.SanaMobileNo)
            .Map(dest => dest.SanaOrganizationCode, src => src.SanaOrganizationCode)
            .Map(dest => dest.SanaOrganizationName, src => src.SanaOrganizationName)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.LastLegalPaperDate, src => src.LastLegalPaperDate)
            .Map(dest => dest.LastLegalPaperNo, src => src.LastLegalPaperNo)
            .Map(dest => dest.LegalpersonNatureId, src => new List<string> { { src.LegalpersonNatureId != null ? src.LegalpersonNatureId.ToString().Trim() : "" } })
            .Map(dest => dest.LegalpersonTypeId, src => new List<string> { { src.LegalpersonTypeId != null ? src.LegalpersonTypeId.ToString().Trim() : "" } })
            .Map(dest => dest.CompanyTypeId, src => new List<string> { { src.CompanyTypeId != null ? src.CompanyTypeId.ToString().Trim() : "" } })
            .Map(dest => dest.OwnershipDocumentType, src => src.OwnershipDocumentType)
            .Map(dest => dest.ManagerNationalNo, src => src.Managernationalcode)
            .Map(dest => dest.ManagerName, src => src.Managername)
            .Map(dest => dest.ManagerFamily, src => src.Managerfmily)
            .Map(dest => dest.LawyerNationalId, src => src.Lawyernationalid)
            .Map(dest => dest.LawyerName, src => src.Lawyername)
            .Map(dest => dest.LawyerMobile, src => src.Lawyermobile)
            .Map(dest => dest.LawyerPostalCode, src => src.Lawyerpostalcode)
            .Map(dest => dest.LawyerBirthDate, src => src.Lawyerbirthdate)
            .Map(dest => dest.LawyerFatherName, src => src.Lawyerfathername)
            .Map(dest => dest.HasLawyer, src => src.Haslawyer.ToBoolean())
            .IgnoreNonMapped(true);

            var returnObject = ElzamArtSixPerson.Adapt<ElzamArtSixPersonViewModel>();
            return returnObject;
        }
        public static void ToEntityElzamArtSixCreate(ref Domain.Entities.SsrArticle6Inq entity, CreateElzamArtSixCommand viewModel, string scriptoriumId)
        {
            var config = new TypeAdapterConfig();

            /*<TSource, TDestination>*/
            config.NewConfig<CreateElzamArtSixCommand, Domain.Entities.SsrArticle6Inq>()
            .Map(dest => dest.Id, src => src.Id != string.Empty ? Guid.Parse(src.Id) : Guid.NewGuid())
            .Map(dest => dest.Type, src => src.Type.ToBoolean())
            .Map(dest => dest.ProvinceId, src => src.ProvinceId.FirstOrDefault())
            .Map(dest => dest.CountyId, src => src.CountyId.FirstOrDefault())
            .Map(dest => dest.EstateMap, src => src.EstateMap)
            .Map(dest => dest.EstateUnitId, src => src.EstateUnitId.FirstOrDefault())
            .Map(dest => dest.EstateSectionId, src => src.EstateSectionId.FirstOrDefault())
            .Map(dest => dest.EstateSubsectionId, src => src.EstateSubsectionId.FirstOrDefault())
            .Map(dest => dest.EstateMainPlaque, src => src.EstateMainPlaque)
            .Map(dest => dest.EstateSecondaryPlaque, src => src.EstateSecondaryPlaque)
            .Map(dest => dest.EstateArea, src => src.EstateArea)
            .Map(dest => dest.EstatePostCode, src => src.EstatePostCode)
            .Map(dest => dest.Attachments, src => src.Attachments)
            .Map(dest => dest.SendDate, src => src.SendDate)
            .Map(dest => dest.SendTime, src => src.SendTime)
            .Map(dest => dest.ScriptoriumId, src => scriptoriumId)
            .Map(dest => dest.EstateDocElectronicNoteNo, src => src.EstateDocElectronicNoteNo)
            .Map(dest => dest.EstRelatedInfoLoadBySvc, src => src.EstRelatedInfoLoadBySvc == true ? 1 : 2)
            .Map(dest => dest.Ilm, src => "1")
            .Map(dest => dest.TrackingCode, src => src.TrackingCode)
            .Map(dest => dest.Address, src => src.Address)
            .Map(dest => dest.EstateUsingId, src => src.EstateUsingId.FirstOrDefault())
            .Map(dest => dest.EstateTypeId, src => src.EstateTypeId.FirstOrDefault())
            .Map(dest => dest.SsrArticle6InqPeople, src => new List<SsrArticle6InqPerson> { ToEntityElzamArtSixPerson(src.ElzamArtSixSellerPerson, scriptoriumId) , ToEntityElzamArtSixPerson(src.ElzamArtSixBuyerPerson, scriptoriumId) })
           .IgnoreNonMapped(true);

            config.Compile();
            viewModel.Adapt(entity, config);
        }

        private static SsrArticle6InqPerson ToEntityElzamArtSixPerson(CreateElzamArtSixPersonCommand elzamArtSixPerson, string scriptoriumId)
        {
            SsrArticle6InqPerson Person = new SsrArticle6InqPerson();
            var config = new TypeAdapterConfig();

            /*<TSource, TDestination>*/
            config.NewConfig<CreateElzamArtSixPersonCommand, Domain.Entities.SsrArticle6InqPerson>()
            .Map(dest => dest.Id, src => Guid.NewGuid())
            .Map(dest => dest.Address, src => src.Address)
            .Map(dest => dest.BirthDate, src => src.BirthDate)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Family, src => src.Family)
            .Map(dest => dest.FatherName, src => src.FatherName)
            .Map(dest => dest.NationalityCode, src => src.NationalityCode)
            .Map(dest => dest.MobileNo, src => src.MobileNo)
            .Map(dest => dest.PostalCode, src => src.PostalCode)
            .Map(dest => dest.ScriptoriumId, src => scriptoriumId)
            .Map(dest => dest.IdentityNo, src => src.IdentityNo)
            .Map(dest => dest.IsSabtahvalChecked, src => src.IsSabteAhvalChecked == true ? 1 : 2)
            .Map(dest => dest.IsIlencChecked, src => src.IsIlencChecked == true ? 1 : 2)
            .Map(dest => dest.SanaState, src => src.IsSanaChecked == true ? 1 : 2)
            .Map(dest => dest.IssuePlaceText, src => src.IssuePlaceText)
            .Map(dest => dest.IsAlive, src => src.IsAlive == true ? 1 : 2)
            .Map(dest => dest.MobileNoState, src => Convert.ToBoolean(src.MobileNoState) == true ? 1 : 2)
            .Map(dest => dest.IsRelated, src => 1)
            .Map(dest => dest.SeriAlpha, src => src.AlphabetSeri)
            .Map(dest => dest.Seri, src => src.Seri)
            .Map(dest => dest.SerialNo, src => src.SerialNo)
            .Map(dest => dest.ShareTotal, src => !src.ShareTotal.IsNullOrEmpty()? src.ShareTotal:null)
            .Map(dest => dest.SharePart, src => !src.SharePart.IsNullOrEmpty()? src.SharePart:null)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.SexType, src => src.SexType)
            .Map(dest => dest.IsOriginal, src => 1)
            .Map(dest => dest.IsIranian, src => 1)
            .Map(dest => dest.Ilm, src => "1")
            .Map(dest => dest.PersonType, src => src.PersonType)
            .Map(dest => dest.RelationType, src => src.RelationType)
            .Map(dest => dest.Managernationalcode, src => src.ManagerNationalNo)
            .Map(dest => dest.Managername, src => src.ManagerName)
            .Map(dest => dest.Managerfmily, src => src.ManagerFamily)
            .Map(dest => dest.Lawyernationalid, src => src.LawyerNationalId)
            .Map(dest => dest.Lawyername, src => src.LawyerName)
            .Map(dest => dest.Lawyermobile, src => src.LawyerMobile)
            .Map(dest => dest.Lawyerpostalcode, src => src.LawyerPostalCode)
            .Map(dest => dest.Lawyerbirthdate, src => src.LawyerBirthDate)
            .Map(dest => dest.Lawyerfathername, src => src.LawyerFatherName)
            .Map(dest => dest.Haslawyer, src => src.HasLawyer == true ? 1 : 2)
            .Map(dest => dest.RelationType, src => "1")
           .IgnoreNonMapped(true);
            config.Compile();
            Person.Adapt<ElzamArtSixPersonViewModel>();
            return elzamArtSixPerson.Adapt(new SsrArticle6InqPerson(), config);
        }

    }
}
