using FroniusIntegration.Entities;

namespace FroniusIntegration.Interfaces.Repositories;

public interface ILocationRepository
{
  Task<Location?> GetLocationAsync(int locationId);
}

