using JsonTask.Entities;
using JsonTask.InputSchema;
using JsonTask.Services;
using System.Collections.Generic;
using Xunit;

namespace JsonTaskTests
{
   public class SerializeCommandTests
   {
      [Fact]
      public void ValidDrugInput_Serialize_Success()
      {
         // Arrange
         IList<Drug> drugs = new List<Drug> {
            DrugFactory.GetDrug("testname1", "testIndication1", "testRoute"),
            DrugFactory.GetDrug("testname2", "testIndication2", "testRoute")
         };
         DrugsInput input = new DrugsInput { Drugs = drugs };
         SerializeCommand command = new SerializeCommand(input);

         // Act
         var result = command.Execute();

         // Assert
         Assert.Contains("testname1", result);
         Assert.Contains("testIndication1", result);
         Assert.Contains("testname2", result);
         Assert.Contains("testIndication2", result);
         Assert.DoesNotContain("testRoute", result);
         Assert.Contains("TestRoute", result);
      }
   }
}
