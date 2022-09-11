using FroniusIntegration.Command;
using FroniusIntegration.Entities;
using FroniusIntegration.Handlers;
using FroniusIntegration.Services;
using Hangfire;

namespace FroniusIntegration;

public class ControllerService
{
  private readonly GetFroniusApi _getFroniusApi;
  private readonly PowerEnergyHandler _powerEnergyHandler;

  public ControllerService(PowerEnergyHandler powerEnergyHandler, GetFroniusApi getFroniusApi)
  {
    _powerEnergyHandler = powerEnergyHandler;
    _getFroniusApi = getFroniusApi;
  }

  [DisableConcurrentExecution(0)]
  [AutomaticRetry(Attempts = 0)]
  public async Task ProcessEventsAsync()
  {
    var result = await _getFroniusApi.GetConcretingAsync();
    var generation = new Generation(1, 5, DateTime.Now, DateTime.Now);
    BackgroundJob.Enqueue(() => _powerEnergyHandler.HandleAsync(new IntegrateGenerationCommand(generation)));
  }
}