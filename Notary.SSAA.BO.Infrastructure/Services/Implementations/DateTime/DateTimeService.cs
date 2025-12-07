using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using System.Diagnostics;
using System.Globalization;

namespace Notary.SSAA.BO.Infrastructure.Services.Implementations.DateTime
{
    public sealed class DateTimeService : IDateTimeService
    {
        private readonly IDateTimeRepository _dapperRepository;
        private readonly Stopwatch Stopwatch = new();
        private string databaseDateTime = string.Empty;
        private readonly PersianCalendar persianCalendar = new();
        private readonly string dateTimeQuery = @"SELECT TO_CHAR(SYSTIMESTAMP, 'YYYY-MM-DD HH24:MI:SS.FF')
    FROM DUAL";
        public DateTimeService(IDateTimeRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
            databaseDateTime = _dapperRepository.GetCurrentDateTime(dateTimeQuery).Result;
            Stopwatch.Start();
        }
        public System.DateTime CurrentDateTime => System.DateTime.ParseExact(databaseDateTime, "yyyy-MM-dd HH:mm:ss.ffffff", CultureInfo.InvariantCulture).AddMilliseconds(Stopwatch.ElapsedMilliseconds);

        public DateOnly CurrentDate => DateOnly.FromDateTime(CurrentDateTime);

        public string CurrentPersianDateTime => string.Format("{0}/{1}/{2}-{3}:{4}", persianCalendar.GetYear(CurrentDateTime), persianCalendar.GetMonth(CurrentDateTime).ToString().PadLeft(2, '0'),
            persianCalendar.GetDayOfMonth(CurrentDateTime).ToString().PadLeft(2, '0'), CurrentDateTime.Hour.ToString().PadLeft(2, '0'), CurrentDateTime.Minute.ToString().PadLeft(2, '0'));

        public string CurrentPersianDate => string.Format("{0}/{1}/{2}", persianCalendar.GetYear(CurrentDateTime), persianCalendar.GetMonth(CurrentDateTime).ToString().PadLeft(2, '0'), persianCalendar.GetDayOfMonth(CurrentDateTime).ToString().PadLeft(2, '0'));

        public string CurrentTime => CurrentDateTime.ToString("HH:mm:ss");
    }
}
