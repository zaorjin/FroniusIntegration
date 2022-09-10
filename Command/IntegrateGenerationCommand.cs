using FroniusIntegration.Entities;
using FroniusIntegration.Interfaces.Commands;

namespace FroniusIntegration.Command;

public class IntegrateGenerationCommand : ICommand
{
  public IntegrateGenerationCommand(Generation gereation)
  {
    Generation = gereation;
  }

  public Generation Generation { get; set; }
}