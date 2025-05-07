using AlquilaFacilPlatform.IAM.Domain.Model.Aggregates;
using AlquilaFacilPlatform.IAM.Domain.Respositories;
using AlquilaFacilPlatform.Locals.Domain.Model.Aggregates;
using AlquilaFacilPlatform.Locals.Domain.Model.Commands;
using AlquilaFacilPlatform.Locals.Domain.Model.Entities;
using AlquilaFacilPlatform.Locals.Domain.Repositories;
using AlquilaFacilPlatform.Locals.Domain.Services;
using AlquilaFacilPlatform.Locals.Infraestructure.Persistence.EFC.Repositories;
using AlquilaFacilPlatform.Shared.Domain.Repositories;

namespace AlquilaFacilPlatform.Locals.Application.Internal.CommandServices;

public class LocalCommandService (ILocalRepository localRepository, ILocalCategoryRepository localCategoryRepository, IUnitOfWork unitOfWork) : ILocalCommandService
{
    
    public async Task<Local?> Handle(CreateLocalCommand command)
    {
        var localCategory = await localCategoryRepository.FindByIdAsync(command.LocalCategoryId);
        if (localCategory == null)
        {
            throw new Exception("Local category not found");
        }

        if (command.Price <= 0)
        {
            throw new Exception("Price must be greater than 0");
        }
        var local = new Local(command);
        await localRepository.AddAsync(local);
        await unitOfWork.CompleteAsync();
        return local;
    }

    public async Task<Local?> Handle(UpdateLocalCommand command)
    {
        var local = await localRepository.FindByIdAsync(command.Id);
        if (local == null)
        {
            throw new Exception("Local not found");
        }
        var localCategory = await localCategoryRepository.FindByIdAsync(command.LocalCategoryId);
        if (localCategory == null)
        {
            throw new Exception("Local category not found");
        }

        if (command.Price <= 0)
        {
            throw new Exception("Price must be greater than 0");
        }
        localRepository.Update(local);
        local.Update(command);
        await unitOfWork.CompleteAsync();
        return local;
    }
}