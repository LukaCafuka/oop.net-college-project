using DataLayer.DataHandling;
using System;
using System.Windows;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                // Initialize culture handling
                CultureHandling.Initialize("WPFApp.Resources.Strings", typeof(App));

                // Check if config exists
                if (!ConfigHandling.ConfigExists())
                {
                    var settingsWindow = new SettingsWindow();
                    if (settingsWindow.ShowDialog() != true)
                    {
                        // User closed the settings window without saving
                        Shutdown();
                        return;
                    }
                }
                else
                {
                    // Load existing config
                    ConfigHandling.LoadConfig();
                    CultureHandling.LoadCulture();
                }

                // Start the main window
                var mainWindow = new MainWindow();
                mainWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during startup: {ex.Message}\n\nStack trace:\n{ex.StackTrace}", 
                    "Startup Error", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error);
                Shutdown();
            }
        }
    }
}
