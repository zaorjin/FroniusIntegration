using System.Text;
using Dapper;
using FroniusIntegration.Entities;
using FroniusIntegration.Interfaces.Repositories;

namespace FroniusIntegration.Repositories;

public class GenerationRepository : IGenerationRepository
{
  private readonly AppDataContext _context;

  public GenerationRepository(AppDataContext context)
  {
    _context = context;
  }

  public async Task InsertGenerationAsync(Generation generation)
  {
    var sqlCommand = new StringBuilder();

    sqlCommand.Append("INSERT INTO generation_hours");
    sqlCommand.Append(" ( location_id, volume_khw, created_at, updated_at )");
    sqlCommand.Append(" VALUES");
    sqlCommand.Append($" (@{nameof(Generation.LocationId)}");
    sqlCommand.Append($", @{nameof(Generation.VolumeKwh)} ");
    sqlCommand.Append($", @{nameof(Generation.CreateAt)} ");
    sqlCommand.Append($", @{nameof(Generation.UpdateAt)} )");

    await _context.Connection.ExecuteAsync(sqlCommand.ToString(), generation);
  }
}