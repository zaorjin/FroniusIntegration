using Newtonsoft.Json;

namespace FroniusIntegration.Entities;

public class PowerFronius
{
  public Body Body {get; set;}
}

public class Body
{
  public Data Data { get; set; }
}

public class Data
{
  public TOTAL_ENERGY TOTAL_ENERGY { get; set; }
}

public class TOTAL_ENERGY
{
  public Values Values { get; set; }
}

public class Values
{
  [JsonProperty("1")]
  public int totalEnergy { get; set; }
}

