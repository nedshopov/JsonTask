using Newtonsoft.Json;

namespace JsonTask.Entities
{
   public class RouteOfAdministration
   {
      [JsonProperty(PropertyName = "RouteOfAdministration")]
      public string Route { get; set; }

      public string RouteForm { get; set; }
   }
}