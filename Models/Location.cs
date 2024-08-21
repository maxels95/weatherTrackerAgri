using System;
using System.Collections.Generic;
using AgriWeatherTracker.Models;

public class Location
{
    public int Id { get; set; }
    public string Country { get; set; }
    public string Name { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public int? CropId { get; set; }  // Foreign key to Crop
    public Crop Crop { get; set; }   // Navigation property back to Crop
    public ICollection<SignalGenerated>? SignalsGenerated { get; set; }
}