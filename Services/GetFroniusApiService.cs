using FroniusIntegration.Entities;
using FroniusIntegration.Interfaces.Services;
using FroniusIntegration.Utils;
using Newtonsoft.Json;
using RestSharp;

namespace FroniusIntegration.Services;

public class GetFroniusApiService : IGetFroniusApiService
{
  private readonly FroniusRestClient _froniusRestClient;

  public GetFroniusApiService(FroniusRestClient froniusRestClient)
  {
    _froniusRestClient = froniusRestClient;
  }

  public async Task<PowerFronius> GetPowerFroniusAsync()
  {
    var request = RequestBuilder.Build(Method.Get, "/solar_api/v1/GetInverterRealtimeData.cgi?Scope=System");
    var response = await _froniusRestClient.ExecuteAsync(request);
    var powerFronius = JsonConvert.DeserializeObject<PowerFronius>(response.Content);
    return powerFronius;
  }
}