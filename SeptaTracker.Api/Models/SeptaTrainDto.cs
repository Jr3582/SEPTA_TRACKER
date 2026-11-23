using System.Text.Json.Serialization;

namespace SeptaTracker.Api.Models;

public class SeptaTrainDto
{
    [JsonPropertyName("trainno")]
    public string? TrainNumber { get; set; }

    [JsonPropertyName("currentstop")]
    public string? CurrentStop { get; set; }

    [JsonPropertyName("nextstop")]
    public string? NextStop { get; set; }

    [JsonPropertyName("dest")]
    public string? Destination { get; set; }

    [JsonPropertyName("line")]
    public string? Line { get; set; }

    [JsonPropertyName("late")]
    public int Late { get; set; }

    [JsonPropertyName("lat")]
    public string? Latitude { get; set; }

    [JsonPropertyName("lon")]
    public string? Longitude { get; set; }

}