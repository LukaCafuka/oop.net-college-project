using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;

namespace WindowsFormsApp
{
    public class PlayerImageManager
    {
        private const string IMAGES_DIR = "Resources/PlayerImages";
        private const string DEFAULT_IMAGE_PATH = "Resources/PlayerImages/default_player.png";
        private static Image defaultImage;

        public PlayerImageManager()
        {
            EnsureDirectoryExists();
            EnsureDefaultImageExists();
            LoadDefaultImage();
        }

        private void EnsureDirectoryExists()
        {
            if (!Directory.Exists(IMAGES_DIR))
            {
                Directory.CreateDirectory(IMAGES_DIR);
            }
        }

        private void EnsureDefaultImageExists()
        {
            if (!File.Exists(DEFAULT_IMAGE_PATH))
            {
                CreateDefaultImage();
            }
        }

        private void LoadDefaultImage()
        {
            try
            {
                if (defaultImage == null)
                {
                    using (var stream = new FileStream(DEFAULT_IMAGE_PATH, FileMode.Open, FileAccess.Read))
                    {
                        defaultImage = new Bitmap(Image.FromStream(stream));
                    }
                }
            }
            catch (Exception)
            {
                // If loading fails, create a new default image
                CreateDefaultImage();
                using (var stream = new FileStream(DEFAULT_IMAGE_PATH, FileMode.Open, FileAccess.Read))
                {
                    defaultImage = new Bitmap(Image.FromStream(stream));
                }
            }
        }

        private void CreateDefaultImage()
        {
            using (var bitmap = new Bitmap(80, 80))
            {
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.Clear(Color.LightGray);
                    using (var font = new Font("Arial", 24))
                    {
                        var text = "?";
                        var textSize = graphics.MeasureString(text, font);
                        var x = (bitmap.Width - textSize.Width) / 2;
                        var y = (bitmap.Height - textSize.Height) / 2;
                        graphics.DrawString(text, font, Brushes.DarkGray, x, y);
                    }
                }
                bitmap.Save(DEFAULT_IMAGE_PATH, ImageFormat.Png);
            }
        }

        public Image LoadPlayerImage(string playerName)
        {
            try
            {
                string imagePath = GetImagePath(playerName);
                if (File.Exists(imagePath))
                {
                    // Create a copy of the image to avoid file locking issues
                    using (var stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                    {
                        return new Bitmap(Image.FromStream(stream));
                    }
                }
                // Return a copy of the default image
                return new Bitmap(defaultImage);
            }
            catch (Exception)
            {
                // If there's any error, return a copy of the default image
                return new Bitmap(defaultImage);
            }
        }

        public async Task<string> SavePlayerImageAsync(string playerName, Image image)
        {
            try
            {
                string imagePath = GetImagePath(playerName);
                using (var stream = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                {
                    // Create a copy of the image to avoid any potential locking issues
                    using (var bitmap = new Bitmap(image))
                    {
                        await Task.Run(() => bitmap.Save(stream, ImageFormat.Png));
                    }
                }
                return imagePath;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to save image: {ex.Message}");
            }
        }

        private string GetImagePath(string playerName)
        {
            string sanitizedName = string.Join("_", playerName.Split(Path.GetInvalidFileNameChars()));
            return Path.Combine(IMAGES_DIR, $"{sanitizedName}.png");
        }
    }
}  