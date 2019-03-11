using System;
using System.IO;

namespace JsonTaskTests
{
   public static class TestConstants
   {
      public static string Ertapenem {
         get
         {
            string path = Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), "..\\..\\..\\");
            string fileToLoad = "\\input_BNF_ertapenem";
            string result;
            using(StreamReader file = File.OpenText(path + fileToLoad+ ".json"))
            {
               result = file.ReadToEnd();
            }
            return result;
         }
      }

      public static string DrugFileForParsing
      {
         get
         {
            string path = Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), "..\\..\\..\\");
            string fileToLoad = "\\drugfile_forparsing";
            string result;
            using(StreamReader file = File.OpenText(path + fileToLoad + ".json"))
            {
               result = file.ReadToEnd();
            }
            return result;
         }
      }
   }
}
