using JsonTask;
using System.Linq;
using Xunit;

namespace JsonTaskTests
{
   public class ParseCommandTests
   {
      [Fact]
      public void RegularInputErtapenem_Deserialize_Success()
      {
         // Arrange
         string jsonInput = TestConstants.Ertapenem;
         ParseCommand command = new ParseCommand("test");

         // Act
         var result = command.Deserialize(jsonInput);

         // Assert
         Assert.Equal(1, result.Drugs.Count);
         Assert.Equal("ertapenem", result.Drugs.AsQueryable().FirstOrDefault().Name);
         Assert.Equal(2, result.Drugs.AsQueryable().FirstOrDefault().IndicationsDose.IndicationAndDoseGroups.Count());
      }

      [Fact]
      public void DrugFileForParsing_Deserialize_Success()
      {
         // Arrange
         string jsonInput = TestConstants.DrugFileForParsing;
         ParseCommand command = new ParseCommand("test");

         // Act
         var result = command.Deserialize(jsonInput);

         // Assert
         Assert.Equal(3, result.Drugs.Count);
         Assert.Single(result.Drugs[0].IndicationsDose.IndicationAndDoseGroups);
         Assert.Single(result.Drugs[1].IndicationsDose.IndicationAndDoseGroups);
         Assert.Single(result.Drugs[2].IndicationsDose.IndicationAndDoseGroups);
      }
   }
}
