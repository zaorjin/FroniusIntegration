using FroniusIntegration.Interfaces.Repositories;
using FroniusIntegration.Repositories;

namespace FroniusIntegration.Extensions;
public static class RepositoriesExtensions
{
  public static void AddCustomRepositories(this IServiceCollection services)
  {
    services.AddTransient<IGenerationRepository, GenerationRepository>();
    services.AddTransient<ILocationRepository, LocationRepository>();
  }
}