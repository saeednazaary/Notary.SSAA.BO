using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.Domain.RepositoryObjects.Grids
{
    public class ExecutiveRequestGrid
    {
        public ExecutiveRequestGrid()
        {
            GridItems = new();
            SelectedItems = new();
        }
        public int? TotalCount { get; set; }
        public List<ExecutiveRequestGridItem> GridItems { get; set; }
        public List<ExecutiveRequestGridItem> SelectedItems { get; set; }
    }

    public record ExecutiveRequestGridItem
    {
        public string Id { get; set; }
        //شناسه یکتا
        public string ExecutiveRequestNo { get; set; }
        //تاریخ درخواست
        public string ExecutiveRequestDate { get; set; }
        //نوع اجرائیه
        public string ExecutiveTypeTitle { get; set; }
        // وضعیت تقاضانامه
        public string ExecutiveRequestState { get; set; }
        //
        public string ExecutiveRequestPerson { get; set; }
        // لیست افراد تقاضانامه
        public List<string> ExecutiveRequestPersonList { get; set; }
        public bool IsSelected { get; set; }
    }
}
