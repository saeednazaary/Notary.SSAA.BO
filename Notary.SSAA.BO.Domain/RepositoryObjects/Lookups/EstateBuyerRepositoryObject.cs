using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.Domain.RepositoryObjects.Lookups
{
    public class EstateBuyerRepositoryObject
    {
        public EstateBuyerRepositoryObject()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<EstateBuyerLookupItem> GridItems { get; set; }
        public List<EstateBuyerLookupItem> SelectedItems { get; set; }
    }
}
