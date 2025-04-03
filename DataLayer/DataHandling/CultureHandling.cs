using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataHandling
{
    public class CultureHandling
    {
        private static ResourceManager? rm;

        public static void Initialize(string baseName, Type type)
        {
            rm = new ResourceManager(baseName, type.Assembly);
        }

        public static string GetString(string key)
        {
            try
            {
                return rm.GetString(key);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void SetCulture(string cultureCode)
        {
            var culture = new CultureInfo(cultureCode);

            Thread.CurrentThread.CurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;
        }
        public static void LoadCulture()
        {
            SetCulture(JsonModels.ConfigFile.language == "Croatian" ? "en-US" : "hr-HR");
        }
    }
}
