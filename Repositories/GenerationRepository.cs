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

    sqlCommand.Append("INSERT INTO generations");
    sqlCommand.Append(" ( location_id, volume_khw )");
    sqlCommand.Append(" VALUES");
    sqlCommand.Append(" ( 'PUCMINAS'");
    sqlCommand.Append($" @{nameof(Generation.VolumeKwh)} )");

    await _context.Connection.ExecuteAsync(sqlCommand.ToString(), generation);
  }
}