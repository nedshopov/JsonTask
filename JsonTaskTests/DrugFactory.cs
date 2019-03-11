using JsonTask.Entities;
using System.Collections.Generic;

namespace JsonTaskTests
{
   /// <summary>
   /// Static factory class for producig drug test objects.
   /// </summary>
   public static class DrugFactory
   {
      /// <summary>
      /// Create new test drug.
      /// </summary>
      /// <param name="testName">Name of the drug</param>
      /// <param name="testIndication">Indication for use.</param>
      /// <param name="testRoute">Route of using.</param>
      /// <returns>A new drug test object.</returns>
      public static Drug GetDrug(string testName, string testIndication, string testRoute)

      {
         IList<DoseInstruction> doseInstructions = new List<DoseInstruction>
         {
            new DoseInstruction
            {
               DoseQuantity = new DoseQuantity {Value = 2, Unit = JsonTask.Shared.UnitType.Grams },
               Frequency = new DoseQuantity { FrequencyType = FrequencyType.None },
               DoseType = DoseType.Standard,
               Emergency = true
            }
         };
         RouteOfAdministration routeOfAdministration = new RouteOfAdministration { Route = "By " + testRoute};
         RoutesAndPatientGroups routesAndPatientGroup = new RoutesAndPatientGroups
         {
            RoutesOfAdministration = new List<RouteOfAdministration> { routeOfAdministration },
            PatientGroups = new List<PatientGroup>() {
               new PatientGroup
               {
                  Age = new Age {From = 2, FromUnit = JsonTask.Shared.UnitType.Years },
                  DoseStatement = new DoseStatement { DoseInstruction = doseInstructions }
               }
            }
         };
         IList<RoutesAndPatientGroups> routesAndPatientGroups = new List<RoutesAndPatientGroups> { routesAndPatientGroup };
         IList<TherapeuticIndications> therapeuticIndications = new List<TherapeuticIndications>
         { new TherapeuticIndications {Indication = testIndication } };
         IList<IndicationAndDoseGroups> indicationAndDoseGroups = new List<IndicationAndDoseGroups>
         {
            new IndicationAndDoseGroups { RoutesAndPatientGroups = routesAndPatientGroups, TherapeuticIndications = therapeuticIndications}
         };
         Drug drug = new Drug { Name = testName, IndicationsDose = new IndicationsAndDose { IndicationAndDoseGroups = indicationAndDoseGroups } };
         return drug;
      }
   }
}
