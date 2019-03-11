using System.Collections.Generic;

namespace JsonTask.Entities
{
   public class DoseStatement
   {
      public IList<DoseInstruction> DoseInstruction { get; set; }
   }
}