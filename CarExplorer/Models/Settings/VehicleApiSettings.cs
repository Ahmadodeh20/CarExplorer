namespace CarExplorer.Models.Settings;

public class VehicleApiSettings
{
    public string BaseUrl { get; set; } = string.Empty;

    public VehicleApiEndpoints Endpoints { get; set; } = new();
}


public class VehicleApiEndpoints
{
    public string AllMakes { get; set; } = string.Empty;

    public string VehicleTypes { get; set; } = string.Empty;

    public string Models { get; set; } = string.Empty;
}