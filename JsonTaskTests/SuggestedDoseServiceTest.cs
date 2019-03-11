using JsonTask.Entities;
using JsonTask.OutputEntities;
using JsonTask.Services;
using JsonTask.Shared;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace JsonTaskTests
{
   /// <summary>
   /// Test class for testing the SuggestedDoseService.
   /// </summary>
   public class SuggestedDoseServiceTest
   {
      [Fact]
      public void ValidDrug_GetSuggestedDoses_Success()
      {
         // Arrange
         Drug drug = DrugFactory.GetDrug("testname1", "testIndication1", "testRoute");

         // Act
         IList<SuggestedDose> result = SuggestedDoseService.GetSuggestedDoses(drug);

         // Assert
         Assert.Single(result);
         Assert.Equal("testIndication1", result.FirstOrDefault().Indications.FirstOrDefault());
         Assert.Equal("TestRoute", result.FirstOrDefault().DoseAdministrations.FirstOrDefault().Route);
      }

      [Fact]
      public void EnumTypeValue_GetEnumMemberValue_Success()
      {
         // Arrange
         UnitType unitTypeDays = UnitType.Days;
         UnitType unitTypeHours = UnitType.Hours;
         UnitType unitTypeYear = UnitType.Year;
         UnitType unitTypeWeeks = UnitType.Weeks;

         // Act
         string unitTypeDaysMemberValue = SuggestedDoseService.GetEnumMemberValue(unitTypeDays);
         string unitTypeHoursMemberValue = SuggestedDoseService.GetEnumMemberValue(unitTypeHours);
         string unitTypeYearMemberValue = SuggestedDoseService.GetEnumMemberValue(unitTypeYear);
         string unitTypeWeeksMemberValue = SuggestedDoseService.GetEnumMemberValue(unitTypeWeeks);

         // Assert
         Assert.Equal("days", unitTypeDaysMemberValue);
         Assert.Equal("hour(s)", unitTypeHoursMemberValue);
         Assert.Equal("year", unitTypeYearMemberValue);
         Assert.Equal("weeks", unitTypeWeeksMemberValue);
      }
   }
}
