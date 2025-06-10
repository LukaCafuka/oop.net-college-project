using DataLayer.JsonModels;
using System.Text;

namespace DataLayer.DataHandling
{
    public class ConfigHandling
    {
        private const char DEL = '=';

        public static event Action OnConfigFileMissing;

        public static void SaveConfig()
        {
            // Use a StringBuilder to construct the config content
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder
                .AppendLine($"language={JsonModels.ConfigFile.language}")
                .AppendLine($"gender={JsonModels.ConfigFile.gender}")
                .AppendLine($"country={JsonModels.ConfigFile.country}")
                .AppendLine($"versusCountry={JsonModels.ConfigFile.versusCountry}")
                .AppendLine($"countryIndex={JsonModels.ConfigFile.countryIndex}")
                .AppendLine($"versusCountryIndex={JsonModels.ConfigFile.versusCountryIndex}")
                .AppendLine($"resolution={JsonModels.ConfigFile.resolution}");

            // Write the constructed string to the config file
            File.WriteAllText(ResourceLocations.ConfigPath, stringBuilder.ToString());
        }

        public static bool ConfigExists()
        {
            if (!Directory.Exists(ResourceLocations.ConfigDir))
            {

                Directory.CreateDirectory(ResourceLocations.ConfigDir);
            }


            if (!File.Exists(ResourceLocations.ConfigPath))
            {
                OnConfigFileMissing?.Invoke();

                return false;
            }

            return true;
        }

        public static List<string> LoadConfig()
        {
            


            string[] lines = File.ReadAllLines(ResourceLocations.ConfigPath);
            List<string> processedData = new List<string>();


            foreach (string item in lines)
            {
                string[] data = item.Split(DEL);
                if (data.Length == 2) 
                {
                    processedData.Add(data[1]);


                    switch (data[0].Trim())
                    {
                        case "language":
                            ConfigFile.language = data[1];
                            break;
                        case "gender":
                            if (Enum.TryParse<ConfigFile.Gender>(data[1].Trim(), true, out var gender))
                            {
                                ConfigFile.gender = gender;
                            }
                            else
                            {
                                throw new Exception();
                            }
                            break;
                        case "country":
                            JsonModels.ConfigFile.country = data[1];
                            break;
                        case "versusCountry":
                            JsonModels.ConfigFile.versusCountry = data[1];
                            break;
                        case "countryIndex":
                            JsonModels.ConfigFile.countryIndex = int.Parse(data[1]);
                            break;
                        case "versusCountryIndex":
                            JsonModels.ConfigFile.versusCountryIndex = int.Parse(data[1]);
                            break;
                        case "resolution":
                            JsonModels.ConfigFile.resolution = data[1];
                            break;
                    }
                }
            }

            return processedData;
        }
    }
}
