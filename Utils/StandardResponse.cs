namespace FroniusIntegration.Utils;

public class StandardResponse<T>
{
  public int Status { get; set; } = 0;

  public string Message { get; set; } = string.Empty;

  public T? Body { get; set; }

  public string ErrorCode { get; set; } = string.Empty;

  public string Notification { get; set; } = string.Empty;
}