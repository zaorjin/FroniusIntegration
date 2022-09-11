using FroniusIntegration.Interfaces.Services;
using FroniusIntegration.Services;

namespace FroniusIntegration.Extensions;

public static class ServicesExtensions
{
  public static void AddCustomServices(this IServiceCollection services)
  {
    services.AddTransient<IGetFroniusApi, GetFroniusApi>();
  }
}