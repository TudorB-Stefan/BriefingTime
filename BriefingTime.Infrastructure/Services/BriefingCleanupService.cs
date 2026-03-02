using BriefingTime.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BriefingTime.Infrastructure.Services;

public class BriefingCleanupService(IServiceScopeFactory scopeFactory, ILogger<BriefingCleanupService> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Cleanup service run: {time}",DateTimeOffset.Now);
            using (var scope = scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();var cutoffDate = DateTime.UtcNow.AddDays(-30);
                var oldBriefings = await context.Briefings
                    .Where(b => b.CreatedAt < cutoffDate)
                    .ToListAsync(stoppingToken);
                if (oldBriefings.Any())
                {
                    context.Briefings.RemoveRange(oldBriefings);
                    await context.SaveChangesAsync(stoppingToken);
                    logger.LogInformation("Deleted {count} old briefings.", oldBriefings.Count);
                }
            }
            await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
        }
    }
}