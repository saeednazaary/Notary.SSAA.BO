namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Services.ConfirmationAuthenticity
{
    public class TimeUnitViewModel
    {
        public TimeUnitViewModel()
        {
            TimeUnitsList = new();
        }
        public List<TimeUnitDataDetail> TimeUnitsList { get; set; }

    }
    public class TimeUnitDataDetail
    {
        public string Id { get; set; }
        public string Title { get; set; }
    }
}
