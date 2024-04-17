using CsvHelper.Configuration;

namespace CallDetailRecorderAPI.Servicies
{
    public interface ICSVService
    {
        public IEnumerable<T> ReadCSV<T>(Stream file, CsvConfiguration configuration);
    }
}
