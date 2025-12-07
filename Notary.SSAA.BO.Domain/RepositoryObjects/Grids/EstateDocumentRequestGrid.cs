using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.Domain.RepositoryObjects.Grids
{
    public class EstateDocumentRequestGrid
    {
        public EstateDocumentRequestGrid()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<EstateDocumentRequestGridItem> GridItems { get; set; }
        public List<EstateDocumentRequestGridItem> SelectedItems { get; set; }
    }
}
