using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace JsonTask.Entities
{
   public class DoseInstruction
   {
      [JsonConverter(typeof(StringEnumConverter))]
      public DoseType DoseType { get; set; }

      public bool Emergency { get; set; }

      public DoseQuantity DoseQuantity { get; set; }

      public DoseQuantity Frequency { get; set; }
   }
}