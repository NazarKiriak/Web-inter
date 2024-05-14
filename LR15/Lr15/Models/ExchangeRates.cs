namespace LR14.Models
{
    public class ExchangeRates
    {
        public string Result { get; set; }
        public DateTime TimeLastUpdateUtc { get; set; }
        public DateTime TimeNextUpdateUtc { get; set; }
        public string BaseCode { get; set; }
        public Dictionary<string, decimal> Conversion_Rates { get; set; }
    }
}
