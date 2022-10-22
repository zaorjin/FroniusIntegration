using FroniusIntegration.Entities;
using FroniusIntegration.Interfaces.Commands;

namespace FroniusIntegration.Command;

public class IntegrateGenerationCommand : ICommand
{
  public IntegrateGenerationCommand(PowerFronius powerFronius)
  {
    PowerFronius = powerFronius;
  }

  public PowerFronius PowerFronius { get; set; }
}