using AlquilaFacilPlatform.Shared.Infrastructure.Persistence.EFC.Configuration;
using AlquilaFacilPlatform.Shared.Infrastructure.Persistence.EFC.Repositories;
using AlquilaFacilPlatform.Subscriptions.Domain.Model.Aggregates;
using AlquilaFacilPlatform.Subscriptions.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AlquilaFacilPlatform.Subscriptions.Infrastructure.Persistence.EFC.Repositories;

public class SubscriptionRepository(AppDbContext context)
    : BaseRepository<Subscription>(context), ISubscriptionRepository
{
    public async Task<Subscription?> FindByUserIdAsync(int userId)
    {
        return await context.Set<Subscription>()
            .FirstOrDefaultAsync(s => s.UserId == userId);
    }
    
    public async Task<IEnumerable<Subscription>> FindByUsersIdAsync(List<int> usersId)
    {
        return await context.Set<Subscription>()
            .Where(s => usersId.Contains(s.UserId))
            .ToListAsync();
    }
}