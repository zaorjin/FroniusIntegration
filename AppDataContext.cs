using FroniusIntegration.Shared;
using Microsoft.Extensions.Options;
using Npgsql;

namespace FroniusIntegration;

public class AppDataContext
{
  public AppDataContext(IOptions<ConnectionStringsOption> connectionStringsOption)
  {
    var connectionStringsTopsysOption = connectionStringsOption.Value;
    Connection = new NpgsqlConnection(connectionStringsTopsysOption.Fronius);
  }

  public NpgsqlConnection Connection { get; set; }
}