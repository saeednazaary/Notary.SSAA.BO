

using Notary.SSAA.BO.Domain.RepositoryObjects.Document;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Grids
{
    public sealed class DocumentSelselehAyadiGridViewModel
    {
        public DocumentSelselehAyadiGridViewModel()
        {
            GridItems = new();
        }
        public List<DocumentSelselehAyadiRepositoryObject> GridItems { get; set; }
    }

}
