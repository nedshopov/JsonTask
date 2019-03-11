using JsonTask.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace JsonTask.InputSchema
{
   public class DrugsInput
   {
      [JsonConverter(typeof(DrugsJsonConverter))]
      public IList<Drug> Drugs { get; set; } = new List<Drug>();
   }
}
