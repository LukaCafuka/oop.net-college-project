using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataHandling
{
    public class FavouriteHandling
    {
        public static void SaveFavourites(HashSet<string> favourites)
        {
            StringBuilder stringBuilder = new StringBuilder();
            favourites.ToList().ForEach(item => stringBuilder.AppendLine(item));
            File.WriteAllText(ResourceLocations.FavouritesPath, stringBuilder.ToString());
        }
        public static HashSet<string> LoadFavourites()
        {
            if (!(File.Exists(ResourceLocations.FavouritesPath)))
            {
                File.Create(ResourceLocations.FavouritesPath).Close();
            }
            string[] lines = File.ReadAllLines(ResourceLocations.FavouritesPath);
            HashSet<string> processedData = new HashSet<string>();
            lines.ToList().ForEach(line => processedData.Add(line));
            JsonModels.ConfigFile.favourites = processedData;
            return processedData;
        }
    }
}
