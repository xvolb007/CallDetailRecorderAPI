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
        [HttpGet("average-call-cost")]
        public IActionResult GetAverageCallCost()
        {
            decimal averageCallCost = _сallDetailRecordRepository.GetAverageCallCost();
            return Ok(averageCallCost);
        }

        [HttpGet("longest-call-duration")]
        public IActionResult GetLongestCallDuration()
        {
            TimeSpan longestCallDuration = _сallDetailRecordRepository.GetLongestCallDuration();
            return Ok(longestCallDuration);
        }

        [HttpGet("total-calls-count")]
        public IActionResult GetTotalCallsCount()
        {
            int totalCallsCount = _сallDetailRecordRepository.GetTotalCallsCount();
            return Ok(totalCallsCount);
        }

        [HttpGet("total-calls-count-in-period")]
        public IActionResult GetTotalCallsCountInPeriod([FromQuery(Name = "startDate")] DateTime startDate, [FromQuery(Name = "endDate")] DateTime endDate)
        {
            int totalCallsCountInPeriod = _сallDetailRecordRepository.GetTotalCallsCountInPeriod(startDate, endDate);
            return Ok(totalCallsCountInPeriod);
        }

        [HttpGet("total-cost-in-period")]
        public IActionResult GetTotalCostInPeriod([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            decimal totalCostInPeriod = _сallDetailRecordRepository.GetTotalCostInPeriod(startDate, endDate);
            return Ok(totalCostInPeriod);
        }

        [HttpGet("longest-calls")]
        public IActionResult GetLongestCalls([FromQuery] int count)
        {
            IEnumerable<CallDetailRecord> longestCalls = _сallDetailRecordRepository.GetLongestCalls(count);
            return Ok(longestCalls);
        }

        [HttpGet("calls-by-caller-id")]
        public IActionResult GetCallsByCallerId([FromQuery] long callerId)
        {
            IEnumerable<CallDetailRecord> callsByCallerId = _сallDetailRecordRepository.GetCallsByCallerId(callerId);
            return Ok(callsByCallerId);
        }
    }
}
