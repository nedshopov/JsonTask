using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace JsonTask.Shared
{
   /// <summary>
   /// A common value object and unit's type.
   /// </summary>
   public class ValueObject
   {
      public int Value { get; set; }

      [JsonConverter(typeof(StringEnumConverter))]
      public UnitType Unit { get; set; }
   }
}
