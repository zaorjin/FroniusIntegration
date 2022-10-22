using FroniusIntegration.Command;
using FroniusIntegration.Entities;
using FroniusIntegration.Interfaces.Handlers;
using FroniusIntegration.Interfaces.Repositories;

namespace FroniusIntegration.Handlers;

public class PowerEnergyHandler : ICommandHandler<IntegrateGenerationCommand>
{
  private readonly IGenerationRepository _generationRepository;

  public async Task HandleAsync(IntegrateGenerationCommand command)
  {
    var generation = new Generation(
      1,
      command.PowerFronius.Body.Data.TOTAL_ENERGY.Values.totalEnergy,
      DateTime.Now,
      DateTime.Now
    );

    await _generationRepository.InsertGenerationAsync(generation);
  }
}