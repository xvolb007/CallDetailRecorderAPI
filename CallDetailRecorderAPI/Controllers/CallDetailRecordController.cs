using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CallDetailRecorderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallDetailRecordController : ControllerBase
    {
        private readonly ICallDetailRecordRepository _сallDetailRecordRepository;
        public CallDetailRecordController(ICallDetailRecordRepository CallDetailRecordRepository)
        {
            _сallDetailRecordRepository = CallDetailRecordRepository;
        }
    }
}
