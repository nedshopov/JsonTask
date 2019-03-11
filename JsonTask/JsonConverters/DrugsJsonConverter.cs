using JsonTask.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace JsonTask.InputSchema
{
   /// <summary>
   /// Custom json converter for Drug model.
   /// </summary>
   public class DrugsJsonConverter : JsonConverter
   {
      public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
      {
         JToken token = JToken.Load(reader);
         return DeserializeDrugs(token);
      }

      public object DeserializeDrugs(JToken token)
      {
         var list = new List<Drug>();
         bool saveErrors = false;
         foreach(JToken child in token.Children())
         {
            IList<string> messages;
            bool isValid = child.IsValid(GetSchema(), out messages);
            if(isValid)
            {
               try
               {
                  var newObject = JsonConvert.DeserializeObject(child.ToString(), typeof(Drug));
                  list.Add(newObject as Drug);
               }
               catch(Exception)
               {
                  throw;
               }
            }

            else
            {
               string name = child.Children().FirstOrDefault().ToString();
               AddErrors(messages, name);
               saveErrors = true;
            }
         }
         if(saveErrors)
         {
            SaveErrors();
         }
         return list;
      }

      public override bool CanConvert(Type objectType)
      {
         return objectType.IsGenericType && (objectType.GetGenericTypeDefinition() == typeof(List<>));
      }

      public override bool CanWrite => false;
      public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => throw new NotImplementedException();

      /// <summary>
      /// Add errors to the log file.
      /// </summary>
      /// <param name="messages">List of errors.</param>
      /// <param name="drugName">Related drug.</param>
      private void AddErrors(IList<string> messages, string drugName)
      {
         using(StreamWriter writer = new StreamWriter("ErrorsLog.txt", true))
         {
            Console.WriteLine("Error found! See ErrorLogs.txt");
            foreach(string message in messages)
            {
               writer.WriteLine("******  ERROR ********");
               writer.WriteLine(drugName);
               writer.WriteLine(DateTime.Now);
               writer.WriteLine(message);
               writer.WriteLine(Environment.NewLine);
            }
         }
      }

      /// <summary>
      /// Write footer to the log file for better readability.
      /// </summary>
      private void SaveErrors()
      {
         using(StreamWriter writer = new StreamWriter("ErrorsLog.txt", true))
         {
            writer.WriteLine(Environment.NewLine);
            writer.WriteLine("***************************");
            writer.WriteLine(Environment.NewLine);
         }
      }

      private JSchema GetSchema()
      {
         string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "..\\..\\..\\");
         using(StreamReader file = File.OpenText(path + "\\DrugValidation.json"))
         {
            string jsonSchema = file.ReadToEnd();
            return JSchema.Parse(jsonSchema);
         }
      }
   }
}
