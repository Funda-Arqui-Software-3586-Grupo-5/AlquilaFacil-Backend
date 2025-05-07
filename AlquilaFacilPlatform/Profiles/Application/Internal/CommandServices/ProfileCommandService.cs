using AlquilaFacilPlatform.IAM.Domain.Model.Aggregates;
using AlquilaFacilPlatform.IAM.Domain.Respositories;
using AlquilaFacilPlatform.Profiles.Application.Internal.OutboundServices;
using AlquilaFacilPlatform.Profiles.Domain.Model.Aggregates;
using AlquilaFacilPlatform.Profiles.Domain.Model.Commands;
using AlquilaFacilPlatform.Profiles.Domain.Repositories;
using AlquilaFacilPlatform.Profiles.Domain.Services;
using AlquilaFacilPlatform.Shared.Domain.Repositories;

namespace AlquilaFacilPlatform.Profiles.Application.Internal.CommandServices;

public class ProfileCommandService(IUserExternalService userExternalService,IProfileRepository profileRepository, IUnitOfWork unitOfWork) : IProfileCommandService
{
    public async Task<Profile?> Handle(CreateProfileCommand command)
    {
        var profile = new Profile(command);
        var userExists = userExternalService.UserExistsById(command.UserId);
        if (!userExists)
        {
            throw new Exception("User does not exist");
        }

        if (command.Phone.Length < 9)
        {
            throw new Exception("Phone number must to be valid");
        }
        await profileRepository.AddAsync(profile);
        await unitOfWork.CompleteAsync();
        return profile;
    }

    public async Task<Profile> Handle(UpdateProfileCommand command)
    {
        var profile = await profileRepository.FindByIdAsync(command.Id);
        if (profile == null)
        {
            throw new Exception("Profile with ID does not exist");
        }
        var userId = profile.UserId;
        profile.Update(command);
        profile.UserId = userId;
        await unitOfWork.CompleteAsync();
        return profile;
    }
}