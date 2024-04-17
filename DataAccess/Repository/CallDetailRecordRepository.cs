namespace DataAccess.Repository
{
    public class CallDetailRecordRepository : ICallDetailRecordRepository
    {
        private readonly ApplicationDbContext _context;
        public CallDetailRecordRepository(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
