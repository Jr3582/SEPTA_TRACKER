using System.Globalization;
using SeptaTracker.Api.Data;
using SeptaTracker.Api.Models;

public class TrainSnapshotWorker : BackgroundService
{
    private readonly ILogger<TrainSnapshotWorker> _logger;
    private readonly IServiceScopeFactory _scopeFactory;

    public TrainSnapshotWorker(ILogger<TrainSnapshotWorker> logger, IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var timer = new PeriodicTimer(TimeSpan.FromMinutes(1));
        while(await timer.WaitForNextTickAsync(stoppingToken))
        {
            try {
                using var scope = _scopeFactory.CreateScope();
                var client = scope.ServiceProvider.GetRequiredService<SeptaClient>();
                var db = scope.ServiceProvider.GetRequiredService<SeptaDbContext>();
                var all_trains = await client.GetTrainsAsync();
                var capturedAt = DateTimeOffset.UtcNow;
                var snapshots = all_trains.Select(t => ToSnapshot(t ,capturedAt)).Where(s => s != null).OfType<TrainSnapshot>().ToList();
                db.TrainSnapshots.AddRange(snapshots);
                await db.SaveChangesAsync(stoppingToken);
                _logger.LogInformation("Saved {Count} train snapshots", snapshots.Count);
            } catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to save train snapshots");
            }
        }
    }
    
    private static TrainSnapshot? ToSnapshot(SeptaTrainDto dto, DateTimeOffset capturedAt)
    {
        if(string.IsNullOrWhiteSpace(dto.TrainNumber)|| string.IsNullOrWhiteSpace(dto.Line)) return null;
        if (!double.TryParse(dto.Latitude, NumberStyles.Float, CultureInfo.InvariantCulture, out var lat)) return null;
        if (!double.TryParse(dto.Longitude, NumberStyles.Float, CultureInfo.InvariantCulture, out var lon)) return null;
        return new TrainSnapshot
        {
            TrainNumber = dto.TrainNumber,
            Latitude = lat,
            Longitude = lon,
            CurrentStop = dto.CurrentStop ?? "",
            NextStop = dto.NextStop ?? "",
            Destination = dto.Destination ?? "",
            Line = dto.Line,
            Late = dto.Late,
            CapturedAt = capturedAt
        };
    }
}