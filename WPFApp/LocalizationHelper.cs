using System.ComponentModel;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Windows;
using DataLayer.DataHandling;

namespace WPFApp
{
    public class LocalizationHelper : INotifyPropertyChanged
    {
        private static LocalizationHelper _instance;
        private ResourceManager _resourceManager;

        public static LocalizationHelper Instance => _instance ??= new LocalizationHelper();

        public event PropertyChangedEventHandler PropertyChanged;

        private LocalizationHelper()
        {
            _resourceManager = new ResourceManager("WPFApp.Resources.Strings", typeof(LocalizationHelper).Assembly);
        }

        public string GetString(string key)
        {
            try
            {
                return _resourceManager.GetString(key, CultureInfo.CurrentUICulture) ?? key;
            }
            catch
            {
                return key;
            }
        }

        public void ChangeLanguage(string languageCode)
        {
            CultureInfo newCulture = new CultureInfo(languageCode);
            Thread.CurrentThread.CurrentUICulture = newCulture;
            Thread.CurrentThread.CurrentCulture = newCulture;

            // Notify all bound properties that they should update
            OnPropertyChanged(string.Empty);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Properties for binding in XAML
        public string MainWindowTitle => GetString("MainWindowTitle");
        public string FavoriteTeamLabel => GetString("FavoriteTeamLabel");
        public string OpponentTeamLabel => GetString("OpponentTeamLabel");
        public string InfoButton => GetString("InfoButton");
        public string SettingsButton => GetString("SettingsButton");
        public string SettingsWindowTitle => GetString("SettingsWindowTitle");
        public string InitialSettingsTitle => GetString("InitialSettingsTitle");
        public string WindowResolutionGroupBox => GetString("WindowResolutionGroupBox");
        public string FavoriteCountryGroupBox => GetString("FavoriteCountryGroupBox");
        public string OpponentCountryGroupBox => GetString("OpponentCountryGroupBox");
        public string PlayerInfoWindowTitle => GetString("PlayerInfoWindowTitle");
        public string AddChangeImageButton => GetString("AddChangeImageButton");
        public string NameLabel => GetString("NameLabel");
        public string NumberLabel => GetString("NumberLabel");
        public string PositionLabel => GetString("PositionLabel");
        public string CaptainLabel => GetString("CaptainLabel");
        public string GoalsLabel => GetString("GoalsLabel");
        public string YellowCardsLabel => GetString("YellowCardsLabel");
        public string TeamInfoWindowTitle => GetString("TeamInfoWindowTitle");
        public string FifaCodeLabel => GetString("FifaCodeLabel");
        public string GamesPlayedLabel => GetString("GamesPlayedLabel");
        public string WinsLabel => GetString("WinsLabel");
        public string DrawsLabel => GetString("DrawsLabel");
        public string LossesLabel => GetString("LossesLabel");
        public string GoalsForLabel => GetString("GoalsForLabel");
        public string GoalsAgainstLabel => GetString("GoalsAgainstLabel");
        public string GoalDifferenceLabel => GetString("GoalDifferenceLabel");
        public string CloseButton => GetString("CloseButton");
        public string YesText => GetString("YesText");
        public string NoText => GetString("NoText");
    }
} 