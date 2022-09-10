using FroniusIntegration.Shared;
using FroniusIntegration.Utils;
using Microsoft.Extensions.Options;
using RestSharp;

namespace FroniusIntegration;

public class FroniusRestClientDynamic : IRestClientDynamic
{
  public FroniusRestClientDynamic(IOptions<GetFroniusOption> getFroniusOption)
  {
    var getFroniusHostOption = getFroniusOption.Value;
    Client = new RestClient(new Uri(getFroniusHostOption.Host));
  }

  public RestClient Client { get; set; }
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

  /*public static ICommandResult<T?> ReturnCommandHandleCustomerErrorResponse<T>(RestResponse? response, string keyEntity)
  {
    if (response?.Content == "")
    {
      return new CommandResult<T?>(
        false,
        $"ERRORS ON ID {keyEntity}: {response?.ErrorException?.Message ?? string.Empty}",
        default);
    }

    var error = JsonSerializer.Deserialize<FroniusErrorResponse>(response?.Content ?? "");

    var errorsString = string.Join(
      Environment.NewLine,
      error?.Notifications?.Select(t => t.ToString()) ?? Array.Empty<string>());

    var errorMessage = $" ERRORS ON ID {keyEntity}: {(errorsString != "" ? errorsString : response?.Content)}";

    return new CommandResult<T?>(false, errorMessage, default);
  }*/
}