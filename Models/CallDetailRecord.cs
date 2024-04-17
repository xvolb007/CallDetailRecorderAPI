using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using CsvHelper.TypeConversion;

namespace Models
{
    public class CallDetailRecord
    {
        public int? Id { get; set; }
        [Name("caller_id")]
        //int sk
        public long CallerId { get; set; }

        [Name("recipient")]
        public long Recipient { get; set; }
        [TypeConverter(typeof(CustomDateTimeConverter))]

        [Name("call_date")]
        public DateOnly CallDate { get; set; }

        [Name("end_time")]
        public TimeSpan EndTime { get; set; }

        [Name("duration")]
        public int Duration { get; set; }

        [Name("cost")]
        public decimal Cost { get; set; }

        [Name("reference")]
        public string Reference { get; set; }

        [Name("currency")]
        public string Currency { get; set; }
    }
    //Convert date from yyyy/MM/dd to yyyy-MM-dd
    public class CustomDateTimeConverter : DateTimeConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (DateOnly.TryParseExact(text, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out var result))
            {
                return result;
            }
            return base.ConvertFromString(text, row, memberMapData);
        }
    }
}
