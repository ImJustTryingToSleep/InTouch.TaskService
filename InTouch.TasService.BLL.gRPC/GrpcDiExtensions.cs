using Microsoft.Extensions.DependencyInjection;
using UserServiceClientGrpcApp;

namespace InTouch.TasService.BLL.gRPC;

public static class GrpcDiExtensions
{
    public static IServiceCollection ConfigureGrpc(this IServiceCollection services)
    {
        services.AddGrpcClient<UserServiceGrpc.UserServiceGrpcClient>(o =>
        {
            o.Address = new Uri("https://localhost:7100");
        });
        return services;
    }
}