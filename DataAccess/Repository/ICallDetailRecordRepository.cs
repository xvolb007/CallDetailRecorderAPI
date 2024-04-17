using Models;

namespace DataAccess.Repository
{
    public interface ICallDetailRecordRepository
    {
        void AddCallDetailRecord(CallDetailRecord callDetailRecord);
        decimal GetAverageCallCost();
        TimeSpan GetLongestCallDuration();
        int GetTotalCallsCount();
        int GetTotalCallsCountInPeriod(DateTime startDate, DateTime endDate);
        decimal GetTotalCostInPeriod(DateTime startDate, DateTime endDate);
        IEnumerable<CallDetailRecord> GetLongestCalls(int count);
        IEnumerable<CallDetailRecord> GetCallsByCallerId(long callerId);
    }
}
