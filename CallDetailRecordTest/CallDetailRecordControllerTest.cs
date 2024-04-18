using CallDetailRecorderAPI.Controllers;
using CallDetailRecorderAPI.Servicies;
using DataAccess.Repository;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Text;

namespace CallDetailRecordTest
{
    public class CallDetailRecordControllerTest
    {
        private readonly ICallDetailRecordRepository _сallDetailRecordRepository;
        private readonly ICSVService _csvService;
        public CallDetailRecordControllerTest()
        {
            _сallDetailRecordRepository = A.Fake<ICallDetailRecordRepository>();
            _csvService = A.Fake<ICSVService>();

        }
        [Fact]
        public void AppendDatabaseFromCSV_Should_Add_Records_From_CSV()
        {
            // Arrange
            var controller = new CallDetailRecordController(_сallDetailRecordRepository, _csvService);
            var fileContent = "Id,CallerId,Duration\n1,Caller1,300\n2,Caller2,200";
            var file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes(fileContent)), 0, fileContent.Length, "data", "data.csv");

            var expectedRecords = new List<CallDetailRecord>
            {
                new CallDetailRecord { Id = 1, CallerId = 44444, Duration = 300 },
                new CallDetailRecord { Id = 2, CallerId = 55555, Duration = 200 }
            };

            A.CallTo(() => _csvService.ReadCSV<CallDetailRecord>(A<System.IO.Stream>._, A<CsvHelper.Configuration.CsvConfiguration>._))
                .Returns(expectedRecords);

            // Act
            var result = controller.AppendDatabaseFromCSV(new FormFileCollection { file });

            // Assert
            A.CallTo(() => _сallDetailRecordRepository.AddCallDetailRecord(A<CallDetailRecord>._)).MustHaveHappenedTwiceExactly();
            result.Should().BeOfType<OkResult>();
        }
        [Fact]
        public void GetAverageCallCost_Should_Return_Average_Cost()
        {
            // Arrange
            var controller = new CallDetailRecordController(_сallDetailRecordRepository, _csvService);
            decimal expectedAverageCost = 10.0m;

            A.CallTo(() => _сallDetailRecordRepository.GetAverageCallCost()).Returns(expectedAverageCost);

            // Act
            var result = controller.GetAverageCallCost();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult?.Value.Should().Be(expectedAverageCost);
        }
        [Fact]
        public void GetLongestCallDuration_Should_Return_Longest_Duration()
        {
            // Arrange
            var controller = new CallDetailRecordController(_сallDetailRecordRepository, _csvService);
            var expectedDuration = TimeSpan.FromMinutes(20);

            A.CallTo(() => _сallDetailRecordRepository.GetLongestCallDuration()).Returns(expectedDuration);

            // Act
            var result = controller.GetLongestCallDuration();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult?.Value.Should().Be(expectedDuration);
        }
        [Fact]
        public void GetTotalCallsCount_Should_Return_Total_Count()
        {
            // Arrange
            var controller = new CallDetailRecordController(_сallDetailRecordRepository, _csvService);
            int expectedTotalCount = 10;

            A.CallTo(() => _сallDetailRecordRepository.GetTotalCallsCount()).Returns(expectedTotalCount);

            // Act
            var result = controller.GetTotalCallsCount();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult?.Value.Should().Be(expectedTotalCount);
        }

        [Fact]
        public void GetTotalCallsCountInPeriod_Should_Return_Total_Count_In_Period()
        {
            // Arrange
            var controller = new CallDetailRecordController(_сallDetailRecordRepository, _csvService);
            int expectedTotalCount = 5;

            DateTime startDate = new DateTime(2024, 4, 1);
            DateTime endDate = new DateTime(2024, 4, 30);

            A.CallTo(() => _сallDetailRecordRepository.GetTotalCallsCountInPeriod(startDate, endDate)).Returns(expectedTotalCount);

            // Act
            var result = controller.GetTotalCallsCountInPeriod(startDate, endDate);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult?.Value.Should().Be(expectedTotalCount);
        }

        [Fact]
        public void GetTotalCostInPeriod_Should_Return_Total_Cost_In_Period()
        {
            // Arrange
            var controller = new CallDetailRecordController(_сallDetailRecordRepository, _csvService);
            decimal expectedTotalCost = 150.75m;

            DateTime startDate = new DateTime(2024, 4, 1);
            DateTime endDate = new DateTime(2024, 4, 30);

            A.CallTo(() => _сallDetailRecordRepository.GetTotalCostInPeriod(startDate, endDate)).Returns(expectedTotalCost);

            // Act
            var result = controller.GetTotalCostInPeriod(startDate, endDate);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult?.Value.Should().Be(expectedTotalCost);
        }

        [Fact]
        public void GetLongestCalls_Should_Return_Longest_Calls()
        {
            var controller = new CallDetailRecordController(_сallDetailRecordRepository, _csvService);
            var expectedCount = 2;
            var expectedLongestCalls = new List<CallDetailRecord>
            {
                new CallDetailRecord { Id = 1, Duration = 1200 },
                new CallDetailRecord { Id = 2, Duration = 900 }
            };

            A.CallTo(() => _сallDetailRecordRepository.GetLongestCalls(expectedCount)).Returns(expectedLongestCalls);

            // Act
            var result = controller.GetLongestCalls(expectedCount);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult?.Value.Should().BeEquivalentTo(expectedLongestCalls);
        }
        [Fact]
        public void GetCallsByCallerId_Should_Return_Calls_By_CallerId()
        {
            // Arrange
            var controller = new CallDetailRecordController(_сallDetailRecordRepository, _csvService);
            var expectedCallsByCallerId = new List<CallDetailRecord>
            {
                new CallDetailRecord { Id = 1, CallerId = 4444 },
                new CallDetailRecord { Id = 2, CallerId = 55555 }
            };
            int callerId = 123;

            A.CallTo(() => _сallDetailRecordRepository.GetCallsByCallerId(callerId)).Returns(expectedCallsByCallerId);

            // Act
            var result = controller.GetCallsByCallerId(callerId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult?.Value.Should().BeEquivalentTo(expectedCallsByCallerId);
        }
    }
}
