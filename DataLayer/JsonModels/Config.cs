using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.JsonModels
{
    public class ConfigFile
    {
        public static string? language;

        public static Gender? gender;
        public enum Gender {
            Male,
            Female
        };
        public static string? country;
        public static string? versusCountry;
        public static string? resolution;
        public static HashSet<string>? favourites;
        public static int countryIndex;
        public static int versusCountryIndex;
    }
}
