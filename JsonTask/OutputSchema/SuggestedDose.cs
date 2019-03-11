using System.Collections.Generic;

namespace JsonTask.OutputEntities
{
   public class SuggestedDose
   {
      public IList<string> Indications { get; set; }

      public IList<DoseAdministration> DoseAdministrations { get; set; }
   }
}