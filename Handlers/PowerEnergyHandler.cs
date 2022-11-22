using FroniusIntegration.Command;
using FroniusIntegration.Entities;
using FroniusIntegration.Extensions;
using FroniusIntegration.Interfaces.Handlers;
using FroniusIntegration.Interfaces.Repositories;
using FroniusIntegration.Interfaces.Services;

namespace FroniusIntegration.Handlers;

public class PowerEnergyHandler : ICommandHandler<IntegrateGenerationCommand>
{
  private readonly IGenerationRepository _generationRepository;
  private readonly IGetFroniusApiService _getFroniusApiService;
  private readonly ILocationRepository _locationRepository;

  public PowerEnergyHandler(
    IGenerationRepository generationRepository, 
    IGetFroniusApiService getFroniusApiService,
    ILocationRepository locationRepository)
  {
    _locationRepository = locationRepository;
    _generationRepository = generationRepository;
    _getFroniusApiService = getFroniusApiService;
  }

  public async Task HandleAsync(IntegrateGenerationCommand command)
  {
    var location = await _locationRepository.GetLocationAsync(1);

    var timeDiferance = (int)DateTime.Now.Subtract(DefaultEnergyValue.lastDefaultValueDate).TotalMinutes;
    if(DefaultEnergyValue.Energy == 0){
      var powerFronius = await _getFroniusApiService.GetPowerFroniusAsync(location.InversorAddress);
      DefaultEnergyValue.Energy = powerFronius.Body.Data.TOTAL_ENERGY.Values.totalEnergy;
      DefaultEnergyValue.lastDefaultValueDate = DateTime.Now;
      return;
    }
    if(timeDiferance > 62 && timeDiferance < 58){
      var powerFronius = await _getFroniusApiService.GetPowerFroniusAsync(location.InversorAddress);
      DefaultEnergyValue.Energy = powerFronius.Body.Data.TOTAL_ENERGY.Values.totalEnergy;
      DefaultEnergyValue.lastDefaultValueDate = DateTime.Now;
      return;
    }
    var energyHour = (float)command.PowerFronius.Body.Data.TOTAL_ENERGY.Values.totalEnergy - DefaultEnergyValue.Energy;
    var generation = new Generation(
      4,
      (energyHour/1000),
      DateTime.Now,
      DateTime.Now
    );

    DefaultEnergyValue.Energy = command.PowerFronius.Body.Data.TOTAL_ENERGY.Values.totalEnergy;
    DefaultEnergyValue.lastDefaultValueDate = DateTime.Now;
    await _generationRepository.InsertGenerationAsync(generation);
  }
}