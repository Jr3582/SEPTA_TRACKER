namespace SeptaTracker.Api.Models;

public class TrainSnapshot
{
    public int Id { get; set; }
    public required string TrainNumber { get; set; }
    public required string CurrentStop { get; set; }
    public required string NextStop { get; set; }
    public required string Destination { get; set; }
    public required string Line { get; set; }
    public required int Late { get; set; }
    public required double Latitude { get; set; }
    public required double Longitude { get; set; }
    public DateTimeOffset CapturedAt { get; set; }

}