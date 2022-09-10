using FroniusIntegration.Command;
using FroniusIntegration.Entities;
using FroniusIntegration.Handlers;
using Hangfire;

namespace FroniusIntegration;

public class ControllerService
{
  private readonly PowerEnergyHandler _powerEnergyHandler;

  public ControllerService(PowerEnergyHandler powerEnergyHandler)
  {
    _powerEnergyHandler = powerEnergyHandler;
  }

  [DisableConcurrentExecution(0)]
  [AutomaticRetry(Attempts = 0)]
  public async Task ProcessEventsAsync()
  {
    var generation = new Generation(1, 5, DateTime.Now, DateTime.Now);
    BackgroundJob.Enqueue(() => _powerEnergyHandler.HandleAsync(new IntegrateGenerationCommand(generation)));
  }
}