using JsonTask.Entities;
using JsonTask.InputSchema;
using JsonTask.OutputEntities;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace JsonTask.Services
{
   /// <summary>
   /// Command for serializing the drug input object.
   /// </summary>
   public class SerializeCommand
   {
      private DrugsInput input;

      /// <summary>
      /// Constructor.
      /// </summary>
      /// <param name="drugInput">Drug input object.</param>
      public SerializeCommand(DrugsInput drugInput)
      {
         input = drugInput;
      }

      /// <summary>
      /// Process the serialize command.
      /// </summary>
      /// <returns>Serialized output to string.</returns>
      public string Execute()
      {
         Result result = new Result();
         foreach(Drug drug in input.Drugs)
         {
            DrugOutput drugOutput = new DrugOutput();
            drugOutput.Name = drug.Name;
            drugOutput.SuggestedDose = SuggestedDoseService.GetSuggestedDoses(drug);
            result.Drugs.Add(drugOutput);
         }
         return Process(result);
      }

      /// <summary>
      /// Process the model to validate and serialize as json.
      /// I don't like how it looks and I would rather implement it via a custom serializer, instead of series of linq statements.
      /// </summary>
      /// <param name="result">The output data model.</param>
      public string Process(Result result)
      {
         IList<DrugOutput> drugs = result.Drugs;
         JArray drugsArray = new JArray(
            drugs.Select(d => new JObject
            {
                  {"name", d.Name },
                  {"suggestedDose", new JArray(d.SuggestedDose.Select(sd => new JObject
                  {
                     { "indications", new JArray(sd.Indications) },
                     { "doseAdministrations", new JArray(sd.DoseAdministrations.Select(da => new JObject
                     {
                        {"route", da.Route },
                        {"method", da.Method },
                        {"doses", new JArray(da.Doses.Select(o => new JObject
                        {
                           {"ageBand", new JObject {
                             {"ageLow", new JObject { {"value", o.AgeBand.AgeLow.Value }, {"unit", SuggestedDoseService.GetEnumMemberValue(o.AgeBand.AgeLow.Unit) } } },
                             {"ageHigh", new JObject { {"value", o.AgeBand.AgeHigh.Value }, {"unit", SuggestedDoseService.GetEnumMemberValue(o.AgeBand.AgeHigh.Unit) } } } }
                           },
                           {"quantity", new JObject {
                              { "value", o.Quantity.Value },
                              { "unit", SuggestedDoseService.GetEnumMemberValue(o.Quantity.Unit)}
                           } },
                           {"flags", new JObject { { "frequency", o.Flags.Frequency } }
                         },
                        }))
                        }
                     }))
                     }
                  }))
                  }
            })
            );
         JObject results = new JObject(new JProperty("drugs", drugsArray));
         return results.ToString();
      }

   }
}
