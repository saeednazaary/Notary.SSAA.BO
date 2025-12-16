using Notary.SSAA.BO.Domain.Entities;

namespace Notary.SSAA.BO.WebApi.Seeds.SignRequest.Entities.Fingerprint
{
    public class PersonFingerprintUseCaseCollection
    {
        public static List<PersonFingerprintUseCase> GetSeedData()
        {
            return new List<PersonFingerprintUseCase>
            {
                new PersonFingerprintUseCase
                {
                    Id = "1",
                    Code = "01",
                    Title = "گواهي امضاء",
                    State = "1"
                },
                new PersonFingerprintUseCase
                {
                    Id = "4",
                    Code = "04",
                    Title = "تقاضانامه اجرائيه",
                    State = "1"
                },
                new PersonFingerprintUseCase
                {
                    Id = "5",
                    Code = "05",
                    Title = "خدمات تبعي اجرائيه",
                    State = "1"
                },
                new PersonFingerprintUseCase
                {
                    Id = "3",
                    Code = "03",
                    Title = "درخواست صدور سند مالکيت",
                    State = "1"
                },
                new PersonFingerprintUseCase
                {
                    Id = "2",
                    Code = "02",
                    Title = "سند رسمي",
                    State = "1"
                },
                new PersonFingerprintUseCase
                {
                    Id = "7",
                    Code = "07",
                    Title = "بررسی هویت اشخاص حقیقی",
                    State = "1"
                }
            };
        }
    }
}