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
        public App()
        {
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            System.Threading.Tasks.TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show($"Unhandled UI Exception: {e.Exception.Message}\n\n{e.Exception.StackTrace}", "Unhandled Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                MessageBox.Show($"Unhandled Domain Exception: {ex.Message}\n\n{ex.StackTrace}", "Unhandled Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TaskScheduler_UnobservedTaskException(object? sender, System.Threading.Tasks.UnobservedTaskExceptionEventArgs e)
        {
            MessageBox.Show($"Unobserved Task Exception: {e.Exception.Message}\n\n{e.Exception.StackTrace}", "Unhandled Exception", MessageBoxButton.OK, MessageBoxImage.Error);
            e.SetObserved();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                base.OnStartup(e);

                // Initialize culture
                CultureHandling.Initialize("WPFApp.Resources.Strings", typeof(App));

                // Check config
                if (!ConfigHandling.ConfigExists())
                {
                    var settingsWindow = new SettingsWindow();
                    if (settingsWindow.ShowDialog() != true)
                    {
                        Shutdown();
                        return;
                    }
                }
                else
                {
                    ConfigHandling.LoadConfig();
                    CultureHandling.LoadCulture();
                }

                var mainWindow = new MainWindow();
                mainWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Startup error: {ex.Message}\n\n{ex.StackTrace}", "Startup Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown();
            }
        }
    }
}
