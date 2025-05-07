using AlquilaFacilPlatform.Locals.Domain.Model.Aggregates;
using AlquilaFacilPlatform.Locals.Domain.Model.ValueObjects;
using AlquilaFacilPlatform.Shared.Domain.Repositories;

namespace AlquilaFacilPlatform.Locals.Domain.Repositories;

public interface ILocalRepository : IBaseRepository<Local>
{
   HashSet<string> GetAllDistrictsAsync();
   
   Task<IEnumerable<Local>> GetLocalsByCategoryIdAndCapacityrange(int categoryId, int minCapacity, int maxCapacity);
   
   Task<IEnumerable<Local>> GetLocalsByUserIdAsync(int userId);
   Task<bool> IsOwnerAsync(int userId, int localId);
   
}