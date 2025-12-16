using Notary.SSAA.BO.Domain.Entities;

namespace Notary.SSAA.BO.WebApi.Seeds.SignRequest.Entities.SignRequestCollection
{
    public class SignRequestSubjectGroupCollection
    {

        public static List<SignRequestSubjectGroup> GetSeedData()
        {
            return new List<SignRequestSubjectGroup>
        {
            new SignRequestSubjectGroup
            {
                Id = "01",
                Code = "01",
                Title = "رضایت",
                State = "1",
                LegacyId = "01"
            },
            new SignRequestSubjectGroup
            {
                Id = "02",
                Code = "02",
                Title = "تعهد",
                State = "1",
                LegacyId = "02"
            },
            new SignRequestSubjectGroup
            {
                Id = "03",
                Code = "03",
                Title = "نمونه امضا",
                State = "1",
                LegacyId = "03"
            },
            new SignRequestSubjectGroup
            {
                Id = "04",
                Code = "04",
                Title = "استشهاد",
                State = "1",
                LegacyId = "04"
            },
            new SignRequestSubjectGroup
            {
                Id = "05",
                Code = "05",
                Title = "دادخواست",
                State = "1",
                LegacyId = "05"
            },
            new SignRequestSubjectGroup
            {
                Id = "06",
                Code = "06",
                Title = "اقرار",
                State = "1",
                LegacyId = "06"
            },
            new SignRequestSubjectGroup
            {
                Id = "07",
                Code = "07",
                Title = "وكالت",
                State = "1",
                LegacyId = "07"
            },
            new SignRequestSubjectGroup
            {
                Id = "08",
                Code = "08",
                Title = "درخواست",
                State = "1",
                LegacyId = "08"
            },
            new SignRequestSubjectGroup
            {
                Id = "09",
                Code = "09",
                Title = "نامه",
                State = "1",
                LegacyId = "09"
            },
            new SignRequestSubjectGroup
            {
                Id = "10",
                Code = "10",
                Title = "اعتراض",
                State = "1",
                LegacyId = "10"
            },
            new SignRequestSubjectGroup
            {
                Id = "99",
                Code = "99",
                Title = "سایر",
                State = "1",
                LegacyId = "99"
            }
        };
        }
    }
}

