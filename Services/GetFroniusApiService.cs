using FroniusIntegration.Entities;
using FroniusIntegration.Interfaces.Services;
using FroniusIntegration.Utils;
using Newtonsoft.Json;
using RestSharp;

namespace FroniusIntegration.Services;

public class GetFroniusApiService : IGetFroniusApiService
{
  private readonly FroniusRestClientDynamic _froniusRestClientDynamic;

  public GetFroniusApiService(FroniusRestClientDynamic froniusRestClientDynamic)
  {
    _froniusRestClientDynamic = froniusRestClientDynamic;
  }

  public async Task<PowerFronius> GetPowerFroniusAsync(string inversorAddress)
  {
    _froniusRestClientDynamic.UpdateRestClient(inversorAddress);
    var froniusRestClient = new FroniusRestClient(_froniusRestClientDynamic);

    var request = RequestBuilder.Build(Method.Get, "/solar_api/v1/GetInverterRealtimeData.cgi?Scope=System");
    var response = await froniusRestClient.ExecuteAsync(request);
    var powerFronius = JsonConvert.DeserializeObject<PowerFronius>(response.Content);
    return powerFronius;
  }
}