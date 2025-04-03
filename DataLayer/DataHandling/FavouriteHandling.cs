using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataHandling
{
    class FavouriteHandling
    {
        public static void SaveFavourites(HashSet<string> favourites)
        {
            StringBuilder stringBuilder = new StringBuilder();
            favourites.ToList().ForEach(item => stringBuilder.AppendLine(item));
            File.WriteAllText(ResourceLocations.FavouritesPath, stringBuilder.ToString());
        }
        public static HashSet<string> LoadFavourites()
        {
            string[] lines = File.ReadAllLines(ResourceLocations.FavouritesPath);
            HashSet<string> processedData = new HashSet<string>();
            lines.ToList().ForEach(line => processedData.Add(line));
            JsonModels.ConfigFile.favourites = processedData;
            return processedData;
        }
    }
}
