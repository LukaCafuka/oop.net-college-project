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

        public event Action<string>? PlayerImageChanged;

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

            // Load player image
            RefreshPlayerImage();
        }

        private void RefreshPlayerImage()
        {
            if (string.IsNullOrEmpty(_playerName)) return;

            try
            {
                // Set player image - use actual player name without modification
                string playerImagePath = $"Resources/Players/{_playerName}.png";
                string defaultImagePath = "Resources/Players/no_image.png";
                
                if (File.Exists(playerImagePath))
                {
                    LoadImageFromPath(playerImagePath);
                }
                else if (File.Exists(defaultImagePath))
                {
                    LoadImageFromPath(defaultImagePath);
                }
                else
                {
                    imgPlayer.Source = null; // No image available
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading image for {_playerName}: {ex.Message}");
                imgPlayer.Source = null;
            }
        }

        private void LoadImageFromPath(string imagePath)
        {
            var bitmap = new System.Windows.Media.Imaging.BitmapImage();
            bitmap.BeginInit();
            bitmap.CacheOption = System.Windows.Media.Imaging.BitmapCacheOption.OnLoad;
            bitmap.UriSource = new Uri(Path.GetFullPath(imagePath), UriKind.Absolute);
            bitmap.EndInit();
            bitmap.Freeze();
            imgPlayer.Source = bitmap;
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
                    // First copy the file
                    File.Copy(dlg.FileName, destPath, true);
                    
                    // Force a small delay to ensure file is written
                    System.Threading.Thread.Sleep(100);
                    
                    // Refresh the image display
                    RefreshPlayerImage();
                    
                    // Notify that the image changed
                    PlayerImageChanged?.Invoke(_playerName!);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to save image: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
} 