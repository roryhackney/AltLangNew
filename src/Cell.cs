using System.Text;

public class Cell {
    private string? Oem {get; set;}
    private string? Model {get; set;}
    private int? LaunchAnnounced {get; set;}
    private string? LaunchStatus {get; set;}
    private string? BodyDimensions {get; set;}
    private float? BodyWeight {get; set;}
    private string? BodySim {get; set;}
    private string? DisplayType {get; set;}
    private float? DisplaySize {get; set;}
    private string? DisplayResolution {get; set;}
    private string? FeaturesSensors {get; set;}
    private string? PlatformOS {get; set;}

    public Cell(string? Oem, string? Model, int? LaunchAnnounced, string? LaunchStatus, string? BodyDimensions,
                float? BodyWeight, string? BodySim, string? DisplayType, float? DisplaySize, string? DisplayResolution,
                string? FeaturesSensors, string? PlatformOS) {
                    this.Oem = Oem;
                    this.Model = Model;
                    this.LaunchAnnounced = LaunchAnnounced;
                    this.LaunchStatus = LaunchStatus;
                    this.BodyDimensions = BodyDimensions;
                    this.BodyWeight = BodyWeight;
                    this.BodySim = BodySim;
                    this.DisplayType = DisplayType;
                    this.DisplaySize = DisplaySize;
                    this.DisplayResolution = DisplayResolution;
                    this.FeaturesSensors = FeaturesSensors;
                    this.PlatformOS = PlatformOS;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder("{");
        sb.Append(Oem).Append("\n");
        sb.Append(Model).Append("\n");
        sb.Append(LaunchAnnounced).Append("\n");
        sb.Append(LaunchStatus).Append("\n");
        sb.Append(BodyDimensions).Append("\n");
        sb.Append(BodyWeight).Append("\n");
        sb.Append(BodySim).Append("\n");
        sb.Append(DisplayType).Append("\n");
        sb.Append(DisplaySize).Append("\n");
        sb.Append(DisplayResolution).Append("\n");
        sb.Append(FeaturesSensors).Append("\n");
        sb.Append(PlatformOS).Append("}").Append("\n");
        return sb.ToString();
    }
}
