using JsonTask.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace JsonTask.OutputEntities
{
   public class OutputQuantityValue
   {
      public double Value { get; set; }

      [JsonConverter(typeof(StringEnumConverter))]
      public UnitType Unit { get; set; }
   }
}
