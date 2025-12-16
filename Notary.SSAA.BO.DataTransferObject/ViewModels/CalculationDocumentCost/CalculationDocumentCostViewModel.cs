namespace Notary.SSAA.BO.DataTransferObject.ViewModels.CalculationDocumentCost
{
    public class CalculationDocumentCostViewModel
    {
        CalculationDocumentCostViewModel()
        {
            this.Items = new List<CalculationDocumentCostItem>();
        }
        public IList<CalculationDocumentCostItem> Items { get; set; }
        public string TotalPrice { get; set; }
    }

    public class CalculationDocumentCostItem
    {
        public string RowNo { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }
    }
}
