public class CropDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<int> GrowthCycles { get; set; }
    public List<int> Locations { get; set; }  // Now only a list of integers (Location IDs)
    public List<int>? HealthScores { get; set; }
    public int? SignalGeneratedId { get; set; }
}
