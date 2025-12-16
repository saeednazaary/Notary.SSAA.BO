using Notary.SSAA.BO.Domain.Entities;

namespace Notary.SSAA.BO.WebApi.Seeds.SignRequest.Entities.SignRequestCollection
{

    public partial class SsrSignEbookBaseinfoCollection
    {
        public static List<SsrSignEbookBaseinfo> GetSeedData()
        {
            return new List<SsrSignEbookBaseinfo>
        {
            new SsrSignEbookBaseinfo
            {
                Id = Guid.NewGuid(),
                ScriptoriumId = "57999",
                LastClassifyNo = 124,
                LastRegisterVolumeNo = "12",
                LastRegisterPaperNo = 5,
                NumberOfBooks = 15,
                ExordiumConfirmDate = "1402/10/10",
                ExordiumConfirmTime = "10:10",
                ExordiumDigitalSign = "d"
            },
            new SsrSignEbookBaseinfo
            {
                Id = Guid.NewGuid(),
                ScriptoriumId = "52539",
                LastClassifyNo = 134,
                LastRegisterVolumeNo = "22",
                LastRegisterPaperNo = 6,
                NumberOfBooks = 17,
                ExordiumConfirmDate = "1402/10/10",
                ExordiumConfirmTime = "10:10",
                ExordiumDigitalSign = "d"
            }
        };
        }
    }
}
