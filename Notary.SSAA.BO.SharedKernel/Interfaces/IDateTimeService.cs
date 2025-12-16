namespace Notary.SSAA.BO.SharedKernel.Interfaces
{
    public interface IDateTimeService
    {
        DateTime CurrentDateTime { get; }
        string CurrentTime { get; }
        DateOnly CurrentDate { get; }
        string CurrentPersianDateTime { get; }
        string CurrentPersianDate { get; }
    }
}
