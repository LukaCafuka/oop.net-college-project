using DataLayer.DataHandling;
using DataLayer.JsonModels;
using System;
using System.Windows;

namespace WPFApp
{
    public partial class SettingsWindow : Window
    {
        private string selectedResolution = "Fullscreen";

        public SettingsWindow()
        {
            InitializeComponent();
            LoadExistingSettings();
        }

        private void LoadExistingSettings()
        {
            // Load gender setting
            if (ConfigFile.gender == ConfigFile.Gender.Male)
            {
                rbMale.IsChecked = true;
            }
            else if (ConfigFile.gender == ConfigFile.Gender.Female)
            {
                rbFemale.IsChecked = true;
            }

            // Load language setting
            if (ConfigFile.language == "Croatian")
            {
                rbCroatian.IsChecked = true;
            }
            else if (ConfigFile.language == "English")
            {
                rbEnglish.IsChecked = true;
            }

            // Load resolution setting
            if (!string.IsNullOrEmpty(ConfigFile.resolution))
            {
                selectedResolution = ConfigFile.resolution;
                switch (ConfigFile.resolution)
                {
                    case "Fullscreen":
                        rbFullscreen.IsChecked = true;
                        break;
                    case "1280 x 720":
                        rbResolution1.IsChecked = true;
                        break;
                    case "1600 x 900":
                        rbResolution2.IsChecked = true;
                        break;
                    case "1920 x 1080":
                        rbResolution3.IsChecked = true;
                        break;
                }
            }
        }

        private void rbGender_Checked(object sender, RoutedEventArgs e)
        {
            if (rbMale.IsChecked == true)
            {
                ConfigFile.gender = ConfigFile.Gender.Male;
            }
            else if (rbFemale.IsChecked == true)
            {
                ConfigFile.gender = ConfigFile.Gender.Female;
            }
        }

        private void rbLanguage_Checked(object sender, RoutedEventArgs e)
        {
            if (rbCroatian.IsChecked == true)
            {
                ConfigFile.language = "Croatian";
                CultureHandling.SetCulture("hr-HR");
            }
            else if (rbEnglish.IsChecked == true)
            {
                ConfigFile.language = "English";
                CultureHandling.SetCulture("en-US");
            }
        }

        private void rbResolution_Checked(object sender, RoutedEventArgs e)
        {
            if (rbFullscreen.IsChecked == true)
            {
                selectedResolution = "Fullscreen";
            }
            else if (rbResolution1.IsChecked == true)
            {
                selectedResolution = "1280 x 720";
            }
            else if (rbResolution2.IsChecked == true)
            {
                selectedResolution = "1600 x 900";
            }
            else if (rbResolution3.IsChecked == true)
            {
                selectedResolution = "1920 x 1080";
            }
        }

        private void SaveSettings_Click(object sender, RoutedEventArgs e)
        {
            if (ConfigFile.gender == null)
            {
                MessageBox.Show("Please select a championship", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrEmpty(ConfigFile.language))
            {
                MessageBox.Show("Please select a language", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            ConfigFile.resolution = selectedResolution;
            ConfigHandling.SaveConfig();

            MessageBox.Show("Settings have been saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            DialogResult = true;
            Close();
        }
    }
} 