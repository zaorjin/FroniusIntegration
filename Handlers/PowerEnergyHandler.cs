using FroniusIntegration.Command;
using FroniusIntegration.Interfaces.Handlers;

namespace FroniusIntegration.Handlers;

public class PowerEnergyHandler : ICommandHandler<IntegrateGenerationCommand>
{
  public Task HandleAsync(IntegrateGenerationCommand command)
  {
    throw new NotImplementedException();
  }
}