using System.Net;
using RestSharp;
using RestSharp.Authenticators;

namespace FroniusIntegration.Utils;

public interface IRestClientDynamic
{
  public RestClient Client { get; set; }
}

public abstract class ThrottledRestClientDynamic
{
  private const int TOO_MANY_REQUESTS = 429;
  private const int GATEWAY_TIME_OUT_ERROR = 504;
  private const int ONE_MINUTE_IN_MILLISECONDS = 6000;
  private readonly RestClient _restClient;

  protected ThrottledRestClientDynamic(IRestClientDynamic restClientDynamic)
  {
    _restClient = restClientDynamic.Client;
  }

  public virtual async Task<RestResponse> ExecuteAsync(RestRequest request)
  {
    RestResponse response;

    var wait = 0;

    do
    {
      if (wait > 0)
        await Task.Delay(wait);

      response = await _restClient.ExecuteAsync(request);

      wait = 0;

      if (response.StatusCode.Equals((HttpStatusCode)TOO_MANY_REQUESTS))
        wait = Convert.ToInt32(response?.Headers?.FirstOrDefault(t => t.Name.Equals("Retry-After"))?.Value) * 1000;
      else if (response.StatusCode.Equals((HttpStatusCode)GATEWAY_TIME_OUT_ERROR)) wait = ONE_MINUTE_IN_MILLISECONDS;
    } while (wait > 0);

    return response!;
  }

  public virtual async Task<RestResponse<T>> ExecuteAsync<T>(RestRequest request)
  {
    RestResponse<T> response;

    var wait = 0;

    do
    {
      if (wait > 0)
        await Task.Delay(wait);

      response = await _restClient.ExecuteAsync<T>(request);

      wait = 0;

      if (response.StatusCode.Equals((HttpStatusCode)TOO_MANY_REQUESTS))
        wait = Convert.ToInt32(response?.Headers?.FirstOrDefault(t => t.Name.Equals("Retry-After"))?.Value) * 1000;
      else if (response.StatusCode.Equals((HttpStatusCode)GATEWAY_TIME_OUT_ERROR)) wait = ONE_MINUTE_IN_MILLISECONDS;
    } while (wait > 0);

    return response!;
  }

  public void SetJwtToken(string token)
  {
    var authenticator = new JwtAuthenticator(token);
    _restClient.Authenticator = authenticator;
  }
}