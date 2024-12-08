namespace Pawnshop.Api.Extensions;

public static class AddCorsExtension
{
    public static IServiceCollection CorsExtension(this IServiceCollection services)
    {
        services.AddCors(conf =>
        {
            conf.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        return services;
    }
}