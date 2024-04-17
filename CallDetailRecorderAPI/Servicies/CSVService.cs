using CsvHelper;
using CsvHelper.Configuration;

namespace CallDetailRecorderAPI.Servicies
{
    public class CSVService : ICSVService
    {
        public IEnumerable<T> ReadCSV<T>(Stream file, CsvConfiguration configuration)
        {
            var reader = new StreamReader(file);
            var csv = new CsvReader(reader, configuration);

            var records = csv.GetRecords<T>();
            return records;
        }
    }
}
