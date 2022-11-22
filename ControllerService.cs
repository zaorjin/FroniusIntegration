using FroniusIntegration.Command;
using FroniusIntegration.Handlers;
using FroniusIntegration.Interfaces.Repositories;
using FroniusIntegration.Interfaces.Services;
using Hangfire;

namespace FroniusIntegration;

public class ControllerService
{
  private readonly IGetFroniusApiService _getFroniusApiService;
  private readonly PowerEnergyHandler _powerEnergyHandler;
  private readonly ILocationRepository _locationRepository;

  public ControllerService(
    PowerEnergyHandler powerEnergyHandler, 
    IGetFroniusApiService getFroniusApiService,
    ILocationRepository locationRepository)
  {
    _powerEnergyHandler = powerEnergyHandler;
    _getFroniusApiService = getFroniusApiService;
    _locationRepository = locationRepository;
  }

  [DisableConcurrentExecution(0)]
  [AutomaticRetry(Attempts = 0)]
  public async Task EnergyHoursJobAsync()
  {
    var location = await _locationRepository.GetLocationAsync(1);
    var powerFronius = await _getFroniusApiService.GetPowerFroniusAsync(location.InversorAddress);
    BackgroundJob.Enqueue(() => _powerEnergyHandler.HandleAsync(new IntegrateGenerationCommand(powerFronius)));
  }
}