namespace LocalManagement.Application.Internal.OutboundServices;

public interface IUserCommentExternalService
{
    Task<bool> UserExists(int userId);
}