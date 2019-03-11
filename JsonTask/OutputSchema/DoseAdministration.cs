using System.Collections.Generic;

namespace JsonTask.OutputEntities
{
   public class DoseAdministration
   {
      public string Route { get; set; }
      public string Method { get; set; }
      public IList<DoseOutput> Doses { get; set; } = new List<DoseOutput>();
   }
}