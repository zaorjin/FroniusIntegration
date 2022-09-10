using FroniusIntegration.Interfaces.Commands;

namespace FroniusIntegration.Command;

public class CommandResult<T> : ICommandResult<T>
{
  public CommandResult(bool success, string message, T data)
  {
    Success = success;
    Message = message;
    Data = data;
  }

  public bool Success { get; set; }
  public string Message { get; set; }
  public T Data { get; set; }
}