using System.Text;
using Dapper;
using FroniusIntegration.Entities;
using FroniusIntegration.Interfaces.Repositories;

namespace FroniusIntegration.Repositories;

public class LocationRepository : ILocationRepository
{
  private readonly AppDataContext _context;

  public LocationRepository(AppDataContext context)
  {
    _context = context;
  }

  public async Task<Location?> GetLocationAsync(int locationId)
  {
    var sqlCommand = new StringBuilder();

    sqlCommand.Append("SELECT");
    sqlCommand.Append($" name AS {nameof(Location.LocationName)} ");
    sqlCommand.Append($", inverter_address AS {nameof(Location.InversorAddress)} ");
    sqlCommand.Append($", inverter_port AS {nameof(Location.InversorPort)} ");
    sqlCommand.Append(" FROM locations");
    sqlCommand.Append(" WHERE id = @locationId");

    return await _context.Connection.QueryFirstOrDefaultAsync<Location?>(sqlCommand.ToString(), new{locationId});
  }
}