using System.IdentityModel.Tokens.Jwt;
internal static class CommonExtensions
{
  public static IServiceCollection AddApplicationServices(this IServiceCollection services)
  {
    // TODO: Add all services that will be used

    return services;
  }

  public static IServiceCollection AddDefaultAuthentication(this IServiceCollection services, IConfiguration configuration)
  {
    var identitySection = configuration.GetSection("Identity");

    if (!identitySection.Exists())
    {
      // No identity section, so no authentication
      return services;
    }

    JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

    services.AddAuthentication().AddJwtBearer(options =>
    {
      var identityUrl = identitySection.GetRequiredValue("Url");
      var audience = identitySection.GetRequiredValue("Audience");

      options.Authority = identityUrl;
      options.RequireHttpsMetadata = false;
      options.Audience = audience;
      options.TokenValidationParameters.ValidateAudience = false;
    });

    return services;
  }

  
}