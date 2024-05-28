using System.Text;

public class Cell {
    public string? Oem {get; set;}
    public string? Model {get; set;}
    public int? LaunchAnnounced {get; set;}
    public string? LaunchStatus {get; set;}
    public string? BodyDimensions {get; set;}
    public float? BodyWeight {get; set;}
    public string? BodySim {get; set;}
    public string? DisplayType {get; set;}
    public float? DisplaySize {get; set;}
    public string? DisplayResolution {get; set;}
    public string? FeaturesSensors {get; set;}
    public string? PlatformOS {get; set;}

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

    public override bool Equals(object? obj)
    {
        if (obj != null && obj.GetType() == this.GetType()) {
            Cell other = (Cell) obj;
            if (bothNullOrEqual<string?>(Oem, other.Oem) &&
                bothNullOrEqual<string?>(Model, other.Model) &&
                bothNullOrEqual<int?>(LaunchAnnounced, other.LaunchAnnounced) &&
                bothNullOrEqual<string?>(LaunchStatus, other.LaunchStatus) &&
                bothNullOrEqual<string?>(BodyDimensions, other.BodyDimensions) &&
                bothNullOrEqual<float?>(BodyWeight, other.BodyWeight) &&
                bothNullOrEqual<string?>(BodySim, other.BodySim) &&
                bothNullOrEqual<string?>(DisplayType, other.DisplayType) &&
                bothNullOrEqual<float?>(DisplaySize, other.DisplaySize) &&
                bothNullOrEqual<string?>(DisplayResolution, other.DisplayResolution) &&
                bothNullOrEqual<string?>(FeaturesSensors, other.FeaturesSensors) &&
                bothNullOrEqual<string?>(PlatformOS, other.PlatformOS)
            ) {return true;}
        }
        return false;
    }

    public bool bothNullOrEqual<T> (Object? value1, Object? value2) {
        if (value1 == null && value2 == null) return true;
        if (value1 == null || value2 == null) return false;
        if (value1.Equals(value2)) return true;
        return false;
    }
}
