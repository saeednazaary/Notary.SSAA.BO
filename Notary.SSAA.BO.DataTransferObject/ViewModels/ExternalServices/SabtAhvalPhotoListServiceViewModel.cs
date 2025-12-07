
namespace Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices
{
    public class SabtAhvalPhotoListServiceViewModel
    {
        public SabtAhvalPhotoListServiceViewModel()
        {
            PersonsData = new List<SabtAhvalPhotoServiceViewModel>();
        }
        public IList<SabtAhvalPhotoServiceViewModel> PersonsData { get; set; }

    }

    public class SabtAhvalPhotoServiceViewModel
    {
        public string Name { get; set; }
        public string Family { get; set; }
        public string NationalNo { get; set; }
        public byte[] PersonalImage { get; set; }
    }
}
