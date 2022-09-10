using FroniusIntegration.Interfaces.Commands;

namespace FroniusIntegration.Interfaces.Handlers;

public interface ICommandHandler<in TCommand, TResultData>
  where TCommand : ICommand
{
  Task<ICommandResult<TResultData>> HandleAsync(TCommand command);
}

public interface ICommandHandler<in TCommand>
  where TCommand : ICommand
{
  Task HandleAsync(TCommand command);
}