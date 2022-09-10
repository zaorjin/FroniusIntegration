using RestSharp;

namespace FroniusIntegration.Utils;

public static class RequestBuilder
{
  public static RestRequest Build(Method method, string resource)
  {
    var request = new RestRequest
    {
      RequestFormat = DataFormat.Json,
      Method = method,
      Resource = resource
    };

    return request;
  }
}