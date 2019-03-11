using System.Collections.Generic;

namespace JsonTask.OutputEntities
{
   public class DrugOutput
   {
      public string Name { get; set; }

      public IList<SuggestedDose> SuggestedDose { get; set; }
   }
}