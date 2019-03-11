using System.Collections.Generic;

namespace JsonTask.OutputEntities
{
   public class Result
   {
      public IList<DrugOutput> Drugs { get; set; } = new List<DrugOutput>();
   }
}
