using JsonTask.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace JsonTask.Entities
{
   public class Age
   {
      public int From { get; set; }

      [JsonConverter(typeof(StringEnumConverter))]
      public UnitType FromUnit { get; set; }

      public int To { get; set; }

      [JsonConverter(typeof(StringEnumConverter))]
      public UnitType ToUnit { get; set; }

      public string LevelOfSupervision { get; set; }
   }
}