using Notifications.Shared.Domain.Repositories;
using Notifications.Shared.Domain.Repositories;
using Notifications.Shared.Infrastructure.Persistence.EFC.Configuration;
using Notifications.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace Notifications.Shared.Infrastructure.Persistence.EFC.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context) => _context = context;
    public async Task CompleteAsync() => await _context.SaveChangesAsync();
}