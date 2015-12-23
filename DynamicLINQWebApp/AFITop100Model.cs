using System.Collections.Generic;
using System.IO;
using System.Web;
using Newtonsoft.Json;

namespace DynamicLINQWebApp
{
   public class MovieRecord
   {
      public int Rank { get; set; }
      public string Movie { get; set; }
      public int Year { get; set; }
      public string Cast { get; set; }
      public string Director { get; set; }

      public static List<MovieRecord> GetData()
      {
         var path = HttpContext.Current.Server.MapPath(@"\AFITop100.json");
         return JsonConvert.DeserializeObject<List<MovieRecord>>( File.ReadAllText( path ) );
      }
   }

   public class AFITop100Model
   {
      private static List<MovieRecord> _allRecords = MovieRecord.GetData();
      public static List<MovieRecord> AllRecords {  get { return _allRecords; } }
   }
}
