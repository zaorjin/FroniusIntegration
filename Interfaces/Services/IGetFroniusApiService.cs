using FroniusIntegration.Entities;

namespace FroniusIntegration.Interfaces.Services;

public interface IGetFroniusApiService
{
  Task<PowerFronius> GetPowerFroniusAsync();
}