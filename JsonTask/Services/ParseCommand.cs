using JsonTask.Entities;
using JsonTask.InputSchema;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using Newtonsoft.Json.Serialization;
using System.IO;

namespace JsonTask
{
   /// <summary>
   /// The main service class.
   /// </summary>
   public class ParseCommand
   {
      private string filepath;

      /// <summary>
      /// Constructor.
      /// </summary>
      /// <param name="filepath">Path to the file to use as input.</param>
      public ParseCommand(string filepath)
      {
         this.filepath = filepath;
      }

      /// <summary>
      /// Main run function.
      /// </summary>
      public DrugsInput Execute()
      {
         using(StreamReader file = File.OpenText(this.filepath))
         {
            string jsonInput = file.ReadToEnd();
            return Deserialize(jsonInput);
         }
      }

      /// <summary>
      /// Start parsing the json string into object models.
      /// </summary>
      /// <param name="jsonInput">json string.</param>
      /// <returns>DrugsInput model</returns>
      public DrugsInput Deserialize(string jsonInput)
      {
         DrugsInput input = JsonConvert.DeserializeObject<DrugsInput>(jsonInput);
         return input;
      }

      /// <summary>
      /// Templorary helper method used to generate validation schema. Can be expanded with setting and re-used. Ideally on runtime.
      /// </summary>
      private void GenerateSchemaForInput()
      {
         JSchemaGenerator generator = new JSchemaGenerator();
         generator.DefaultRequired = Required.AllowNull;
         generator.ContractResolver = new CamelCasePropertyNamesContractResolver();
         JSchema jSchema = generator.Generate(typeof(Drug));
      }
   }
}

