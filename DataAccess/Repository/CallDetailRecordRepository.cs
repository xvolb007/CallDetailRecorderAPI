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
    }
}
