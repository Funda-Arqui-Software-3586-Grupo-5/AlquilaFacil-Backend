using AlquilaFacilPlatform.IAM.Domain.Model.Aggregates;

namespace AlquilaFacilPlatform.Tests.UnitTests;

public class UserTests
{
    [Fact]
    public void UpdateUsername_ShouldChangeUsername()
    {
        string initialUsername = "initialUsername";
        string newUsername = "newUsername";
        string email = "email@gmail.com";
        var user = new User(initialUsername, "passwordHash", email);

        user.UpdateUsername(newUsername);

        Assert.Equal(newUsername, user.Username);
    }
    
    [Fact]
    public void UpdatePasswordHash_ShouldChangePasswordHash()
    {
        string username = "username";
        string initialPasswordHash = "initialHash";
        string newPasswordHash = "newHash";
        string email = "email@gmail.com";
        var user = new User(username, initialPasswordHash, email);

        user.UpdatePasswordHash(newPasswordHash);

        Assert.Equal(newPasswordHash, user.PasswordHash);
    }
}