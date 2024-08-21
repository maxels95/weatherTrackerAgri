using System;

namespace AgriWeatherTracker.DTOs
{
    public class SignalGeneratedDto
    {
        public int Id { get; set; }
        public int CropId { get; set; }
        public int HealthScoreId { get; set; }
        public decimal CommodityPrice { get; set; }
        public DateTime SignalDate { get; set; }
        public decimal Temperature { get; set; }
        public int WeatherId { get; set; }
        public decimal Score { get; set; }
        public string SignalType { get; set; }
        public bool IsRealized { get; set; }
        public string AdditionalMetadata { get; set; }
    }
}
