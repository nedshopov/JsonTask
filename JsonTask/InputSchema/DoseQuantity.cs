using JsonTask.Shared;

namespace JsonTask.Entities
{
   public class DoseQuantity : ValueObject
   {
      public FrequencyType FrequencyType { get; set; }

      public string SiType { get; set; }

      public string Name { get; set; }
   }
}