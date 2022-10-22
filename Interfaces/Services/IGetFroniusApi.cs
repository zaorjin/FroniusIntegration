using FroniusIntegration.Entities;

namespace FroniusIntegration.Interfaces.Services;

public interface IGetFroniusApi
{
  Task<int> GetConcretingAsync();
}