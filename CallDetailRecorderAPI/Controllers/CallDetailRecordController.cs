using CallDetailRecorderAPI.Servicies;
using CsvHelper.Configuration;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Globalization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CallDetailRecorderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallDetailRecordController : ControllerBase
    {
        private readonly ICallDetailRecordRepository _сallDetailRecordRepository;
        private readonly ICSVService _csvService;
        public CallDetailRecordController(ICallDetailRecordRepository CallDetailRecordRepository, ICSVService csvService)
        {
            _сallDetailRecordRepository = CallDetailRecordRepository;
            _csvService = csvService;
        }
        [HttpPost("append-database-from-csv")]
        public IActionResult AppendDatabaseFromCSV([FromForm] IFormFileCollection file)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null,
                MissingFieldFound = null
            };
            var callDetailRecords = _csvService.ReadCSV<CallDetailRecord>(file[0].OpenReadStream(), config);
            foreach (var callDetailRecord in callDetailRecords)
            {
                _сallDetailRecordRepository.AddCallDetailRecord(callDetailRecord);
            }
            return Ok();
        }
    }
}
