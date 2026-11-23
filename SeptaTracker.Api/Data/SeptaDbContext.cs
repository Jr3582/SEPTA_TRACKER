using Microsoft.EntityFrameworkCore;
using SeptaTracker.Api.Models;

namespace SeptaTracker.Api.Data;

public class SeptaDbContext : DbContext
{
    public DbSet<TrainSnapshot> TrainSnapshots { get; set; }
    public SeptaDbContext(DbContextOptions<SeptaDbContext> options) : base(options)
    {
        
    }
}