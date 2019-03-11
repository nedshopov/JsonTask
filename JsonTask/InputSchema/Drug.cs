namespace JsonTask.Entities
{
   /// <summary>
   /// A base drug class.
   /// </summary>
   public class Drug
   {
      /// <summary>
      /// Name of the drug.
      /// </summary>
      public string Name { get; set; }

      /// <summary>
      /// Indications and dose for the drug.
      /// </summary>
      public IndicationsAndDose IndicationsDose { get; set; }
   }
}
