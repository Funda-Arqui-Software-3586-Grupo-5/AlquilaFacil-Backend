using AlquilaFacilPlatform.Profiles.Domain.Model.Commands;
using AlquilaFacilPlatform.Profiles.Domain.Services;

namespace AlquilaFacilPlatform.Profiles.Interfaces.ACL.Services;

public class ProfilesContextFacade(IProfileCommandService profileCommandService) : IProfilesContextFacade
{
    public async Task<int> CreateProfile(string name, string? fatherName, string? motherName, string dateOfBirth, string documentNumber,
        string phone, int userId, string photoUrl)
    {
        var createProfileCommand = new CreateProfileCommand(name, fatherName, motherName, dateOfBirth, documentNumber, phone, userId, photoUrl);
        var profile = await profileCommandService.Handle(createProfileCommand);
        return profile?.Id ?? 0;
    }
}