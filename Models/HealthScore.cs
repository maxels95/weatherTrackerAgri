using AgriWeatherTracker.Models;

public class HealthScore
{
    public int Id { get; set; }
    public int CropId { get; set; }
    public Crop? Crop { get; set; }
    public int LocationId { get; set; }
    public Location Location { get; set; }
    public DateTime Date { get; set; }
    public double Score { get; set; }
    public int? SignalGeneratedId { get; set; }
    public SignalGenerated? SignalGenerated { get; set; }
}
