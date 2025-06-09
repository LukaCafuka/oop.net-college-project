using System;
using System.Windows;
using System.Windows.Media.Animation;
using DataLayer.JsonModels;
using QuickType;
using Microsoft.Win32;
using System.IO;

namespace WPFApp
{
    public partial class PlayerInfoWindow : Window
    {
        private string? _playerName;

        public PlayerInfoWindow(StartingEleven player, int goals, int yellowCards)
        {
            InitializeComponent();
            Loaded += PlayerInfoWindow_Loaded;
            _playerName = player.Name;
            PopulatePlayerData(player, goals, yellowCards);
        }

        private void PlayerInfoWindow_Loaded(object sender, RoutedEventArgs e)
        {
            AnimateIn();
        }

        private void PopulatePlayerData(StartingEleven player, int goals, int yellowCards)
        {
            txtName.Text = player.Name;
            txtNumber.Text = player.ShirtNumber.ToString();
            txtPosition.Text = player.Position.ToString();
            txtCaptain.Text = player.Captain ? "Yes" : "No";
            txtGoals.Text = goals.ToString();
            txtYellowCards.Text = yellowCards.ToString();

            // Set player image
            string playerImagePath = $"Resources/Players/{player.Name}.png";
            string defaultImagePath = "Resources/default_player.png";
            try
            {
                if (File.Exists(playerImagePath))
                {
                    imgPlayer.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(Path.GetFullPath(playerImagePath)));
                }
                else if (File.Exists(defaultImagePath))
                {
                    imgPlayer.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(Path.GetFullPath(defaultImagePath)));
                }
                else
                {
                    imgPlayer.Source = null; // No image available
                }
            }
            catch
            {
                imgPlayer.Source = null;
            }
        }

        private void AnimateIn()
        {
            // Create a scale animation
            var scaleX = new DoubleAnimation
            {
                From = 0.5,
                To = 1.0,
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            var scaleY = new DoubleAnimation
            {
                From = 0.5,
                To = 1.0,
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            // Create an opacity animation
            var opacity = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.3),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            // Apply the animations
            var transform = new System.Windows.Media.ScaleTransform();
            RenderTransform = transform;
            RenderTransformOrigin = new Point(0.5, 0.5);

            transform.BeginAnimation(System.Windows.Media.ScaleTransform.ScaleXProperty, scaleX);
            transform.BeginAnimation(System.Windows.Media.ScaleTransform.ScaleYProperty, scaleY);
            BeginAnimation(OpacityProperty, opacity);
        }

        private void btnChangeImage_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_playerName)) return;
            var dlg = new OpenFileDialog
            {
                Filter = "Image Files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg",
                Title = "Select Player Image"
            };
            if (dlg.ShowDialog() == true)
            {
                string destDir = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Players");
                if (!Directory.Exists(destDir))
                    Directory.CreateDirectory(destDir);
                string destPath = Path.Combine(destDir, _playerName + ".png");
                try
                {
                    File.Copy(dlg.FileName, destPath, true);
                    imgPlayer.Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(destPath));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to save image: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
} 