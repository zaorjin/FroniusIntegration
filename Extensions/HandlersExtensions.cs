using FroniusIntegration.Handlers;

namespace FroniusIntegration.Extensions;

public static class HandlersExtensions
{
  public static void AddCustomHandlers(this IServiceCollection services)
  {
    services.AddTransient<PowerEnergyHandler, PowerEnergyHandler>();
  }
}