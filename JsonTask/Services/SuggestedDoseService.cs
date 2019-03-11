using JsonTask.Entities;
using JsonTask.OutputEntities;
using JsonTask.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace JsonTask.Services
{
   /// <summary>
   /// Additional service for egenerating the suggested doses.
   /// </summary>
   public static class SuggestedDoseService
   {
      /// <summary>
      /// Get suggested doses for a drug.
      /// </summary>
      /// <param name="drug">Drug model with data from the json input.</param>
      /// <returns>List of suggested doses.</returns>
      public static IList<SuggestedDose> GetSuggestedDoses(Drug drug)
      {
         IList<SuggestedDose> suggestedDoses = new List<SuggestedDose>();
         SuggestedDose suggestedDose = new SuggestedDose();
         foreach(IndicationAndDoseGroups indicationGroup in drug.IndicationsDose.IndicationAndDoseGroups)
         {
            suggestedDoses.Add(ProcessIndicationAndDoseGroup(indicationGroup));
         }
         return suggestedDoses;
      }

      /// <summary>
      /// Method for getting the enum member value attribute.
      /// </summary>
      /// <param name="value">Value object.</param>
      /// <returns>The enum member value.</returns>
      public static string GetEnumMemberValue(object value)
      {
         MemberInfo[] memInfo = typeof(UnitType).GetMember(value.ToString());
         EnumMemberAttribute attr = memInfo[0].GetCustomAttributes(false).OfType<EnumMemberAttribute>().FirstOrDefault();
         if(attr != null)
         {
            return attr.Value;
         }

         return null;
      }

      private static SuggestedDose ProcessIndicationAndDoseGroup(IndicationAndDoseGroups indicationGroup)
      {
         SuggestedDose dose = new SuggestedDose();
         dose.Indications = indicationGroup.TherapeuticIndications.Select(x => x.Indication).ToList();
         string[] routes = indicationGroup.RoutesAndPatientGroups.Select(x => x.RoutesOfAdministration.Select(y => y.Route).FirstOrDefault()).FirstOrDefault().Split(" ");
         string route = CapitalizeWord(routes[1]);
         string method = (routes.Length > 2) ? CapitalizeWord(routes[2]) : string.Empty;
         DoseAdministration doseAdministration = new DoseAdministration { Route = CapitalizeWord(routes[1]), Method = method };
         RoutesAndPatientGroups routesAndPatientGroups = indicationGroup.RoutesAndPatientGroups.FirstOrDefault();
         if( routesAndPatientGroups != null)
         {
            foreach(PatientGroup patientGroup in routesAndPatientGroups.PatientGroups)
            {
               DoseOutput output = new DoseOutput();
               output.AgeBand = new AgeBand
               {
                  AgeLow = new ValueObject
                  {
                     Value = patientGroup.Age.From,
                     Unit = CheckYearFormat(patientGroup.Age.FromUnit)
                  },
                  AgeHigh = new ValueObject
                  {
                     Value = patientGroup.Age.To,
                     Unit = CheckYearFormat(patientGroup.Age.ToUnit)
                  }
               };
               output.Flags = new Flag { Frequency = CalculateFrequency(patientGroup.DoseStatement.DoseInstruction.FirstOrDefault()) };
               output.Quantity = CalculateQuantity(patientGroup.DoseStatement.DoseInstruction.FirstOrDefault());
               doseAdministration.Doses.Add(output);
            }
         }
         dose.DoseAdministrations = new List<DoseAdministration> { doseAdministration };
         return dose;
      }

      private static OutputQuantityValue CalculateQuantity(DoseInstruction doseInstruction)
      {
         OutputQuantityValue result = new OutputQuantityValue();
         if(doseInstruction != null)
         {
            result.Unit = SimplifyUnit(doseInstruction.DoseQuantity.Unit);
            result.Value = doseInstruction.DoseQuantity.Value;
         }
         return result;
      }

      private static UnitType SimplifyUnit(UnitType unit)
      {
         UnitType result = unit;
         if(unit == UnitType.MgKg)
         {
            result = UnitType.SimplifiedMgKg;
         }
         if(unit == UnitType.Grams)
         {
            result = UnitType.SimplifiedGram;
         }
         return result;
      }

      private static UnitType CheckYearFormat(UnitType fromUnit)
      {
         return fromUnit == UnitType.Years ? UnitType.Year : fromUnit;
      }

      private static string CapitalizeWord(string str)
      {
         return str.First().ToString().ToUpper() + str.Substring(1);
      }

      private static string CalculateFrequency(DoseInstruction doseInstruction)
      {
         string result = string.Empty;
         if(doseInstruction != null)
         {
            string unitString = GetEnumMemberValue(doseInstruction.Frequency.Unit);
            string timesInterval = doseInstruction.Frequency.Value.ToString() + " " + unitString;
            if(doseInstruction.Frequency.FrequencyType == FrequencyType.None)
            {
               result = timesInterval;
            }
            else if(doseInstruction.Frequency.FrequencyType == FrequencyType.Every)
            {
               result = "Every " + timesInterval;
            }
         }
         return result;
      }
   }
}
