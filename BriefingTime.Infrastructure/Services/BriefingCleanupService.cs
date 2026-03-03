using BriefingTime.Core.Interfaces.Repositories;
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
                var briefingRepository = scope.ServiceProvider.GetRequiredService<IBriefingRepository>();
                var oldBriefings = await briefingRepository.GetOldBriefings();
                var count = oldBriefings.Count();
                if (count>0)
                {
                    foreach (var oldBriefing in oldBriefings)
                    {
                        await briefingRepository.DeleteAsync(oldBriefing);
                    }
                    logger.LogInformation("Deleted {count} old briefings.", oldBriefings.Count());
                }
            }
            await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
        }
    }
}