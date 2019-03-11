using System.Collections.Generic;

namespace JsonTask.Entities
{
   public class RoutesAndPatientGroups
   {
      public IList<RouteOfAdministration> RoutesOfAdministration { get; set; }

      public IList<PatientGroup> PatientGroups { get; set; }
   }
}