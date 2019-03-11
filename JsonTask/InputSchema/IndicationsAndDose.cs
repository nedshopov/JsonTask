using System.Collections.Generic;

namespace JsonTask.Entities
{

   public class IndicationsAndDose
   {
      public string PotName { get; set; }

      public IList<IndicationAndDoseGroups> IndicationAndDoseGroups { get; set; }
   }
}