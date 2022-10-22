using FroniusIntegration.Command;
using FroniusIntegration.Handlers;
using FroniusIntegration.Interfaces.Services;
using Hangfire;

namespace FroniusIntegration;

public class ControllerService
{
  private readonly IGetFroniusApiService _getFroniusApiService;
  private readonly PowerEnergyHandler _powerEnergyHandler;

  public ControllerService(PowerEnergyHandler powerEnergyHandler, IGetFroniusApiService getFroniusApiService)
  {
    _powerEnergyHandler = powerEnergyHandler;
    _getFroniusApiService = getFroniusApiService;
  }

  [DisableConcurrentExecution(0)]
  [AutomaticRetry(Attempts = 0)]
  public async Task ProcessEventsAsync()
  {
    var powerFronius = await _getFroniusApiService.GetPowerFroniusAsync();
    BackgroundJob.Enqueue(() => _powerEnergyHandler.HandleAsync(new IntegrateGenerationCommand(powerFronius)));
  }
}