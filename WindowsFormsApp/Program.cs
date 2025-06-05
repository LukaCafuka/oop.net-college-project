using DataLayer.DataHandling;
using System.Diagnostics;

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
            try
            {
                Debug.WriteLine("Starting application...");
                
                // To customize application configuration such as set high DPI settings or default font,
                // see https://aka.ms/applicationconfiguration.
                Debug.WriteLine("Initializing culture...");
                CultureHandling.Initialize("WindowsFormsApp.FormLang", typeof(ConfigForm));
                
                Debug.WriteLine("Initializing application configuration...");
                ApplicationConfiguration.Initialize();
                
                Debug.WriteLine("Creating main form...");
                var mainForm = new Form1();
                
                Debug.WriteLine("Running application...");
                Application.Run(mainForm);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in Main: {ex}");
                MessageBox.Show($"An error occurred: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}