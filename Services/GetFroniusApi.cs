using FroniusIntegration.Entities;
using FroniusIntegration.Interfaces.Services;
using FroniusIntegration.Utils;
using RestSharp;

namespace FroniusIntegration.Services;

public class GetFroniusApi : IGetFroniusAPi
{
  private readonly FroniusRestClient _froniusRestClient;

  public GetFroniusApi(FroniusRestClient froniusRestClient)
  {
    _froniusRestClient = froniusRestClient;
  }

  public async Task<Generation> GetConcretingAsync()
  {
    var request = RequestBuilder.Build(Method.Get, "/solar_api/v1/GetInverterRealtimeData.cgi?Scope=System");
    var response = await _froniusRestClient.ExecuteAsync<StandardResponse<Generation>>(request);
    return response.Data.Result;
  }
}