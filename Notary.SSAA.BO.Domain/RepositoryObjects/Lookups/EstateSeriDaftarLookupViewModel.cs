using Notary.SSAA.BO.Domain.RepositoryObjects.Bases;

namespace Notary.SSAA.BO.Domain.RepositoryObjects.Lookups
{
    public class EstateSeriDaftarLookupViewModel
    {
        public EstateSeriDaftarLookupViewModel()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<EstateSeriDaftarLookupItem> GridItems { get; set; }
        public List<EstateSeriDaftarLookupItem> SelectedItems { get; set; }
    }
    public class EstateSeriDaftarLookupItem : BaseLookupItem
    {
        public string SSAACode { get; set; }

    }
}
