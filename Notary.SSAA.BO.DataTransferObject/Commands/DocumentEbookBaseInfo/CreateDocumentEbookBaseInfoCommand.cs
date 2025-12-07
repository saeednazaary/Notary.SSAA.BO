using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.SharedKernel.Result;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Commands.DocumentEbookBaseInfo
{
    public class CreateDocumentEbookBaseInfoCommand : BaseCommandRequest<ApiResult>
    {
        public CreateDocumentEbookBaseInfoCommand()
        {
        }
        public string LastClassifyNo { get; set; }
        public string LastRegisterVolumeNo { get; set; }
        public string LastRegisterPaperNo { get; set; }
        public string NumberOfBooksJari { get; set; }
        public string NumberOfBooksVehicle { get; set; }
        public string NumberOfBooksRahni { get; set; }
        public string NumberOfBooksOghaf { get; set; }
        public string NumberOfBooksArzi { get; set; }
        public string NumberOfBooksAgent { get; set; }
        public string NumberOfBooksOthers { get; set; }
        public string TotalBooks { get; set; }
    }
    }
