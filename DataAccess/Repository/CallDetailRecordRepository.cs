using Models;

namespace DataAccess.Repository
{
    public class CallDetailRecordRepository : ICallDetailRecordRepository
    {
        private readonly ApplicationDbContext _context;
        public CallDetailRecordRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddCallDetailRecord(CallDetailRecord callDetailRecord)
        {
            _context.CallDetailRecords.Add(callDetailRecord);
            _context.SaveChanges();
        }
        public decimal GetAverageCallCost()
        {
            return _context.CallDetailRecords.Average(record => record.Cost);
        }

        public TimeSpan GetLongestCallDuration()
        {
            return TimeSpan.FromSeconds(_context.CallDetailRecords.Max(record => record.Duration));
        }

        public int GetTotalCallsCount()
        {
            return _context.CallDetailRecords.Count();
        }

        public int GetTotalCallsCountInPeriod(DateTime startDate, DateTime endDate)
        {
            DateOnly start = ConvertToDateOnly(startDate);
            DateOnly end = ConvertToDateOnly(endDate);
            return _context.CallDetailRecords
                .Count(record => record.CallDate >= start && record.CallDate <= end);
        }

        public decimal GetTotalCostInPeriod(DateTime startDate, DateTime endDate)
        {
            DateOnly start = ConvertToDateOnly(startDate);
            DateOnly end = ConvertToDateOnly(endDate);
            return _context.CallDetailRecords
                .Where(record => record.CallDate >= start && record.CallDate <= end)
                .Sum(record => record.Cost);
        }
        private DateOnly ConvertToDateOnly(DateTime dateTime)
        {
            return new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);
        }

        public IEnumerable<CallDetailRecord> GetLongestCalls(int count)
        {
            return _context.CallDetailRecords
                .OrderByDescending(record => record.Duration)
                .Take(count)
                .ToList();
        }

        public IEnumerable<CallDetailRecord> GetCallsByCallerId(long callerId)
        {
            var callDetailRecords = _context.CallDetailRecords
                .Where(record => record.CallerId == callerId)
                .ToList();
            return callDetailRecords;
        }
    }
}
