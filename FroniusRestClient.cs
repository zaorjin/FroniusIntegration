using FroniusIntegration.Entities;
using FroniusIntegration.Interfaces.Repositories;
using FroniusIntegration.Shared;
using FroniusIntegration.Utils;
using Microsoft.Extensions.Options;
using RestSharp;

namespace FroniusIntegration;

public class FroniusRestClientDynamic : IRestClientDynamic
{
  public void UpdateRestClient(string inversorAddress)
  {
    Client = new RestClient(new Uri($"http://{inversorAddress}"));
  }

  public RestClient? Client { get; set; }
}

public class FroniusRestClient : ThrottledRestClientDynamic
{
  public FroniusRestClient(FroniusRestClientDynamic restClientDynamic)
    : base(restClientDynamic)
  {
  }

  public override async Task<RestResponse> ExecuteAsync(RestRequest request)
  {
    var response = await base.ExecuteAsync(request);

    return response;
  }
}