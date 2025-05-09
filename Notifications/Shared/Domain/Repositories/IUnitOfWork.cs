namespace Notifications.Shared.Domain.Repositories;

public interface IUnitOfWork
{
    Task CompleteAsync();
}