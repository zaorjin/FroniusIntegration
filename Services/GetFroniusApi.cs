using FroniusIntegration.Entities;
using FroniusIntegration.Interfaces.Services;
using FroniusIntegration.Utils;
using RestSharp;
using Newtonsoft.Json;

namespace FroniusIntegration.Services;

public class GetFroniusApi : IGetFroniusApi
{
  private readonly FroniusRestClient _froniusRestClient;

  public GetFroniusApi(FroniusRestClient froniusRestClient)
  {
    _froniusRestClient = froniusRestClient;
  }

  public async Task<int> GetConcretingAsync()
  {
    var request = RequestBuilder.Build(Method.Get, "/solar_api/v1/GetInverterRealtimeData.cgi?Scope=System");
    var response = await _froniusRestClient.ExecuteAsync(request);
    var company = JsonConvert.DeserializeObject<PowerFronius>(response.Content);
    return company.Body.Data.TOTAL_ENERGY.Values.totalEnergy;
  }
}