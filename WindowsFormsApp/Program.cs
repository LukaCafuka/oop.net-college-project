using DataLayer.DataHandling;
namespace WindowsFormsApp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            CultureHandling.Initialize("WindowsFormsApp.FormLang", typeof(ConfigForm));
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}