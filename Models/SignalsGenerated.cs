using System;

namespace AgriWeatherTracker.Models
{
    public class SignalGenerated
    {
        public int Id { get; set; } // Primary Key
        public int CropId { get; set; } // Foreign Key referencing Crop
        public int HealthScoreId { get; set; } // Foreign Key referencing HealthScore
        public int LocationId { get; set; }
        public decimal CommodityPrice { get; set; } // Commodity price at the time of signal generation
        public DateTime SignalDate { get; set; } // Date when the signal was generated
        public decimal Temperature { get; set; } // Temperature that triggered the signal
        public int WeatherId { get; set; } // Foreign Key referencing Weather data
        public decimal Score { get; set; } // Health score that led to the signal
        public string SignalType { get; set; } // Type of signal (e.g., "Buy", "Sell")
        public bool IsRealized { get; set; } // Indicates if the signal was realized
        public string AdditionalMetadata { get; set; } // Optional JSON field for extra data

        // Navigation properties (for EF Core)
        public Crop Crop { get; set; }
        public HealthScore HealthScore { get; set; }
        public Weather Weather { get; set; }
        public Location Location { get; set; }
    }
}
