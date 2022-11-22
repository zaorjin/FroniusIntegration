namespace FroniusIntegration.Entities;

public class Generation
{
  public Generation(int? locationId, float? volumeKwh, DateTime createAt, DateTime updateAt)
  {
    LocationId = locationId;
    VolumeKwh = volumeKwh;
    CreateAt = createAt;
    UpdateAt = updateAt;
  }

  public int? LocationId { get; set; }
  public float? VolumeKwh { get; set; }
  public DateTime CreateAt { get; set; }
  public DateTime UpdateAt { get; set; }
}