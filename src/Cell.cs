using System.Text;

public class Cell {
    public string? Oem {get;}
    public string? Model {get;}
    public int? LaunchAnnounced {get;}
    public string? LaunchStatus {get;}
    public string? BodyDimensions {get;}
    public float? BodyWeight {get;}
    public string? BodySim {get;}
    public string? DisplayType {get;}
    public float? DisplaySize {get;}
    public string? DisplayResolution {get;}
    public string? FeaturesSensors {get;}
    public string? PlatformOS {get;}

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
