using System.Text.Json.Serialization;

namespace FroniusIntegration.Shared;

public class FroniusErrorResponse
{
  [JsonPropertyName("notifications")] public IEnumerable<ErrorNotification>? Notifications { get; set; }
}

public class ErrorNotification
{
  [JsonPropertyName("propertyName")] public string PropertyName { get; set; } = string.Empty;

  [JsonPropertyName("attemptedValue")] public string AttemptedValue { get; set; } = string.Empty;

  [JsonPropertyName("errorMessage")] public string ErrorMessage { get; set; } = string.Empty;

  [JsonPropertyName("errorCode")] public string ErrorCode { get; set; } = string.Empty;

  public override string ToString()
  {
    return
      $"{nameof(PropertyName)}: {PropertyName}, {nameof(AttemptedValue)}: {AttemptedValue}, {nameof(ErrorMessage)}: {ErrorMessage}";
  }
}