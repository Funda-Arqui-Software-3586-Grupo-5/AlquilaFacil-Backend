using Microsoft.EntityFrameworkCore;
using Notifications.Domain.Models.Aggregates;
using Notifications.Domain.Repositories;
using Notifications.Shared.Infrastructure.Persistence.EFC.Configuration;
using Notifications.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace Notifications.Infrastructure.Persistence.EFC.Repositories;

public class NotificationRepository(AppDbContext context) : BaseRepository<Notification>(context), INotificationRepository
{
    public async Task<IEnumerable<Notification>> GetNotificationsByUserId(int userId)
    {
        return await Context.Set<Notification>().Where(n => n.UserId == userId).ToListAsync();
    }
}