using AlquilaFacilPlatform.IAM.Infrastructure.Pipeline.Middleware.Components;

namespace AlquilaFacilPlatform.IAM.Infrastructure.Pipeline.Middleware.Extensions;

public static class RequestAuthorizationMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestAuthorization(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestAuthorizationMiddleware>();
    }
}