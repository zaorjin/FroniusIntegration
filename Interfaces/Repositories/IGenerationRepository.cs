using FroniusIntegration.Entities;

namespace FroniusIntegration.Interfaces.Repositories;

public interface IGenerationRepository
{
  Task InsertGenerationAsync(Generation generation);
}