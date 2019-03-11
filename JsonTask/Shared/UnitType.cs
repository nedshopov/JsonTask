using System.Runtime.Serialization;

namespace JsonTask.Shared
{
   /// <summary>
   /// Enumeration of unit types.
   /// </summary>
   public enum UnitType
   {
      [EnumMember(Value = "year")]
      Year,

      [EnumMember(Value = "g")]
      SimplifiedGram,

      [EnumMember(Value = "mg/kg")]
      SimplifiedMgKg,

      [EnumMember(Value = "times daily as a single dose")]
      TimesDailyAsSingleDose,

      [EnumMember(Value = "gram(s)")]
      Grams,

      [EnumMember(Value = "mg(s)/kilogram")]
      MgKg,

      [EnumMember(Value = "hour(s)")]
      Hours,

      [EnumMember(Value = "days")]
      Days,

      [EnumMember(Value = "weeks")]
      Weeks,

      [EnumMember(Value = "months")]
      Months,

      [EnumMember(Value = "years")]
      Years
   }
}