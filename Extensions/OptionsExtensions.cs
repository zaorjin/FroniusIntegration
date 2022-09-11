using FroniusIntegration.Shared;

namespace FroniusIntegration.Extensions;

public static class OptionsExtensions
{
  public static void AddOptionsConfiguration(this IServiceCollection services, IConfiguration configuration)
  {
    services.Configure<ConnectionStringsOption>(configuration.GetSection(ConnectionStringsOption.ConnectionStrings));
    services.Configure<GetFroniusOption>(
      configuration.GetSection(GetFroniusOption.FroniusApi));
  }
}
