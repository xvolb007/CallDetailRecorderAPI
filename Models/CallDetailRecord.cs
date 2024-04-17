namespace Models
{
    public class CallDetailRecord
    {
        public int? Id { get; set; }
        public int CallerId { get; set; }
        public int Recipient { get; set; }
        public DateOnly CallDate { get; set; }
        public TimeSpan EndTime { get; set; }
        public int Duration { get; set; }
        public decimal Cost { get; set; }
        public string Reference { get; set; }
        public string Currency { get; set; }
    }
}
