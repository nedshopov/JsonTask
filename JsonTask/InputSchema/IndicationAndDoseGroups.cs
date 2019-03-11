using System.Collections.Generic;

namespace JsonTask.Entities
{
   public class IndicationAndDoseGroups
   {
     public IList<TherapeuticIndications> TherapeuticIndications { get; set; }

     public IList<RoutesAndPatientGroups> RoutesAndPatientGroups { get; set; }
   }
}