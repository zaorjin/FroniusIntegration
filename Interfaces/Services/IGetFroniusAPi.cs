using FroniusIntegration.Entities;

namespace FroniusIntegration.Interfaces.Services;

public interface IGetFroniusAPi
{
  Task<Generation> GetConcretingAsync();
}