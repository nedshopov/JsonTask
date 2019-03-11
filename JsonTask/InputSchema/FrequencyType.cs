using System.Runtime.Serialization;

namespace JsonTask.Entities
{
   public enum FrequencyType
   {
      [EnumMember(Value = "")]
      None,
      [EnumMember(Value = "Every")]
      Every
   }
}