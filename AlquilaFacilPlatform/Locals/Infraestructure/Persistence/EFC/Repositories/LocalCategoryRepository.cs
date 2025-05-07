using AlquilaFacilPlatform.Locals.Domain.Model.Entities;
using AlquilaFacilPlatform.Locals.Domain.Model.ValueObjects;
using AlquilaFacilPlatform.Locals.Domain.Repositories;
using AlquilaFacilPlatform.Shared.Infrastructure.Persistence.EFC.Configuration;
using AlquilaFacilPlatform.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AlquilaFacilPlatform.Locals.Infraestructure.Persistence.EFC.Repositories;

public class LocalCategoryRepository(AppDbContext context)
    : BaseRepository<LocalCategory>(context), ILocalCategoryRepository
{
    public Task<bool> ExistsLocalCategory(EALocalCategoryTypes type)
    {
        return Context.Set<LocalCategory>().AnyAsync(x => x.Name == type.ToString());
    }

    public async Task<IEnumerable<LocalCategory>> GetAllLocalCategories()
    {
        return await Context.Set<LocalCategory>().ToListAsync();
    }
}
