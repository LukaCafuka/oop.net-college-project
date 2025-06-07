using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    class ResourceLocations
    {
        private const string API_JSON_DATA_DIR_NAME = "api-data";
        private const string CONFIG_DIR_NAME = "config";

        private static string? MAIN_SAVE_DIR = Directory.GetCurrentDirectory();

        public static string? ConfigDir = Path.Combine(MAIN_SAVE_DIR, CONFIG_DIR_NAME);
        public static string? ConfigPath = Path.Combine(ConfigDir, "settings.txt");

        public static string? FavouritesDir = Path.Combine(MAIN_SAVE_DIR, CONFIG_DIR_NAME);
        public static string? FavouritesPath = Path.Combine(FavouritesDir, "favourites.txt");

        public const string MaleTeamsURL = "https://worldcup-vua.nullbit.hr/men/teams/results";
        public const string FemaleTeamsURL = "https://worldcup-vua.nullbit.hr/women/teams/results";

        public const string MaleMatchesURL = "https://worldcup-vua.nullbit.hr/men/matches";
        public const string FemaleMatchesURL = "https://worldcup-vua.nullbit.hr/women/matches";

        public const string MaleMatchesDetailedURL = "https://worldcup-vua.nullbit.hr/men/matches/country?fifa_code=ENG";
        public const string FemaleMatchesDetailedURL = "https://worldcup-vua.nullbit.hr/women/matches/country?fifa_code=ENG";


        public static string? FemaleGroupResultsPath = Path.Combine(MAIN_SAVE_DIR, $"{API_JSON_DATA_DIR_NAME}/female_group_results.json");
        public static string? FemaleMatchesPath = Path.Combine(MAIN_SAVE_DIR, $"{API_JSON_DATA_DIR_NAME}/female_matches.json");
        public static string? FemaleResultsPath = Path.Combine(MAIN_SAVE_DIR, $"{API_JSON_DATA_DIR_NAME}/female_results.json");
        public static string? FemaleTeamsPath = Path.Combine(MAIN_SAVE_DIR, $"{API_JSON_DATA_DIR_NAME}/female_teams.json");



        public static string? MaleGroupResultsPath = Path.Combine(MAIN_SAVE_DIR, "api-data/male_group_results.json");
        public static string? MaleMatchesPath = Path.Combine(MAIN_SAVE_DIR, "api-data/male_matches.json");
        public static string? MaleResultsPath = Path.Combine(MAIN_SAVE_DIR, "api-data/male_results.json");
        public static string? MaleTeamsPath = Path.Combine(MAIN_SAVE_DIR, "api-data/male_teams.json");
    }
}
