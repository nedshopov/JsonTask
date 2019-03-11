using JsonTask.InputSchema;
using JsonTask.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.IO;
using System.Reflection;

namespace JsonTask
{
   public class Program
   {
      public static void Main(string[] args)
      {
         //string fileToLoad = "drugfile_forparsing";
         string fileToLoad = "input_BNF_ertapenem";
         SetJsonConverter();
         string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
         ParseCommand command = new ParseCommand(path +"\\" + fileToLoad+ ".json");
         DrugsInput parsedInput = command.Execute();
         SerializeCommand serializeCommand = new SerializeCommand(parsedInput);
         string resultJson = serializeCommand.Execute();
         WriteJsonToFile(resultJson);
      }

      /// <summary>
      /// Initiation settings for the json serializer.
      /// </summary>
      public static void SetJsonConverter()
      {
         DefaultContractResolver contractResolver = new DefaultContractResolver
         {
            NamingStrategy = new CamelCaseNamingStrategy
            {
               OverrideSpecifiedNames = false
            }
         };
         JsonConvert.DefaultSettings = (() =>
         {
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new StringEnumConverter());
            settings.ContractResolver = contractResolver;
            settings.Formatting = Formatting.Indented;
            return settings;
         });
      }

      private static void WriteJsonToFile(string jsonResult)
      {
         using(StreamWriter file = File.CreateText("output.json"))
         {
            file.Write(jsonResult);
         }
      }
   }
}
