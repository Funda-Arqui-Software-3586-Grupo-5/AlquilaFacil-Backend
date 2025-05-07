using AlquilaFacilPlatform.Profiles.Domain.Model.Aggregates;
using AlquilaFacilPlatform.Profiles.Domain.Model.Commands;
using AlquilaFacilPlatform.Profiles.Domain.Model.Queries;
using AlquilaFacilPlatform.Profiles.Domain.Services;
using Moq;

namespace AlquilaFacilPlatform.Tests.UnitTests;

public class ProfileTests
{ 
    [Fact]
    public void Profile_Constructor_WithParameters_ShouldInitializeProperties()
    { 
        var command = new CreateProfileCommand("Jane", "Johnson", "Doe", "1995-02-15", "87654321", "987654321",1, "photoUrl");
        var profile = new Profile(command);
        
        Assert.Equal(command.Name, profile.Name.Name);
        Assert.Equal(command.FatherName, profile.Name.FatherName);
        Assert.Equal(command.MotherName, profile.Name.MotherName);
        Assert.Equal(command.DateOfBirth, profile.Birth.BirthDate);
        Assert.Equal(command.DocumentNumber, profile.DocumentN.NumberDocument);
        Assert.Equal(command.Phone, profile.PhoneN.PhoneNumber);
        Assert.Equal(command.PhotoUrl, profile.PhotoUrl);
    }

    [Fact]
    public void Profile_Update_ShouldUpdateProperties()
    {
        var command = new CreateProfileCommand("Jane", "Johnson", "Doe", "1995-02-15", "87654321", "987654321",1, "photoUrl");
        var profile = new Profile(command);
        var updateCommand = new UpdateProfileCommand(1, "Jane", "Johnson", "Doe", "1995-02-15", "87654321", "987654321", 1,"photoUrl");
        
        
        profile.Update(updateCommand);
        
        Assert.Equal(updateCommand.Name, profile.Name.Name);
        Assert.Equal(updateCommand.FatherName, profile.Name.FatherName);
        Assert.Equal(updateCommand.MotherName, profile.Name.MotherName);
        Assert.Equal(updateCommand.DateOfBirth, profile.Birth.BirthDate);
        Assert.Equal(updateCommand.DocumentNumber, profile.DocumentN.NumberDocument);
        Assert.Equal(updateCommand.Phone, profile.PhoneN.PhoneNumber);
    }

    [Fact]
    public void ProfileQueryServiceMustToWork()
    {
        var query = new GetAllProfilesQuery();
        var queryService = new Mock<IProfileQueryService>();
        
        //SetUp
        queryService.Setup(x => x.Handle(query));
        //Assert
        
        queryService.Verify(x => x.Handle(query), Times.Once);
    }

}